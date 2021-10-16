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
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;

        public  AccountController(AccountService accountService)
        {
            _accountService = accountService;
        }
        // GET: api/<AccountController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<AccountController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<AccountController>
        [HttpPost]
        public async Task<SysResult<bool>> Post([FromForm] ClientInfoCreateDTO value) => await _accountService.CreateAccount(value);

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost("signIn")]
        public async Task<bool> Post() {
            _ = Request.Form.TryGetValue("userName", out var userName);
            _ =  Request.Form.TryGetValue("password", out var password);
          
            return await _accountService.SignIn(userName,password);
        }
        
    }
}
