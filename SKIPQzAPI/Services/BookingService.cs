using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SKIPQzAPI.Common.Constants;
using SKIPQzAPI.DataAccess;
using SKIPQzAPI.Dtos;
using SKIPQzAPI.Models;
using SKIPQzAPI.Models.Time;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKIPQzAPI.Services
{
    public class BookingService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly ExtraService _extraService;
        private readonly UserManager<IdentityUser> _userManager;
        public BookingService(ApplicationDbContext dbContext,IMapper mapper, IConfiguration config,  ExtraService extraService,UserManager<IdentityUser> userManager)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _configuration = config;
            _extraService = extraService;
            _userManager = userManager;
        }

        public async Task<BookingDto> AddBooking(BookingDto bookingDto)
        {
            var minutesInterval = _dbContext.Services.FirstOrDefault(sv => sv.Id == bookingDto.ServiceId)?.Duration ?? _configuration.GetSection("TimeSlotIntervalLength").Get<double>();
            bookingDto.EndTimeSlot = new TimeComponent(bookingDto.StartTimeSlot).AddMinutes(minutesInterval).ToString();
            
            Booking newBooking = _mapper.Map<Booking>(bookingDto);
            newBooking.Cost = _extraService.GetServiceExtras(newBooking.ServiceId).Aggregate(0m, (carry, next) => carry + next.Cost)+_dbContext.Services.FirstOrDefault(s=>s.Id==newBooking.ServiceId)?.Cost??0m;
            newBooking.client = await _userManager.FindByNameAsync(bookingDto.UserName);
            _dbContext.Add(newBooking);
            var affected = await _dbContext.SaveChangesAsync();
            var lastBooking = _dbContext.Bookings.OrderByDescending(bk => bk.Id).FirstOrDefault();
            return affected > 0 ? _mapper.Map<BookingDto>(lastBooking) : null;
        }

        public async Task<BookingDto> DeleteBooking(int bookingId)
        {
            var removed = _dbContext.Bookings.Where(bk => bk.Id == bookingId).Select(bk=>new Booking { Id=bk.Id,Extras =bk.Extras}).FirstOrDefault();
            BookingDto removedDto = null;
            var affected = 0;
            if(removed!=null)
            {
                removedDto = _mapper.Map<BookingDto>(removed);
                removed.Extras.ForEach(ex => { _dbContext.Remove(ex); });
                _dbContext.Remove(removed);
                affected = await _dbContext.SaveChangesAsync();
            }

            return affected > 0 ? removedDto : null;
        }

        public List<BookingDto> GetBookings(int pageIndex,int pageSize)
        {
            return _dbContext.Bookings
                .OrderByDescending(bk => bk.Id)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToList()
                .Select(bk => _mapper.Map<BookingDto>(bk))
                .ToList();
        }

        public IEnumerable<BookingDto> BookingsPerUser(string userName)
        {
            var userBookings = (from booking in _dbContext.Bookings
                               where booking.client.UserName == userName && booking.Status != PITStatus.InActive
                               orderby booking.BookedDate.Date descending, booking.BookedTimeInterval.StartTime.Hour ascending, booking.BookedTimeInterval.StartTime.Minute ascending
                               select booking)
                               .ToList()
                               .Select(b=>_mapper.Map<BookingDto>(b));
            return userBookings;
        }

        public SysResult<long?> CancelUserBooking(string userName,long? bookingId)
        {
            var cancelledBooking = _dbContext.Bookings.FirstOrDefault(bk => bk.Id == bookingId && bk.client.UserName == userName);
            if (cancelledBooking != null)
            {
                cancelledBooking.Status = PITStatus.InActive;
               var cancelOk =  _dbContext.SaveChanges();
                return new SysResult<long?> { Data = cancelledBooking.Id, Ok = cancelOk>0, Message = cancelOk >0? "Booking Cancelled" : "Failed To Cancel Booking" };
            }
            else
            {
                return new SysResult<long?> { Data = default, Ok = false, Message = "No Such Booking Exists" };
            }
        }
    }
}
