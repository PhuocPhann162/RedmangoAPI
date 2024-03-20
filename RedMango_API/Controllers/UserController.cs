using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RedMango_API.Data;
using RedMango_API.Models;
using RedMango_API.Utility;
using System.Net;

namespace RedMango_API.Controllers
{
    [Route("api/User")]
    [ApiController]
    [Authorize(Roles = SD.Role_Admin)]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _db;
        protected ApiResponse _response;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public UserController(ApplicationDbContext db, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _response = new ApiResponse();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            try
            {
                List<ApplicationUser> lstUsers = await _db.ApplicationUsers.ToListAsync();

                if (lstUsers == null)
                {
                    _response.Result = new List<ApplicationUser>();
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Something wrong when get all users");
                    return BadRequest(_response);
                }

                foreach (var user in lstUsers)
                {
                    user.Role = _userManager.GetRolesAsync(user).GetAwaiter().GetResult().FirstOrDefault();
                }

                _response.Result = lstUsers;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.Result = new List<ApplicationUser>();
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add(ex.Message);
            }
            return BadRequest(_response);
        }


        [HttpPost("lockUnlock/{id}")]
        public async Task<IActionResult> LockUnlock([FromBody]string? id)
        {
            try
            {
                var userFromDb = await _db.Users.FirstOrDefaultAsync(u => u.Id == id);
                if (userFromDb == null)
                {
                    _response.IsSuccess = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.ErrorMessages.Add("Errors while lock/unlock this user");
                    return BadRequest(_response);
                }
                if (userFromDb.LockoutEnd != null && userFromDb.LockoutEnd > DateTime.Now)
                {
                    // user is currently locked and we need to unlock them 
                    userFromDb.LockoutEnd = DateTime.Now;
                    _db.ApplicationUsers.Update(userFromDb);
                    await _db.SaveChangesAsync();
                    _response.StatusCode = HttpStatusCode.OK;
                    _response.Result = "Unlock this user successfully";
                }
                else
                {
                    userFromDb.LockoutEnd = DateTime.Now.AddYears(1000);
                    _db.ApplicationUsers.Update(userFromDb);
                    await _db.SaveChangesAsync();
                    _response.StatusCode = HttpStatusCode.OK;
                    _response.Result = "Lock this user successfully";
                }
                return Ok(_response);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Something wrong when lock/unlock user");
            }
            return BadRequest(_response);
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUserInfoAndRoles(string userId)
        {
            try
            {
                RoleManagement rm = new()
                {
                    ApplicationUser = await _db.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == userId),
                    RoleList = _roleManager.Roles.Select(i => new SelectListItem
                    {
                        Text = i.Name,
                        Value = i.Name,
                    })
                };
                rm.ApplicationUser.Role = _userManager.GetRolesAsync(_db.ApplicationUsers.FirstOrDefault(u => u.Id == userId)).GetAwaiter().GetResult().FirstOrDefault();
                _response.Result = rm;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            } 
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Something wrong when get user role");
            }
            return BadRequest(_response);
        }

        [HttpPost]
        [Route("updateRole/{userId}")]
        public async Task<IActionResult> RoleManagement(string userId, [FromForm] string role)
        {
            try
            {
                var oldRole = _userManager.GetRolesAsync(_db.ApplicationUsers.FirstOrDefault(u => u.Id == userId))
               .GetAwaiter().GetResult().FirstOrDefault();

                ApplicationUser applicationUser = await _db.ApplicationUsers.FirstOrDefaultAsync(u => u.Id == userId);
                if (!(role == oldRole))
                {
                    _userManager.RemoveFromRoleAsync(applicationUser, oldRole).GetAwaiter().GetResult();
                    _userManager.AddToRoleAsync(applicationUser, role).GetAwaiter().GetResult();
                }

                _response.Result = true;
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.StatusCode = HttpStatusCode.BadRequest;
                _response.ErrorMessages.Add("Something wrong when update user role");
            }
            return BadRequest(_response);
        }
    }
}
