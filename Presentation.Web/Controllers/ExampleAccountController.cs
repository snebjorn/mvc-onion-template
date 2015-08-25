using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Core.DomainModel;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Presentation.Web.App_Start;
using Presentation.Web.Models;

namespace Presentation.Web.Controllers
{
    public class ExampleAccountController : Controller
    {
        // Standard asp.net classes to manage users.
        private ApplicationUserManager _userManager;
        private ApplicationSignInManager _signInManager;
        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }   
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { _signInManager = value; }
        }
        
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModelExample model)
        {
            if (model.Password != model.ConfirmPassword)
                return View(model);
            
            var user = new ApplicationUser() { UserName = model.UserName, Email = model.Email };

            var result = await UserManager.CreateAsync(user, model.Password);
            
            if(result.Succeeded)
            {
                await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);
                return RedirectToAction("Index", "Home");
            }

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModelExample model, string returnUrl)
        {
            var result = await SignInManager.PasswordSignInAsync(model.UserName, model.Password, model.RememberMe, false);

            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToAction("Index", "Home");
                case SignInStatus.Failure:
                    return RedirectToAction("Login", "ExampleAccount");
            }
            
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModelExample model)
        {
            var user = await UserManager.FindByEmailAsync(model.Email);
            if (user == null || !(await _userManager.IsEmailConfirmedAsync(user.Id)))
            {
                // Handle "user not found"
            }
            // Send email to the user with instructions on how to reset the password.
            
            // If it reaches this, then something failed and we redisplay the form.
            return View(model);
        }
        
    }
}
