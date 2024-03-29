﻿using AutoMapper;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using SKIPQzAPI.DataAccess;
using SKIPQzAPI.Dtos;
using SKIPQzAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SKIPQzAPI.Models.Time;

namespace SKIPQzAPI
{
    public class MapperProfile:Profile
    {
      
        public MapperProfile()
        {
            
            CreateMap<ServiceProvider, ServiceProviderDto>()
                .ConvertUsing(typeof(ServiceProviderConvertor));
        
            CreateMap<ServiceProviderDto, ServiceProvider>().ConvertUsing(typeof(ServiceProviderDtoConvertor));

            CreateMap<ServiceDto, Service>().ForMember(des=>des.Id,config=>config.MapFrom(s=>s.ServiceId));
            CreateMap<Service, ServiceDto>().ConvertUsing(typeof(ServiceConvertor));
            CreateMap<WorkingDayDto, WorkingDay>().ConvertUsing(typeof(WorkingDayDtoConvertor));
            CreateMap<WorkingDay, WorkingDayDto>().ConvertUsing(typeof(WorkingDayConvertor));
            CreateMap<BookingDto, Booking>().ConvertUsing(typeof(BookingDtoConvertor));
            CreateMap<Booking, BookingDto>().ConvertUsing(typeof(BookingConvertor));
            CreateMap<ExtraDto,Extra>().ReverseMap().ForMember(destination=>destination.ExtraId,opt=>opt.MapFrom(src=>src.Id));
            
            CreateMap<ClientInfoDTO,ClientInfo>().ReverseMap();
            CreateMap<ClientInfoDTO, ClientInfoCreateDTO>().ReverseMap();
            CreateMap<Organisation, OrganisationDto>().ReverseMap();
            CreateMap<Organisation, OrganisationCreateDto>().ReverseMap();
        }
    }


    public class ServiceConvertor : ITypeConverter<Service, ServiceDto>
    {
        private readonly ApplicationDbContext _dbContext;

        public ServiceConvertor(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        ServiceDto ITypeConverter<Service, ServiceDto>.Convert(Service source, ServiceDto destination, ResolutionContext context)
        {
            return( new ServiceDto {
                Cost = source.Cost,
                Duration = source.Duration,
                ImageUrl = source.ImageUrl,
                Name = source.Name,
                ExtraIds = _dbContext.ServiceExtras.Where(svExtra => svExtra.Service.Id == source.Id).Select(sv => sv.Extra.Id).ToList(),
                ServiceId = source.Id
                
            });
        }

    }

  
    public class BookingConvertor : ITypeConverter<Booking, BookingDto>
    {
        private readonly ApplicationDbContext _dbContext;
        public BookingConvertor(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        BookingDto ITypeConverter<Booking, BookingDto>.Convert(Booking source, BookingDto destination, ResolutionContext context)
        {
            TimeComponentInterval bookedTimeInterval = _dbContext.Bookings.Where(bk => bk.Id == source.Id).Select(bk => new TimeComponentInterval { EndTime=bk.BookedTimeInterval.EndTime,StartTime=bk.BookedTimeInterval.StartTime}).FirstOrDefault();
            BookingDto newBookingDto = new BookingDto
            {
                BookedDate = $"{source.BookedDate.Year}-{source.BookedDate.Month-1}-{source.BookedDate.Day}",
                BookingId = source.Id,
                ServiceId = source.ServiceId,
                ServiceProviderId = source.ServiceProviderId,
                EndTimeSlot = bookedTimeInterval?.EndTime.ToString()??"",
                StartTimeSlot = bookedTimeInterval?.StartTime.ToString()??"",
                ExtraIds = _dbContext.Bookings.Where(bk => bk.Id == source.Id).Select(bk => new { Extras = bk.Extras }).FirstOrDefault()?.Extras.Select(ex => ex.Id).ToList() ?? new List<long?>(),
                Cost = source.Cost,
                CanCancel = source.CanCancel
            };

            return newBookingDto;
        }
    }

    public class BookingDtoConvertor : ITypeConverter<BookingDto, Booking>
    {
        private readonly ApplicationDbContext _dbContext;
        public BookingDtoConvertor(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        Booking ITypeConverter<BookingDto, Booking>.Convert(BookingDto source, Booking destination, ResolutionContext context)
        {
            var dateParts = source.BookedDate.Split('-').Select(part => int.Parse(part.Trim()));
            var bookedDate = new DateTime(dateParts.ElementAt(0), dateParts.ElementAt(1) + 1, dateParts.ElementAt(2));

            var workDay = _dbContext.WorkingDays.FirstOrDefault(wd => wd.ServiceProviderId == source.ServiceProviderId && wd.WeekDay == bookedDate.DayOfWeek);
            Booking newBooking = new Booking
            {
                BookedDate = bookedDate ,
                Id = source.BookingId,
                ServiceId = source.ServiceId,
                ServiceProviderId = source.ServiceProviderId,
                BookedTimeInterval =  new TimeComponentInterval {
                    StartTime = new TimeComponent(source.StartTimeSlot),
                    EndTime = new TimeComponent(source.EndTimeSlot),
                    WorkingDayId = workDay!=null?workDay.WorkingDayId:0
                },
                Extras = _dbContext.Bookings.Where(bk=>bk.Id==source.BookingId).Select(bk=>bk.Extras).FirstOrDefault()??source.ExtraIds?.Select(exId=>_dbContext.Extras.FirstOrDefault(ex=>ex.Id==exId)).ToList().Where(ex=>ex!=null).ToList()
            };

            return newBooking;
        }
    }
    public class ServiceProviderConvertor : ITypeConverter<ServiceProvider, ServiceProviderDto>
    {
        private readonly ApplicationDbContext _dbContext;

        public ServiceProviderConvertor(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public ServiceProviderDto Convert(ServiceProvider source, ServiceProviderDto destination, ResolutionContext context)
        {
            var sPDto = new ServiceProviderDto();
            var spUser = _dbContext.ServiceProviders.FirstOrDefault(
                sp => sp.Id == source.Id
                 );
            var email =  spUser?.User?.Email??"";
            sPDto.Email = email;

            sPDto.Name = source.Name;
            sPDto.ServiceProviderId = source.Id;
            sPDto.ScheduledWorkDays = _dbContext.WorkingDays.Where(wD => wD.ServiceProviderId == source.Id).Select(wD => new WorkingDayDto
            {
                DayOfWeek = wD.WeekDay,
                Shifts = wD.Shifts.Select(shift => new TimeInterval
                {
                    EndTimeSlot = shift.EndTime.ToString(),
                    StartTimeSlot = shift.StartTime.ToString(),
                    TimeIntervalId = shift.TimeComponentIntervalId
                }).ToList(),
                WorkingDayId = wD.WorkingDayId
               
            }).ToList();
            sPDto.Services = _dbContext.ServiceProviderServices
                            .Where(sps => sps.ServiceProvider.Id == source.Id)
                            .Join(_dbContext.Services, (sps) => sps.Service.Id, (sv) => sv.Id, (sps, sv) => new ServiceDto { Name = sv.Name, Duration = sv.Duration, Cost = sv.Cost, ServiceId = sv.Id })
                            .ToList();
            sPDto.ImageUrl = source.ImageUrl;
            return sPDto;
        }
    }
    public class ServiceProviderDtoConvertor : ITypeConverter<ServiceProviderDto, ServiceProvider>
    {
        private readonly ApplicationDbContext _dbContext;
        public ServiceProviderDtoConvertor(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public ServiceProvider Convert(ServiceProviderDto sourceMember, ServiceProvider destination, ResolutionContext context)
        {
            var sProvider = new ServiceProvider();
            sProvider.Name = sourceMember.Name;
            sProvider.WorkingDays = sourceMember.ScheduledWorkDays.Select(sD => new WorkingDay {WorkingDayId=sD.WorkingDayId, WeekDay = sD.DayOfWeek, Shifts = sD.Shifts.Select(tI => new TimeComponentInterval(new TimeComponent(tI.StartTimeSlot), new TimeComponent(tI.EndTimeSlot),sD.WorkingDayId,tI.TimeIntervalId)).ToList() }).ToList();
            sProvider.Id = sourceMember.ServiceProviderId;
            sProvider.User = _dbContext.Users.FirstOrDefault(user => user.Email == sourceMember.Email);
            sProvider.ImageUrl = sourceMember.ImageUrl;
            return sProvider;
        }
    }

    public class WorkingDayDtoConvertor : ITypeConverter<WorkingDayDto, WorkingDay>
    {

        public WorkingDay Convert(WorkingDayDto sourceMember, WorkingDay destination, ResolutionContext context)
        {
            var wDay = new WorkingDay();
            wDay.Shifts = sourceMember.Shifts.Select(tInterval => {
                return new TimeComponentInterval(new TimeComponent(tInterval.StartTimeSlot), new TimeComponent(tInterval.EndTimeSlot));
            }).ToList();
            wDay.WeekDay = sourceMember.DayOfWeek;
            return wDay;
        }
    }

    public class WorkingDayConvertor : ITypeConverter<WorkingDay, WorkingDayDto>
    {

        public WorkingDayDto Convert(WorkingDay sourceMember, WorkingDayDto destination, ResolutionContext context)
        {
            var hasShifts = sourceMember.Shifts.Count() > 0;
            var workinDayDto = new WorkingDayDto();
            workinDayDto.DayOfWeek = sourceMember.WeekDay;
            workinDayDto.Shifts = sourceMember.Shifts.Where((tc, index) => index > 0).Select(tc => new TimeInterval { StartTimeSlot = tc.StartTime.ToString(), EndTimeSlot = tc.EndTime.ToString() }).ToList();
            return workinDayDto;
        }
    }
}
