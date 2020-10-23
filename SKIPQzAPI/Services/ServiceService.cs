using AutoMapper;
using SKIPQzAPI.DataAccess;
using SKIPQzAPI.Dtos;
using SKIPQzAPI.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SKIPQzAPI.Services
{
    public class ServiceService
    {
        private readonly IMapper _mapper;
        private readonly ApplicationDbContext _dbContext ;
        public ServiceService(IMapper mapper,ApplicationDbContext dbConext)
        {
            _mapper = mapper;
            _dbContext = dbConext;
        }

        public async Task<ServiceDto> AddService(ServiceDto service)
        {
            try
            {
                var addedService = _mapper.Map<Service>(service);
                await _dbContext.AddAsync(addedService);
                var affected = await _dbContext.SaveChangesAsync();
                var lastAddedService = _dbContext.Services.OrderByDescending(s => s.ServiceId).FirstOrDefault();
                var imageFileName = $"service_{lastAddedService.ServiceId}" + Path.GetExtension(service.ImageFile.FileName);
                var filePath = Path.Combine(@".\wwwroot\images", imageFileName);

                if (lastAddedService != null)
                {
                    lastAddedService.ImageUrl = $"images/{imageFileName}";
                    affected = await _dbContext.SaveChangesAsync();
                }
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await service.ImageFile.CopyToAsync(stream);

                }

                return affected > 0 ? _mapper.Map<ServiceDto>(lastAddedService) : null;
            }
            catch(Exception ex)
            {
                return new ServiceDto() { Name=ex.Message+"\n"+ex.StackTrace};
            }
            
        }

        public IEnumerable<ServiceDto> GetServices(int pageIndex,int pageSize)
        {
            return _dbContext.Services
                .OrderByDescending(s=>s.ServiceId)
                .Skip(pageIndex * pageSize)
                .Take(pageSize)
                .ToList()
                .Select(service=>_mapper.Map<ServiceDto>(service));
        }

        public ServiceDto GetService(int serviceId)
        {
            var targetService = _dbContext.Services.FirstOrDefault(s => s.ServiceId == serviceId);
            return targetService != null ? _mapper.Map<ServiceDto>(targetService) : null;
        }

        public async Task<ServiceDto> DeleteService(int serviceId)
        {
            var removed = _dbContext.Services.FirstOrDefault(service => service.ServiceId == serviceId);
            ServiceDto returned = null;
            var removedSuccessfully = 0;
            if (removed != null)
            {
                returned = _mapper.Map<ServiceDto>(removed);
                _dbContext.Remove(removed);
                removedSuccessfully = await _dbContext.SaveChangesAsync();
            }

            return removedSuccessfully>0 ? returned : null;
        }

        public async Task<int> AddServiceProvider(int serviceId,int serviceProviderId)
        {
            var targetService = _dbContext.Services.FirstOrDefault(s => s.ServiceId == serviceId);
            var targetProvider = _dbContext.ServiceProviders.FirstOrDefault(sp => sp.ServiceProviderId == serviceProviderId);
            
            if(targetProvider!=null && targetService != null)
            {
                ServiceProviderServices spsRec = new ServiceProviderServices { Service = targetService, ServiceProvider = targetProvider };
                _dbContext.Add(spsRec);
                return await _dbContext.SaveChangesAsync();
            }
            else
            {
                return 0;
            }

        }

        public IEnumerable<ServiceProviderDto> GetServiceProviders(int serviceId,int pageIndex,int pageSize)
        {
            return _dbContext.ServiceProviderServices
                .OrderByDescending(spsRec=>spsRec.ServiceProvider.ServiceProviderId)
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .Where(spsRec => spsRec.Service.ServiceId == serviceId)
                .Select(spsRec => spsRec.ServiceProvider)
                .ToList()
                .Select(sp => _mapper.Map<ServiceProviderDto>(sp));
        }

    }
}
