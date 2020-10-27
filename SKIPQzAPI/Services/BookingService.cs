using AutoMapper;
using SKIPQzAPI.DataAccess;
using SKIPQzAPI.Dtos;
using SKIPQzAPI.Models;
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
        public BookingService(ApplicationDbContext dbContext,IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<BookingDto> AddBooking(BookingDto bookingDto)
        {
            Booking newBooking = _mapper.Map<Booking>(bookingDto);
            _dbContext.Add(newBooking);
            var affected = await _dbContext.SaveChangesAsync();
            var lastBooking = _dbContext.Bookings.OrderByDescending(bk => bk.BookingId).FirstOrDefault();
            return affected > 0 ? _mapper.Map<BookingDto>(lastBooking) : null;
        }
    }
}
