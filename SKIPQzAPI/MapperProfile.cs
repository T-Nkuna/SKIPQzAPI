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

namespace SKIPQzAPI
{
    public class MapperProfile:Profile
    {
      
        public MapperProfile()
        {
            var dbContextOptionBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
       
            
            CreateMap<ServiceProvider, ServiceProviderDto>()
                .ForMember(dest=>dest.Email,op=>op.MapFrom<ServiceProviderDtoEmailResolver>())
                .ForMember(dest=>dest.Name,op=>op.MapFrom<ServiceProviderDtoNameResolver>());
            CreateMap<ServiceProviderDto, ServiceProvider>();

            CreateMap<ServiceDto, Service>();
            CreateMap<Service, ServiceDto>();
            
        }
    }

   public class ServiceProviderDtoEmailResolver : IValueResolver<ServiceProvider, ServiceProviderDto, string>
    {
        private readonly ApplicationDbContext _dbContext;
        public ServiceProviderDtoEmailResolver(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public string Resolve(ServiceProvider source, ServiceProviderDto destination, string destMember, ResolutionContext context)
        {
            var spUser = _dbContext.ServiceProviders.Where(
                sp => sp.ServiceProviderId == source.ServiceProviderId
                 ).Select(sp=>sp.User).ToList();
            return spUser.Count()>0?spUser.ElementAt(0).Email:"";
        }
    }

    public class ServiceProviderDtoNameResolver : IValueResolver<ServiceProvider, ServiceProviderDto, string>
    {
        private readonly ApplicationDbContext _dbContext;
        public ServiceProviderDtoNameResolver(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public string Resolve(ServiceProvider source, ServiceProviderDto destination, string destMember, ResolutionContext context)
        {
            var spUser = _dbContext.ServiceProviders.Where(
                sp => sp.ServiceProviderId == source.ServiceProviderId
                 ).Select(sp=>sp.User).ToList();
            return spUser.Count()>0 ? spUser.ElementAt(0).UserName: "";
        }
    }
}
