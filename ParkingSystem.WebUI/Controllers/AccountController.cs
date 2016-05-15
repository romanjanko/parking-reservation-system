using System.Web;
using System.Web.Mvc;
using ParkingSystem.WebUI.Models;
using Microsoft.AspNet.Identity;
using ParkingSystem.DomainModel.Models;
using ParkingSystem.WebUI.Identity;

namespace ParkingSystem.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly ApplicationUserManager _applicationUserManager;

        public AccountController(ApplicationUserManager applicationUserManager)
        {
            _applicationUserManager = applicationUserManager;
        }
        
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = _applicationUserManager.Find(model.EmailAddress, model.Password);

            if (user != null)
            {
                if (!_applicationUserManager.IsEmailConfirmed(user.Id))
                {
                    ModelState.AddModelError("", "You must have a confirmed email to log in. Check your email inbox.");
                    return View(model);
                }
                else if (!_applicationUserManager.IsUserActive(user.Id))
                {
                    ModelState.AddModelError("", "Your account is inactive. Contact your administrator to activate it.");
                    return View(model);
                }
                else
                {
                    // TODO SignInManager
                    var identity = _applicationUserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

                    var ctx = Request.GetOwinContext();
                    var authManager = ctx.Authentication;

                    authManager.SignIn(identity);

                    return RedirectToAction("Index", "Home");
                }
            }

            ModelState.AddModelError("", "The email address or password provided is incorrect.");
            return View(model);
        }

        [HttpPost]
        public ActionResult LogOff()
        {
            var ctx = Request.GetOwinContext();
            var authManager = ctx.Authentication;

            authManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser { UserName = model.EmailAddress, Email = model.EmailAddress };
                IdentityResult result = _applicationUserManager.Create(user, model.NewPassword);

                if (result.Succeeded)
                {
                    var code = _applicationUserManager.GenerateEmailConfirmationToken(user.Id);
                    string callbackUrl = Url.Action(
                        "ConfirmAccount", "Account", 
                        new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);

                    _applicationUserManager.SendEmail(
                        user.Id,
                        "[ParkingReservationSystem] Confirm your account",
                        "Please confirm your account for parking reservation system by clicking this <a href=\"" + callbackUrl + "\">link</a>.");
                    
                    return RedirectToAction("RegistrationConfirmed");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult RegistrationConfirmed()
        {
            return View();
        }
        
        [AllowAnonymous]
        public ActionResult ConfirmAccount(string userId, string code)
        {
            try
            {
                IdentityResult result = _applicationUserManager.ConfirmEmail(userId, code);

                if (result.Succeeded)
                    return View("ConfirmAccount");

                return View("Error");
            }
            catch
            {
                return View("Error");
            }
        }
        
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = _applicationUserManager.ChangePassword(
                                    User.Identity.GetUserId(), model.OldPassword, model.NewPassword);

                if (result.Succeeded)
                {
                    return RedirectToAction("ChangePasswordConfirmation");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }
            }

            return View(model);
        }

        public ActionResult ChangePasswordConfirmation()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult PasswordRecovery()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]
        public ActionResult PasswordRecovery(PasswordRecoveryViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _applicationUserManager.FindByName(model.EmailAddress);

                if (user == null || !_applicationUserManager.IsEmailConfirmed(user.Id))
                {
                    // don't reveal that the user does not exist or is not confirmed
                    return RedirectToAction("PasswordRecoveryConfirmation");
                }

                var code = _applicationUserManager.GeneratePasswordResetToken(user.Id);

                var callbackUrl = Url.Action(
                    "ResetPassword", "Account",
                    new { UserId = user.Id, code = code }, protocol: Request.Url.Scheme);
                
                _applicationUserManager.SendEmail(
                    user.Id,
                    "[ParkingReservationSystem] Reset password",
                    "Please reset your password for parking reservation system by clicking this <a href=\"" + callbackUrl + "\">link</a>.");

                return RedirectToAction("PasswordRecoveryConfirmation");
            }

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult PasswordRecoveryConfirmation()
        {
            return View();
        }

        [AllowAnonymous]
        public ActionResult ResetPassword(string userId, string code)
        {
            if (userId == null || code == null)
                return View("Error");

            return View(
                new PasswordResetViewModel
                {
                    UserId = userId,
                    Code = code
                });
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ResetPassword(PasswordResetViewModel model)
        { 
            try
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = _applicationUserManager.ResetPassword(
                                                    model.UserId, model.Code, model.NewPassword);

                    if (result.Succeeded)
                    {
                        return RedirectToAction("ResetPasswordConfirmation");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error);
                        }
                    }
                }

                return View(model);
            }
            catch
            {
                return View("Error");
            }
        }

        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }
    }
}
