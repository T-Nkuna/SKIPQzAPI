using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SKIPQzAPI.Dtos;
using SKIPQzAPI.Models.Time;
using SKIPQzAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SKIPQzAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceProviderController : Controller
    {
        private readonly ServiceProviderService _serviceProviderService;
        public ServiceProviderController(ServiceProviderService serviceProviderService)
        {
            _serviceProviderService = serviceProviderService;
        }
        // GET: api/<ServiceProviderController>/?pageIndex=&pageSize=
        [HttpGet]
        public IEnumerable<ServiceProviderDto> Get(int pageIndex,int pageSize)
        {
            return _serviceProviderService.GetServiceProviders(pageSize, pageIndex);
        }

        // GET api/<ServiceProviderController>/5
        [HttpGet("{id}")]
        public ServiceProviderDto Get(int id)
        {
            return _serviceProviderService.GetServiceProvider(id);
        }

        // POST api/<ServiceProviderController>
        [HttpPost]
        public async Task<ServiceProviderDto> Post()
        {
            var value = Request.Form;
            var sp = new ServiceProviderDto {
                Name = value["name"],
                Email = value["email"],
                ScheduledWorkDays = JsonConvert.DeserializeObject<List<WorkingDayDto>>(value["scheduledWorkDays"]),
                Services = JsonConvert.DeserializeObject<List<ServiceDto>>(value["services"]),
                ImageFile = value.Files["imageFile"]
            };
           return await _serviceProviderService.AddServiceProvider(sp);
        }

        // PUT api/<ServiceProviderController>/5
        [HttpPost("Update")]
        public async Task<ServiceProviderDto> Put()
        {
            
            var form = Request.Form;
            int serviceProviderId;
            int.TryParse(form["serviceProviderId"], out serviceProviderId);
            var sProviderDto = new ServiceProviderDto { 
                Email = form["email"],
                ServiceProviderId = serviceProviderId,
                ImageFile = form.Files["imageFile"],
                ScheduledWorkDays = JsonConvert.DeserializeObject<List<WorkingDayDto>>(form["scheduledWorkDays"]),
                Name = form["name"],
                Services = JsonConvert.DeserializeObject<List<ServiceDto>>(form["services"]),
                ImageUrl = form["imageUrl"]
            };
            return await _serviceProviderService.UpdateServiceProvider(sProviderDto);
        }

        // DELETE api/<ServiceProviderController>/5
        [HttpPost("{id}")]
        public async Task<ServiceProviderDto> Delete(int id)
        {
            return await _serviceProviderService.DeleteServiceProvider(id);
        }

        [HttpGet("{id}/services/{serviceId}/{dateString}")]
        public List<string> GetServiceProviderTimeSlots(int id,int serviceId,string dateString)
        {

            try
            {
                return _serviceProviderService.GetServiceTimeSlots(id, serviceId, dateString);
            }
            catch(Exception ex)
            {
                return new List<string> { ex.Message, ex.StackTrace };
            }
            
        }
    }
}
