using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SKIPQzAPI.Dtos;
using SKIPQzAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SKIPQzAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : Controller
    {
        private readonly ServiceService _servicesService;
        public ServiceController(ServiceService servicesService)
        {
            _servicesService = servicesService;
        }
        // GET: api/<ServiceController>
        [HttpGet]
        public IEnumerable<ServiceDto> Get(int pageSize,int pageIndex)
        {
            return _servicesService.GetServices(pageIndex,pageSize);
        }

        // GET api/<ServiceController>/5
        [HttpGet("{id}")]
        public ServiceDto Get(int id)
        {
            return _servicesService.GetService(id);
        }

        // POST api/<ServiceController>
        [HttpPost]
        public async Task<ServiceDto> Post()
        {
            var value = Request.Form;
            var service = new ServiceDto();
            service.Cost = Convert.ToDecimal(value["cost"]);
            service.Duration = Convert.ToDouble(value["duration"]);
            service.Name = value["name"];
            service.ImageFile = value.Files["imageFile"];
            service.ExtraIds = value.ContainsKey("extraIds") ? JsonConvert.DeserializeObject<List<long?>>(value["extraIds"]) : new List<long?>();
            return  await _servicesService.AddService(service);
        }

        // PUT api/<ServiceController>/5
        [HttpPost("Update")]
        public async Task<ServiceDto> Put()
        {
            IFormCollection form = Request.Form;
            ServiceDto serviceDTO = new ServiceDto
            {
                Cost = Convert.ToDecimal(form["cost"]),
                Duration = Convert.ToDouble(form["duration"]),
                ImageFile = form.Files["imageFile"],
                Name = form["name"],
                ServiceId = Convert.ToInt32(form["serviceId"]),
                ExtraIds = form.ContainsKey("extraIds") ? JsonConvert.DeserializeObject<List<long?>>(form["extraIds"]) : new List<long?>(),
                ImageUrl = form["imageUrl"]
            };
            return await _servicesService.UpdateService(serviceDTO);
        }

        // DELETE api/<ServiceController>/5
        [HttpPost("{id}")]
        public Task<ServiceDto> Delete(int id)
        {
            return _servicesService.DeleteService(id);
        }

        [HttpPost("{id}/addProvider")]
        public async Task<int> AddProvider(long? id,[FromBody] ServiceProviderDto serviceProviderDto)
        {
            return await _servicesService.AddServiceProvider(id, serviceProviderDto.ServiceProviderId);
        }

        [HttpGet("{id}/providers")]

        public IEnumerable<ServiceProviderDto> GetServiceProviders(int id,int pageIndex,int pageSize)
        {
            return _servicesService.GetServiceProviders(id, pageIndex, pageSize);
        }
    }
}
