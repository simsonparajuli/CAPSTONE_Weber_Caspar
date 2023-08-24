using CASPAR.ApplicationCore.Models;
using CASPAR.ApplicationCore.ViewModels.Input;
using CASPAR.DataAccess;
using CASPAR.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Omu.AwesomeMvc;
using System.Reflection;

namespace CASPAR.Areas.Administrator.Controllers
{
    public class UserController : Controller
    {
        private UnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(UnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public async Task<IActionResult> GetEfAsyncItems(GridParams g)
        {
            var query = _unitOfWork.ApplicationUser.GetAll().ToList();

            var UserRoles = new Dictionary<string, List<string>>();
            foreach (var user in query.ToList())
            {
                var userRole = await _userManager.GetRolesAsync(user);
                UserRoles.Add(user.Id, userRole.ToList());
            }

            var orderedRoles = new List<string>
    {
        StaticDetails.Admin,
        StaticDetails.PC,
        StaticDetails.DC,
        StaticDetails.Secretary,
        StaticDetails.Instructor,
        StaticDetails.Student
    };

            return Json(new GridModelBuilder<ApplicationUser>(query.AsQueryable(), g)
            {
                Map = o =>
                {
                    var userOrderedRoles = UserRoles[o.Id]
                                           .OrderBy(role => orderedRoles.IndexOf(role))
                                           .ToList();

                    var roleToDisplay = string.Join(", ", userOrderedRoles);

                    return new
                    {
                        o.Id,
                        Name = o.FirstName + " " + o.LastName,
                        Role = roleToDisplay,
                        IsAdmin = RolesForUser(o).Contains(StaticDetails.Admin)
                    };
                }
            }.Build());
        }

        public string RolesForUser(ApplicationUser user)
        {
            var roles = _userManager.GetRolesAsync(user).Result;
            var orderedRoles = new List<string>
    {
        StaticDetails.Admin,
        StaticDetails.PC,
        StaticDetails.DC,
        StaticDetails.Secretary,
        StaticDetails.Instructor,
        StaticDetails.Student
    };

            return string.Join(", ", orderedRoles.Where(role => roles.Contains(role)));
        }

        [HttpPost]
        public async Task<IActionResult> AssignAdminRole(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return Json(new { success = false, message = "User ID is missing." });
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return Json(new { success = false, message = "User not found." });
            }

            var result = await _userManager.AddToRoleAsync(user, StaticDetails.Admin);
            if (result.Succeeded)
            {
                return Json(new { success = true });
            }
            else
            {
                return Json(new { success = false, message = "Failed to assign role." });
            }
        }

        [HttpPost]
        public async Task<IActionResult> ToggleAdminRole(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound("User not found");
            }

            var isAdmin = await _userManager.IsInRoleAsync(user, StaticDetails.Admin);
            if (isAdmin)
            {
                await _userManager.RemoveFromRoleAsync(user, StaticDetails.Admin);
            }
            else
            {
                await _userManager.AddToRoleAsync(user, StaticDetails.Admin);
            }

            return Ok("Role updated successfully");
        }
    }
}