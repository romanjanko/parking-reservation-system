using ParkingSystem.Core.Models;
using ParkingSystem.DomainModel.Models;
using ParkingSystem.WebUI.Identity;
using ParkingSystem.WebUI.Models;
using System;
using System.Linq;
using System.Web.Mvc;

namespace ParkingSystem.WebUI.Controllers
{
    [Authorize(Roles = "Admins")]
    public class UsersController : Controller
    {
        private readonly ApplicationUserManager _applicationUserManager;
        private readonly int pageSize = 8;

        public UsersController(ApplicationUserManager applicationUserManager)
        {
            _applicationUserManager = applicationUserManager;
        }

        public ActionResult Index(int page = 1)
        {
            var model = new UsersListViewModel
            {
                Users = _applicationUserManager.Users
                                .OrderBy(u => u.UserName)
                                .Skip((page - 1) * pageSize)
                                .Take(pageSize)
                                .ToList(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = _applicationUserManager.Users.Count()
                }
            };

            return View(model);
        }

        public ActionResult DeleteUser(string id)
        {
            var user = _applicationUserManager.FindByIdAsync(id).Result;

            if (user == null)
                return HttpNotFound();

            return PartialView("_DeleteUser", user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteUser(ApplicationUser user)
        {
            try
            {
                var userToDelete = _applicationUserManager.FindByIdAsync(user.Id).Result;

                if (userToDelete == null)
                    return HttpNotFound();

                // TODO throws an exception for some reason
                var result = _applicationUserManager.DeleteAsync(userToDelete).Result;

                if (result.Succeeded)
                {
                    TempData["messageSuccess"] = string.Format("The user {0} has been successfully deleted.",
                        userToDelete.UserName);

                    return Json(new { success = true });
                }

                foreach (var error in result.Errors)
                    ModelState.AddModelError("", error);
            }
            catch(Exception)
            {
            }
            finally
            {
                ModelState.AddModelError("", "An unknown error occurred.");
            }

            return PartialView("_DeleteUser", user);
        }
    }
}
