using Microsoft.AspNetCore.Mvc;
using SKIPQzAPI.Dtos;
using SKIPQzAPI.Models;
using SKIPQzAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SKIPQzAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrganisationController : ControllerBase
    {
        private readonly OrganisationService _orgService;
        public OrganisationController(OrganisationService orgService)
        {
            _orgService = orgService;
        }
        // GET: api/<OrganisationController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<OrganisationController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<OrganisationController>
        [HttpPost]
        public  async Task<SysResult<long?>> Post([FromBody] OrganisationCreateDto org) => await _orgService.AddOrganisation(org);

        // PUT api/<OrganisationController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrganisationController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
