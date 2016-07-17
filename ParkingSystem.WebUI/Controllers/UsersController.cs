using ParkingSystem.DomainModel.Models;
using ParkingSystem.WebUI.Identity;
using ParkingSystem.WebUI.Models;
using System.Linq;
using System.Web.Mvc;
using ParkingSystem.Core.Pagination;
using ParkingSystem.Core.Services;

namespace ParkingSystem.WebUI.Controllers
{
    [Authorize(Roles = "Admins")]
    public class UsersController : Controller
    {
        private readonly IReservationService _reservationService;
        private readonly ApplicationUserManager _applicationUserManager;
        private readonly int _pageSize = 8;

        public UsersController(IReservationService reservationService,
                               ApplicationUserManager applicationUserManager)
        {
            _reservationService = reservationService;
            _applicationUserManager = applicationUserManager;
        }

        public ActionResult Index(string searchTerm = null, int page = 1)
        {
            IQueryable<ApplicationUser> users;

            if (string.IsNullOrEmpty(searchTerm))
                users = _applicationUserManager.Users;
            else
                users = _applicationUserManager.Users.Where(u => u.UserName.Contains(searchTerm));

            var model = new UsersListViewModel
            {
                Users = users
                            .OrderBy(u => u.UserName)
                            .Skip((page - 1) * _pageSize)
                            .Take(_pageSize)
                            .ToList(),
                SearchTerm = searchTerm,
                AllUsersTotal = _applicationUserManager.Users.Count(),
                PagingInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = _pageSize,
                    TotalItems = users.Count()
                }
            };

            return View(model);
        }

        public ActionResult Search(string searchTerm)
        {
            return RedirectToAction("Index", new { searchTerm = searchTerm });
        }

        public ActionResult MakeUserInactive(string id)
        {
            var user = _applicationUserManager.FindByIdAsync(id).Result;

            if (user == null)
                return HttpNotFound();

            return PartialView("_MakeUserInactive", user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MakeUserInactive(ApplicationUser user)
        {
            var userToInactive = _applicationUserManager.FindByIdAsync(user.Id).Result;

            if (userToInactive == null)
                return HttpNotFound();

            var result = _applicationUserManager.MakeUserInactive(userToInactive);

            if (result.Succeeded)
            {
                TempData["messageSuccess"] = string.Format("The user {0} has been successfully made inactive.",
                    userToInactive.UserName);

                return Json(new { success = true });
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error);

            return PartialView("_MakeUserInactive", user);
        }

        public ActionResult MakeUserActive(string id)
        {
            var user = _applicationUserManager.FindByIdAsync(id).Result;

            if (user == null)
                return HttpNotFound();

            return PartialView("_MakeUserActive", user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult MakeUserActive(ApplicationUser user)
        {
            var userToActive = _applicationUserManager.FindByIdAsync(user.Id).Result;

            if (userToActive == null)
                return HttpNotFound();

            var result = _applicationUserManager.MakeUserActive(userToActive);

            if (result.Succeeded)
            {
                TempData["messageSuccess"] = string.Format("The user {0} has been successfully made active.",
                    userToActive.UserName);

                return Json(new { success = true });
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error);

            return PartialView("_MakeUserActive", user);
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
            var userToDelete = _applicationUserManager.FindByIdAsync(user.Id).Result;

            if (userToDelete == null)
                return HttpNotFound();

            _reservationService.DeleteAllReservationsForUser(userToDelete);
            
            var result = _applicationUserManager.DeleteAsync(userToDelete).Result;

            if (result.Succeeded)
            {
                TempData["messageSuccess"] = string.Format("The user {0} has been successfully deleted.",
                    userToDelete.UserName);

                return Json(new { success = true });
            }

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error);

            return PartialView("_DeleteUser", user);
        }
    }
}
