using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SKIPQzAPI.DataAccess;
using SKIPQzAPI.Dtos;
using SKIPQzAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKIPQzAPI.Services
{
    public class ServiceProviderService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public ServiceProviderService(IMapper mapper,
            ApplicationDbContext dbContext,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public ServiceProviderDto GetServiceProvider(int serviceProviderId)
        {
           var serviceProvider=  _dbContext.ServiceProviders.FirstOrDefault(sP => sP.ServiceProviderId == serviceProviderId);
            return _mapper.Map<ServiceProviderDto>(serviceProvider);
        }

        public IEnumerable<ServiceProviderDto> GetServiceProviders(int serviceId,int pageSize,int pageIndex=0)
        {
           return _dbContext.ServiceProviderServices
                .OrderByDescending(spsRec => spsRec.ServiceProvider.ServiceProviderId)
                .Where(rec => rec.Service.ServiceId == serviceId)
                .Skip(pageIndex*pageSize)
                .Take(pageSize)
                .Select(spS=>spS.ServiceProvider)
                .ToList()
                .Select(sP=>_mapper.Map<ServiceProviderDto>(sP));
        }

        public IEnumerable<ServiceProviderDto> GetServiceProviders(int pageSize, int pageIndex = 0)
        {
            return _dbContext.ServiceProviders
                 .OrderByDescending(sp=>sp.ServiceProviderId)
                 .Skip(pageIndex * pageSize)
                 .Take(pageSize)
                 .ToList()
                 .Select(sP => _mapper.Map<ServiceProviderDto>(sP));
        }

        public async Task<ServiceProviderDto> AddServiceProvider(ServiceProviderDto serviceProvider)
        {
            //create service provider role if it doesn't exist
            var spRoleName = "ServiceProvider";
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
               
                if (spRole != null)
                {
                    await _userManager.AddToRoleAsync(newUser, spRole.Name);
                }

                //add services to the service provider
                serviceProvider.Services.ForEach(sv => {
                    var sourceService = _dbContext.Services.FirstOrDefault(service => service.ServiceId == sv.ServiceId);
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
            var serviceProvider = _dbContext.ServiceProviders.FirstOrDefault(sp => sp.ServiceProviderId == serviceProviderId);
            if (serviceProvider != null) 
            {
                var removedServiceProviderDto = _mapper.Map<ServiceProviderDto>(serviceProvider);
                _dbContext.Remove(serviceProvider);
                return await _dbContext.SaveChangesAsync() > 0 ? removedServiceProviderDto : null;
            }
            else
            {
                return null;
            }
        }
    }
}
