using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using SKIPQzAPI.Dtos;
using SKIPQzAPI.Integration.PayGate;
using SKIPQzAPI.Models;
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
            var (booking,payment,transactUrl) = await _bookingService.AddBooking(value);
            if (payment != null)
            {

               var paymentResponse = await payment.ProcessPaymentRequest();

            }
            return booking;
        }

        [HttpGet("UserBookings")]
        public IEnumerable<BookingDto> BookingsPerUser(string userName) => _bookingService.BookingsPerUser(userName);

        [HttpPost("CancelUserBooking")]
        public SysResult<long?> CancelUserBooking([FromBody] CancelBookingRequestDto cancelRequest) => _bookingService.CancelUserBooking(cancelRequest.UserName, cancelRequest.BookingId);
       
    }
}
