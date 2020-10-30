using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SKIPQzAPI.Dtos;
using SKIPQzAPI.Models.Time;
using SKIPQzAPI.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SKIPQzAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : Controller
    {
        private readonly BookingService _bookingService;

        public BookingController(BookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // GET api/<BookingController>/5
        [HttpGet]
        public  List<BookingDto> Get(int pageSize,int pageIndex)
        {
            return _bookingService.GetBookings(pageIndex,pageSize);
        }

        // POST api/<BookingController>
        [HttpPost]
        public async Task<BookingDto> Post([FromBody] BookingDto value)
        {
            return await _bookingService.AddBooking(value);
        }

        // PUT api/<BookingController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<BookingController>/5
        [HttpDelete("{id}")]
        public async Task<BookingDto> Delete(int id)
        {
            return await _bookingService.DeleteBooking(id);
        }
    }
}
