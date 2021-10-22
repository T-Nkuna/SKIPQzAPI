using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
        private readonly HttpClient _httpClient;

        public BookingController(BookingService bookingService,IHttpClientFactory httpClientFactory)
        {
            _bookingService = bookingService;
            _httpClient = httpClientFactory.CreateClient();
           
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

        [HttpGet("UserBookings")]
        public IEnumerable<BookingDto> BookingsPerUser(string userName) => _bookingService.BookingsPerUser(userName);

       
    }
}
