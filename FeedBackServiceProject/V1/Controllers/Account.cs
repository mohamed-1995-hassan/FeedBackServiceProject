using FeedBackServiceProject.Core.Models;
using FeedBackServiceProject.Infrastructure.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FeedBackServiceProject.Api.V1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Account : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        public SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public Account(UserManager<ApplicationUser> userManager,SignInManager<ApplicationUser> signInManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        [Route("CreateUser")]
        [HttpPost]
        public async  Task<ActionResult> RegisterUser(UserRegistrationModel userRegistrationModel)
        {
            if(ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = userRegistrationModel.UserName,
                    Email = userRegistrationModel.Email,
                    FirstName = userRegistrationModel.FirstName,
                    LastName = userRegistrationModel.LastName,
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                };
                if (_userManager.Users.All(u => u.Id != user.Id))
                {
                    var createUser = await _userManager.FindByEmailAsync(user.Email);
                    if(createUser==null)
                    {
                        IdentityResult identityResult = await _userManager.CreateAsync(user, userRegistrationModel.Password);
                        return Ok(identityResult.Succeeded);
                    }     
                 }
            }
            return BadRequest();
        }

        [Route("Login")]
        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser applicationUser = await _userManager.FindByEmailAsync(loginViewModel.Email);
                var result = await _signInManager.PasswordSignInAsync(applicationUser, loginViewModel.Password, isPersistent: false, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    //user role list here
                    var roles = await _userManager.GetRolesAsync(applicationUser);
                    if (roles.Count>0)
                    {
                        string role = roles.FirstOrDefault();
                        if (role.Equals("Admin"))
                        {
                            return Ok(new { Role = "Admin Login" });
                        }
                        else if (role.Equals("User"))
                        {
                            return Ok(new { Role = "User Login" });
                        }
                    }
                    else
                        return Ok(new { Role = "Anonymus User" });
                }
            }
            return BadRequest();
        }
        [HttpPost]
        [Route("CreateRole")]
        public async Task<ActionResult> CreateRole(string roleName)
        {
            bool CheckForRole = await _roleManager.RoleExistsAsync(roleName);
            if (!CheckForRole)
            {
                var role = new IdentityRole
                {
                    Name = roleName
                };
                IdentityResult identityResult = await _roleManager.CreateAsync(role);
                if (identityResult.Succeeded)
                {
                   return Ok("Rule Created Successfully");
                }
                else
                {
                    return Ok("Error in Adding the rule");
                }
            }
            else
            {
                return Ok(new { RoleStatus = "Rule already defined in the system" });
            }
        }
        [HttpPost]
        [Route("DeleteRole")]
        public async Task<ActionResult> DeleteRole(string role)
        {
            var checkRole = _roleManager.Roles.Where(d => d.Name == role).FirstOrDefault();
            if (checkRole != null)
            {
                await _roleManager.DeleteAsync(checkRole);
                return Ok(new {Result="Role Deleted"});
            }
            else
            {
               return Ok(new { Result = "not Fount Role" });
            }

        }
        [HttpPost]
        [Route("AssignUserToRole")]
        public async Task<ActionResult> AssignUserToRole(string UserEmail, string RoleName)
        {
            ApplicationUser applicationUser = await _userManager.FindByEmailAsync(UserEmail);
            bool CheckForRole = await _roleManager.RoleExistsAsync(RoleName);
            if (CheckForRole && applicationUser != null)
            {
                var result1 = await _userManager.AddToRoleAsync(applicationUser, RoleName);
                return Ok(new { Result = "Role assigned" });
            }
            else
            {
                return Ok(new { Result = "failed to assign" });
            }

        }
        [HttpPost]
        [Route("DeleteUserFromRole")]
        public async Task<ActionResult> DeleteUserFromRole(string UserEmail, string RoleName)
        {
            ApplicationUser applicationUser = await _userManager.FindByEmailAsync(UserEmail);
            bool CheckForRole = await _roleManager.RoleExistsAsync(RoleName);
            if (CheckForRole && applicationUser != null)
            {
                IdentityResult deletionResult = await _userManager.RemoveFromRoleAsync(applicationUser, RoleName);
                if(deletionResult.Succeeded)
                return Ok(new { Result = "User deleted from role" });
                return Ok(new { Result = "User not found" });
            }
            else
            {
                return Ok(new { Result = "failed to deleted" });
            }
        }

        [HttpGet]
        [Route("GetAllRoles")]
        public async Task<ActionResult> GetAllUSerRoles(string UserId)
        {
            var user = await _userManager.FindByIdAsync(UserId);
            var roles = await _userManager.GetRolesAsync(user);
            return Ok(new { User = user, Roles = roles });
        }
        [HttpGet]
        [Route("CheckForAdmin")]
        [Authorize(Roles ="Admin")]
        
        public  ActionResult CheckForAdmin()
        {
            return Ok(new { Message = "Admin Authorize" });
        }
        [HttpGet]
        [Route("CheckForUser")]
        [Authorize(Roles = "User")]
        public  ActionResult CheckForUser()
        {
            return Ok(new { Message = "Admin Authorize" });
        }
    }
}

