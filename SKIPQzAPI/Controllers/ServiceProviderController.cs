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
    public class ServiceProviderController : ControllerBase
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
        public async Task<ServiceProviderDto> Post([FromBody] ServiceProviderDto value)
        {
           return await _serviceProviderService.AddServiceProvider(value);
        }

        // PUT api/<ServiceProviderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ServiceProviderController>/5
        [HttpDelete("{id}")]
        public async Task<ServiceProviderDto> Delete(int id)
        {
            return await _serviceProviderService.DeleteServiceProvider(id);
        }
    }
}