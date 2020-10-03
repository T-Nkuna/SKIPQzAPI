using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SKIPQzAPI.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SKIPQzAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : Controller
    {
        // GET: api/<HomeController>
        [HttpGet]
        public List<string> Get()
        {
            TimeComponent tc = new TimeComponent(12, 30);
            var tSlot = new TimeSlot();
            var slots = TimeComponentInterval.GenerateTimeComponents(DayTimeInterVal.Evening.StartHours, DayTimeInterVal.Evening.EndHours, 15);
            var timeOfDay = TimeComponentInterval.ClassifyHour(9);
            return TimeComponentInterval.GenerateTimeComponents(timeOfDay,15).Select(ts=>ts.ToString()).ToList();
         }

        // GET api/<HomeController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<HomeController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<HomeController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<HomeController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
