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
    public class ExtraController : ControllerBase
    {
        private readonly ExtraService _extraService;

        public ExtraController(ExtraService extraService)
        {
            _extraService = extraService;
        }

        // GET: api/<ExtraController>
        [HttpGet]
        public IEnumerable<ExtraDto> Get(int pageSize, int pageIndex)
        {
            return _extraService.GetExtras(pageIndex,pageSize);
        }

        // GET api/<ExtraController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ExtraController>
        [HttpPost]
        public async Task<ExtraDto> Post([FromBody] ExtraDto value)
        {
            return await _extraService.AddExtra(value);
        }

        // PUT api/<ExtraController>/5
        [HttpPut]
        public async Task<ExtraDto> Put([FromBody] ExtraDto value)
        {
            return await _extraService.UpdateExtra(value);
        }

        // DELETE api/<ExtraController>/5
        [HttpDelete("{id}")]
        public async Task<ExtraDto> Delete(int id)
        {
            return await _extraService.DeleteExtra(id);
        }
    }
}