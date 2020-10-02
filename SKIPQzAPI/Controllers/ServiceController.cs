﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SKIPQzAPI.Dtos;
using SKIPQzAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SKIPQzAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
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
        public async Task<ServiceDto> Post([FromBody] ServiceDto value)
        {
            return  await _servicesService.AddService(value);
        }

        // PUT api/<ServiceController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ServiceController>/5
        [HttpDelete("{id}")]
        public Task<ServiceDto> Delete(int id)
        {
            return _servicesService.DeleteService(id);
        }

        [HttpPost("{id}/addProvider")]
        public async Task<int> AddProvider(int id,[FromBody] ServiceProviderDto serviceProviderDto)
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
