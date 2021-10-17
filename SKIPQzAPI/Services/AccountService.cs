using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NETCore.MailKit.Core;
using SKIPQzAPI.Common.Constants;
using SKIPQzAPI.DataAccess;
using SKIPQzAPI.Dtos;
using SKIPQzAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SKIPQzAPI.Services
{
    public class AccountService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<IdentityUser> _singInManager;
        private readonly IEmailService _mailService;
        public AccountService(IMapper mapper, ApplicationDbContext dbContext, 
            UserManager<IdentityUser> userManager,RoleManager<IdentityRole> roleManager,
            SignInManager<IdentityUser> signInManager,
            IEmailService mailService)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _singInManager = signInManager;
            _mailService = mailService;
        }


        public async Task< (string code,string userID,string email)> ResetPasswordLinkParams(string userName)
        {
            var targetUser =  await _userManager.FindByNameAsync(userName);
            if (targetUser != null)
            {
                var code = await _userManager.GeneratePasswordResetTokenAsync(targetUser);
                var userId = targetUser.Id;
                return (code, userId,targetUser.Email);
            }
            else
            {
                return ("","","");
            }
        }

        public async Task MailResetPasswordLink(string link,string mailTo)
        {
            await _mailService.SendAsync(mailTo, "SkipQz Reset Password", $"Click <a href=\"{link}\">Here</a> To Reset Password", true);
        }

        public async Task<SysResult<bool>> ResetPassword(string userid,string code,string newPassword)
        {
            var user = await _userManager.FindByIdAsync(userid);
            if (user != null)
            {
                var resetResult = (await _userManager.ResetPasswordAsync(user, code, newPassword));
                if (resetResult.Succeeded)
                {
                    return new SysResult<bool> { Data = true, Ok = true, Message = "Password Reset Successful" };
                }
                else
                {
                    return new SysResult<bool> { Data = false, Ok = false, Message = (resetResult.Errors.ElementAt(0)?.Description) };
                }

                
            }
            else
            {
                return new SysResult<bool> { Data = true, Ok = true, Message = "Password Reset Failed (Invalid Account)" }; 
            }
        }
        public async Task<bool> SignIn(string userName,string password)
        {
            var isValidUserClaim = new List<Claim> { new Claim(ClaimTypes.Name, userName), new Claim(ClaimTypes.Role, RoleName.Client) };
            var claimIdentity = new ClaimsIdentity(isValidUserClaim);
           var existingUser =  await _userManager.FindByNameAsync(userName);
            if (existingUser != null && (await _userManager.GetRolesAsync(existingUser)).Any(rName => rName == RoleName.Client))
            {
                var signInResult = await _singInManager.CheckPasswordSignInAsync(existingUser, password, false);
                return signInResult.Succeeded;

            }
            else
            {
               return false;
            }
        }

        public async Task<SysResult<bool>> CreateAccount(ClientInfoCreateDTO accountDetails)
        {

            var existingUser = await _userManager.FindByNameAsync(accountDetails.Email);
            var accountExists = (existingUser!=null);
            accountExists = accountExists && (await _userManager.GetRolesAsync(existingUser)).Any(rName=>rName==RoleName.Client);
            var passwordMatch = accountDetails.Password == accountDetails.ConfirmPassword;
            if (!passwordMatch)
            {
                return new SysResult<bool> { Data = false, Message = "Password Mismatch",Ok=false };
            }
            if (!accountExists)
            {
                var clientInfo = _mapper.Map<ClientInfoDTO>(accountDetails);
                var createResult = await _userManager.CreateAsync(new IdentityUser { UserName = clientInfo.Email, PhoneNumber = clientInfo.PhoneNumber, Email = clientInfo.Email}, accountDetails.Password);
                if (createResult.Succeeded)
                {
                    var newCreatedUser = await _userManager.FindByNameAsync(clientInfo.Email);
                    var newAccountDetails = _mapper.Map<ClientInfo>(clientInfo);
                    newAccountDetails.User = newCreatedUser;
                    var clientRole =  await _roleManager.FindByNameAsync(RoleName.Client);
                    if(clientRole == null)
                    {
                        var roleCreateResult = await _roleManager.CreateAsync(new IdentityRole { Name = RoleName.Client });
                        if (!roleCreateResult.Succeeded)
                        {
                            return new SysResult<bool> { Data = false, Ok = false, Message = $"Failed To Create Role: {roleCreateResult.Errors.ElementAt(0)?.Description}" };
                          
                        }
                    }
                    var addedToRoleResult = (await _userManager.AddToRoleAsync(newCreatedUser, RoleName.Client));
                    return new SysResult<bool> { Data = addedToRoleResult.Succeeded,Ok=addedToRoleResult.Succeeded,Message="Account Created"};
                }
                else
                {
                    return new SysResult<bool> { Message = $"Failed To Create User: {createResult.Errors.ElementAt(0)?.Description}",Data=false,Ok=false};
                }
            }
            else
            {
               return new SysResult<bool> { Message = ($"Account {accountDetails.Email} Already Exists"), Data = false, Ok = false };
            }

        }
    }
}
