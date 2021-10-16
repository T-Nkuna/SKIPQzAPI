using AutoMapper;
using Microsoft.AspNetCore.Identity;
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
        public AccountService(IMapper mapper, ApplicationDbContext dbContext, UserManager<IdentityUser> userManager,RoleManager<IdentityRole> roleManager,SignInManager<IdentityUser> signInManager)
        {
            _mapper = mapper;
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
            _singInManager = signInManager;
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
                throw new ArgumentException("Invalid Credentials");
            }
        }

        public async Task<bool> CreateAccount(ClientInfoCreateDTO accountDetails)
        {

            var existingUser = await _userManager.FindByNameAsync(accountDetails.Email);
            var accountExists = (existingUser!=null);
            accountExists = accountExists && (await _userManager.GetRolesAsync(existingUser)).Any(rName=>rName==RoleName.Client);
            var passwordMatch = accountDetails.Password == accountDetails.ConfirmPassword;
            if (!passwordMatch)
            {
                throw new ArgumentException("Password Mismatch");
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
                            throw new ArgumentException($"Failed To Create Role: {roleCreateResult.Errors.ElementAt(0)?.Description}");
                        }
                    }
                    var addedToRoleResult = (await _userManager.AddToRoleAsync(newCreatedUser, RoleName.Client));
                    return addedToRoleResult.Succeeded;
                }
                else
                {
                    throw new ArgumentException($"Failed To Create User: {createResult.Errors.ElementAt(0)?.Description}");
                }
            }
            else
            {
                throw new ArgumentException($"Account {accountDetails.Email} Already Exists");
            }

        }
    }
}
