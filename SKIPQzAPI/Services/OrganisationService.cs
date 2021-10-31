using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SKIPQzAPI.Common.Constants;
using SKIPQzAPI.DataAccess;
using SKIPQzAPI.Dtos;
using SKIPQzAPI.Models;
using SKIPQzAPI.Repository.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SKIPQzAPI.Services
{
    public class OrganisationService
    {
        private readonly OrganisationRepository _orgRepo;
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public OrganisationService(ApplicationDbContext dbContext, IMapper mapper, UserManager<IdentityUser> uManager,RoleManager<IdentityRole> rManager)
        {
            _dbContext = dbContext;
            _orgRepo = new OrganisationRepository(dbContext);
            _mapper = mapper;
            _userManager = uManager;
            _roleManager = rManager;
        }

        public async Task<SysResult<long?>> AddOrganisation(OrganisationCreateDto orgDto)
        {
            if (orgDto.ConfirmPassword != orgDto.Password)
            {
                return new SysResult<long?> { Data = 0, Ok = false, Message = "Password Mismatch" };
            }

            var org = _mapper.Map<Organisation>(orgDto);
            var newUser = new IdentityUser();
            newUser.Email = orgDto.Email;
            newUser.PhoneNumber = orgDto.PhoneNumber;
            newUser.UserName = newUser.Email;

            var result = await _userManager.CreateAsync(newUser,orgDto.Password);
            if (result.Succeeded)
            {
                if(! await _roleManager.RoleExistsAsync(RoleName.Organisation))
                {
                    var newRole = new IdentityRole(RoleName.Organisation);
                   await _roleManager.CreateAsync(newRole);
                  
                }
              
                org.User = await _userManager.FindByNameAsync(newUser.UserName);
                await _userManager.AddToRoleAsync(org.User, RoleName.Organisation);
                var affectedCount = _orgRepo.Add(org,null,"");
                return new SysResult<long?> { Data = org.Id, Ok = affectedCount > 0, Message = affectedCount > 0 ? "Organisation Created!" : "Failed To Create Organisation" };
            }
            else
            {
                return new SysResult<long?> { Data = 0, Ok = false, Message = result.Errors?.Count() > 0 ? result.Errors.ElementAt(0).Description : "Unkown Error Occured" };
            }
        }
    }
}
