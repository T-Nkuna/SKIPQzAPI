using Microsoft.AspNetCore.Mvc;
using SKIPQzAPI.Dtos;
using SKIPQzAPI.Models;
using SKIPQzAPI.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SKIPQzAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly AccountService _accountService;
        private static string _tokenFileName = "token.txt";
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

        [HttpGet("ResetPassword")]
        public ContentResult GetResetPasswordLink([FromQuery] string userid, [FromQuery] string code)
        {
            var passwordResetUrl = Url.Action(nameof(ResetUserPassword), "Account", new { code, userid }, Request.Scheme, Request.Host.ToString());
            var html = $"<form method=\"post\" action=\"{passwordResetUrl}\">" +
                   $"New Password: <input name=\"newPassword\"\\><br\\>" +
                   $"<button type=\"submit\">Submit</button>" +
                   $"</form>";
            
            return base.Content(html, "text/html");
        }

        [HttpPost("ResetUserPassword")]
        public async Task<ContentResult> ResetUserPassword([FromQuery] string userid, [FromQuery] string code,[FromForm] string newPassword)
        {
            return base.Content(((await _accountService.ResetPassword(userid,code,newPassword))?.Message + $"<br/><a href=\"https://skipqzclient.growthlytix.co.za\">Go to login</a>"),"text/html");
        }

        // POST api/<AccountController>
        [HttpPost]
        public async Task<SysResult<bool>> Post([FromForm] ClientInfoCreateDTO value) => await _accountService.CreateAccount(value);


        [HttpPost("signIn")]
        public async Task<bool> Post()
        {
            _ = Request.Form.TryGetValue("userName", out var userName);
            _ = Request.Form.TryGetValue("password", out var password);
            return await _accountService.SignIn(userName, password);
        }
        
        [HttpPost("forgotPassword/{userName}")]
        public async Task<SysResult<bool>> ForgotPassword(string userName)
        {
            
            var resetPasswordLinkParams = await _accountService.ResetPasswordLinkParams(userName);
            var resetLink = Url.Action(nameof(GetResetPasswordLink), "Account", new { code = resetPasswordLinkParams.code, userid = resetPasswordLinkParams.userID }, Request.Scheme, Request.Host.ToString());// $"{Request.Scheme}://{Request.Host}/api/Account/ResetPassword?code={resetPasswordLinkParams.code}&userid={resetPasswordLinkParams.userID}";
            if (!string.IsNullOrEmpty(resetPasswordLinkParams.email))
            {
                await _accountService.MailResetPasswordLink(resetLink, resetPasswordLinkParams.email);
                return new SysResult<bool> { Data = true, Ok = true, Message = $"Reset Link Has Been Sent To {resetPasswordLinkParams.email}" };
            }
            else
            {
                return new SysResult<bool> { Data = false, Ok = false, Message = $"Failed To Send Reset Link To Account {resetPasswordLinkParams.email}" };
            }
           
        }

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

        
        
    }
}
