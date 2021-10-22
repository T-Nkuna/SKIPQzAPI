using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SKIPQzAPI.Models.Time;

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
            var slots = TimeComponentInterval.GenerateTimeComponents(new TimeComponent(6,30), new TimeComponent(19,23), 15);
            var timeOfDay = TimeComponentInterval.ClassifyHour(9);
            var timeOfDay2 = TimeComponentInterval.ClassifyTime(tc);
            var timeOfDay3 = TimeComponentInterval.ClassifyTime(new TimeComponent(19, 0));
            var timeOfDay4 = TimeComponentInterval.ClassifyTime(new TimeComponent(16, 30));
            return slots.Select(ts=>ts.ToString()).ToList();
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
        [HttpPost("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<HomeController>/5
        [HttpPost("{id}")]
        public void Delete(int id)
        {
        }
    }
}
