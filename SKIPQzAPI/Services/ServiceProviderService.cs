﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using SKIPQzAPI.Common.Constants;
using SKIPQzAPI.DataAccess;
using SKIPQzAPI.Dtos;
using SKIPQzAPI.Models;
using SKIPQzAPI.Models.Time;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SKIPQzAPI.Services
{
    public class ServiceProviderService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;
        public ServiceProviderService(IMapper mapper,
            ApplicationDbContext dbContext,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration

            )
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
            _configuration = configuration;
        }
        public ServiceProviderDto GetServiceProvider(int serviceProviderId)
        {
           var serviceProvider=  _dbContext.ServiceProviders.FirstOrDefault(sP => sP.Id == serviceProviderId);
            return _mapper.Map<ServiceProviderDto>(serviceProvider);
        }

        public IEnumerable<ServiceProviderDto> GetServiceProviders(int serviceId,int pageSize,int pageIndex=0)
        {
           return _dbContext.ServiceProviderServices
                .OrderByDescending(spsRec => spsRec.ServiceProvider.Id)
                .Where(rec => rec.Service.Id == serviceId)
                .Skip(pageIndex*pageSize)
                .Take(pageSize)
                .Select(spS=>spS.ServiceProvider)
                .ToList()
                .Select(sP=>_mapper.Map<ServiceProviderDto>(sP));
        }

        public IEnumerable<ServiceProviderDto> GetServiceProviders(int pageSize, int pageIndex = 0)
        {
            return _dbContext.ServiceProviders
                 .OrderByDescending(sp=>sp.Id)
                 .Skip(pageIndex * pageSize)
                 .Take(pageSize)
                 .ToList()
                 .Select(sP => _mapper.Map<ServiceProviderDto>(sP));
        }

        public async Task<ServiceProviderDto> AddServiceProvider(ServiceProviderDto serviceProvider)
        {
            //create service provider role if it doesn't exist
            var spRoleName = RoleName.ServiceProvider;
            var spRole = await _roleManager.FindByNameAsync(spRoleName);
            if (spRole==null)
            {
                var roleCreated = await _roleManager.CreateAsync(new IdentityRole() { Name = spRoleName });
                if(roleCreated.Succeeded)
                {
                    spRole = await _roleManager.FindByNameAsync(spRoleName);
                }
            }
            var sProvider = _mapper.Map<ServiceProvider>(serviceProvider);
            var userRec = new IdentityUser() { Email=serviceProvider.Email , UserName=serviceProvider.Email};
            var userCreated =  await _userManager.CreateAsync(userRec);
            IdentityUser newUser = null;
            int affected = 0;
            if (userCreated.Succeeded)
            {
                newUser = await _userManager.FindByEmailAsync(serviceProvider.Email);
                sProvider.User = newUser;

                await _dbContext.AddAsync(sProvider);
                affected = await _dbContext.SaveChangesAsync();
                var lastAddedServiceProvider = _dbContext.ServiceProviders.OrderByDescending(sp=>sp.Id).FirstOrDefault();
                if(lastAddedServiceProvider!=null && serviceProvider.ImageFile!=null)
                {
                    var imageFileName = $"serviceProvider_{lastAddedServiceProvider.Id}" + Path.GetExtension(serviceProvider.ImageFile.FileName);
                    var imageFilePath = Path.Combine("./wwwroot/images",imageFileName);
                    using(var fStream = new FileStream(imageFilePath,FileMode.Create))
                    {
                       await serviceProvider.ImageFile.CopyToAsync(fStream);
                       lastAddedServiceProvider.ImageUrl = $"images/{imageFileName}";
                    }

                }
                if (spRole != null)
                {
                    await _userManager.AddToRoleAsync(newUser, spRole.Name);
                }

                //add services to the service provider
                serviceProvider.Services.ForEach(sv => {
                    var sourceService = _dbContext.Services.FirstOrDefault(service => service.Id == sv.ServiceId);
                    if(sourceService!=null)
                    {
                        _dbContext.ServiceProviderServices.Add(new ServiceProviderServices { Service = sourceService, ServiceProvider = sProvider });
                    }
                    
                });

                affected = await _dbContext.SaveChangesAsync();
            }
           
           
            return affected > 0 ? _mapper.Map<ServiceProviderDto>(sProvider) : null;
        }

        public async Task<ServiceProviderDto> DeleteServiceProvider(int serviceProviderId)
        {
            var serviceProvider = _dbContext.ServiceProviders.FirstOrDefault(sp => sp.Id == serviceProviderId);
            if (serviceProvider != null) 
            {
                var removedServiceProviderDto = _mapper.Map<ServiceProviderDto>(serviceProvider);
                var spServices = _dbContext.ServiceProviderServices.Where(spsRec => spsRec.ServiceProvider.Id == serviceProvider.Id).ToList();
                spServices.ForEach(spsRec => _dbContext.Remove(spsRec));
                _dbContext.Remove(serviceProvider);
                return await _dbContext.SaveChangesAsync() > 0 ? removedServiceProviderDto : null;
            }
            else
            {
                return null;
            }
        }

        public List<string> GetServiceTimeSlots(int serviceProviderId, int serviceId, string dateString)
        {
            var pattern = new Regex(@"\d{4}\-\d{1,2}\-\d{1,2}");
            if(!pattern.IsMatch(dateString))
            {
                return new List<string>();
            }
            else
            {
                var matchedStr = pattern.Match(dateString).Value;
                var strDigits = matchedStr.Split('-').Select(digitStr => Convert.ToInt32(digitStr)).ToList();
                var bookedDate = new DateTime(strDigits[0], strDigits[1]+1, strDigits[2]);
                var bookedTimeIntervals = _dbContext.Bookings.Where(booking => booking.ServiceProviderId == serviceProviderId && booking.BookedDate.Date == bookedDate.Date).Select(bk=>new TimeComponentInterval { EndTime = bk.BookedTimeInterval.EndTime,StartTime=bk.BookedTimeInterval.StartTime,TimeComponentIntervalId=bk.BookedTimeInterval.TimeComponentIntervalId,WorkingDayId=bk.BookedTimeInterval.WorkingDayId}).ToList();
                var availableSlots = new List<string>();
                foreach(var slot in GetServiceTimeSlots(serviceProviderId, serviceId, bookedDate.DayOfWeek))
                {
                    var tComponet = new TimeComponent(slot);

                    var targetIntervals = bookedTimeIntervals.Where(tI => tComponet.ToMinutes() >= tI.StartTime.ToMinutes() && tComponet.ToMinutes() < tI.EndTime.ToMinutes());
                    if (targetIntervals.Count() == 0)
                    {
                        availableSlots.Add(slot);
                    }
                }
               return availableSlots;
            }

        }
        public  List<string> GetServiceTimeSlots(int serviceProviderId,int serviceId,DayOfWeek dayOfWeek)
        {
            var serviceProvider =_dbContext.ServiceProviders.FirstOrDefault(sP => sP.Id == serviceProviderId);
            var service = _dbContext.Services.FirstOrDefault(service => service.Id == serviceId);
            var targetWorkingDay = _dbContext.WorkingDays
                .Where(wd => wd.ServiceProviderId == serviceProviderId && wd.WeekDay == dayOfWeek)
                .Select(wd => new WorkingDay { Shifts = wd.Shifts.Select(tcI => new TimeComponentInterval(new TimeComponent(tcI.StartTime.Hour, tcI.StartTime.Minute), new TimeComponent(tcI.EndTime.Hour, tcI.EndTime.Minute))).ToList() })
                .FirstOrDefault();
            
            if(service!=null && serviceProvider!=null && targetWorkingDay!=null && targetWorkingDay.Shifts.Count()>0)
            {

                double minuteInterval = _configuration.GetSection("TimeSlotIntervalLength").Get<double>();
                return service.Duration==0? new List<string>():TimeComponentInterval.GenerateTimeComponents(targetWorkingDay.Shifts[0].StartTime.Hour, targetWorkingDay.Shifts[0].EndTime.Hour,minuteInterval ).Select(tc=>tc.ToString()).ToList();
            }
            else
            {
                return new List<string>();
            }
        }

        public async Task<ServiceProviderDto> UpdateServiceProvider(ServiceProviderDto serviceProviderDto)
        {
            ServiceProvider serviceProvider = _mapper.Map<ServiceProvider>(serviceProviderDto);
           
            if(serviceProvider.Id>0)
            {
                _dbContext.Update(serviceProvider);
                var spsRecs = _dbContext.ServiceProviderServices.Where(spsRec=>spsRec.ServiceProvider.Id==serviceProviderDto.ServiceProviderId).Select(spsRec => new ServiceProviderServices { Service=spsRec.Service, ServiceProvider=spsRec.ServiceProvider}).ToList();
                var spsRecsServiceIds = spsRecs.Select(spsRec => spsRec.Service.Id);
                var newServiceIds = serviceProviderDto.Services.Select(sv => sv.ServiceId);
                var unionServiceIds = spsRecsServiceIds.Union(newServiceIds);
                var intersectionServiceIds = spsRecsServiceIds.Intersect(newServiceIds).ToList();
                var removedServiceIds = unionServiceIds.Where(svId => !newServiceIds.Contains(svId)).ToList();
                var addedServiceIds = newServiceIds.Where(svId => !intersectionServiceIds.Contains(svId) && !removedServiceIds.Contains(svId)).ToList();
                removedServiceIds.ForEach(svId => {
                    var removed = _dbContext.ServiceProviderServices.FirstOrDefault(spsRec => spsRec.Service.Id == svId);
                    _dbContext.Remove(removed);
                });

                addedServiceIds.ForEach(svId =>
                {
                    var sourceService = _dbContext.Services.FirstOrDefault(sv => sv.Id == svId);
                    if (sourceService != null)
                    {
                        _dbContext.ServiceProviderServices.Add(new ServiceProviderServices
                        {
                            Service = sourceService,
                            ServiceProvider = serviceProvider
                        });
                    }
                });

                if (serviceProviderDto.ImageFile != null)
                {
                    var fileName = $"serviceProvider_{serviceProvider.Id}{Path.GetExtension(serviceProviderDto.ImageFile.FileName)}";
                    var filePath = Path.Combine("./wwwroot/images", fileName);
                    using(var fs = new FileStream(filePath,FileMode.Create))
                    {
                       await serviceProviderDto.ImageFile.CopyToAsync(fs);
                       serviceProvider.ImageUrl = $"images/{fileName}";
                    }
                }
            }
           
            var affected =  await _dbContext.SaveChangesAsync();
            return affected > 0 ? _mapper.Map<ServiceProviderDto>(serviceProvider) : null;
        }
    }
}
