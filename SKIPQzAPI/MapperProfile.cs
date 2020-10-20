using AutoMapper;
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

            CreateMap<ServiceDto, Service>();
            CreateMap<Service, ServiceDto>();

            CreateMap<WorkingDayDto, WorkingDay>().ConvertUsing(typeof(WorkingDayDtoConvertor));
            CreateMap<WorkingDay, WorkingDayDto>().ConvertUsing(typeof(WorkingDayConvertor));
           
            
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
            var spUser = _dbContext.ServiceProviders.Where(
                sp => sp.ServiceProviderId == source.ServiceProviderId
                 ).Select(sp => sp.User).ToList();
            var email =  spUser.Count() > 0 ? spUser.ElementAt(0).Email : "";
            sPDto.Email = email;

            sPDto.Name = source.Name;
            sPDto.ServiceProviderId = source.ServiceProviderId;
            sPDto.ScheduledWorkDays = _dbContext.WorkingDays.Where(wD => wD.ServiceProviderId == source.ServiceProviderId).Select(wD => new WorkingDayDto
            {
                DayOfWeek = wD.WeekDay,
                Shifts = wD.Shifts.Select(shift => new TimeInterval
                {
                    EndTimeSlot = shift.EndTime.ToString(),
                    StartTimeSlot = shift.StartTime.ToString()
                }).ToList()
            }).ToList();
            sPDto.Services = _dbContext.ServiceProviderServices
                            .Where(sps => sps.ServiceProvider.ServiceProviderId == source.ServiceProviderId)
                            .Join(_dbContext.Services, (sps) => sps.Service.ServiceId, (sv) => sv.ServiceId, (sps, sv) => new ServiceDto { Name = sv.Name, Duration = sv.Duration, Cost = sv.Cost, ServiceId = sv.ServiceId })
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
            sProvider.WorkingDays = sourceMember.ScheduledWorkDays.Select(sD => new WorkingDay { WeekDay = sD.DayOfWeek, Shifts = sD.Shifts.Select(tI => new TimeComponentInterval(new TimeComponent(tI.StartTimeSlot), new TimeComponent(tI.EndTimeSlot))).ToList() }).ToList();
            sProvider.ServiceProviderId = sourceMember.ServiceProviderId;
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
