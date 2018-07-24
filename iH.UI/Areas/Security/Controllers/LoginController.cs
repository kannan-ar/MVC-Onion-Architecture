namespace iH.UI.Areas.Security.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Security.Claims;
    using System.Web.Mvc;
    using Microsoft.AspNet.Identity;
    using Microsoft.Owin.Security;
    using System.Web;

    using Domain.Security.Entities;
    using Domain.Security.Services;

    using Domain.Org.Services;

    using iH.ViewModels;
    using Code;

    public class LoginController : Controller
    {
        private readonly ISecurityUserService userService;
        private readonly ICompanyService companyService;

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private LoginViewModel GetViewModel()
        {
            LoginViewModel model = new LoginViewModel();

            model.Companies = companyService.GetCompanies();

            return model;
        }

        private void IdentitySignin(SecurityUser user, Int64 companyId, bool isPersist = false)
        {
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()));
            claims.Add(new Claim(ClaimTypes.Name, user.UserName));
            claims.Add(new Claim(iHUser.CompanyIdName, companyId.ToString()));

            var identity = new ClaimsIdentity(claims, DefaultAuthenticationTypes.ApplicationCookie);

            foreach (string role in user.Roles)
            {
                if (string.IsNullOrEmpty(role))
                {
                    continue;
                }

                string[] permissionLevels = role.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

                foreach(string permissionLevel in permissionLevels)
                {
                    identity.AddClaim(new Claim(ClaimTypes.Role, permissionLevel));
                }
            }

            AuthenticationManager.SignIn(new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = isPersist,
                ExpiresUtc = DateTime.UtcNow.AddDays(7)
            }, identity);
        }

        private void IdentitySignout()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie, DefaultAuthenticationTypes.ExternalCookie);
        }

        public LoginController(ISecurityUserService userService, ICompanyService companyService)
        {
            this.userService = userService;
            this.companyService = companyService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View(GetViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(LoginViewModel model)
        {
            SecurityUser user;

            if (userService.Authenticate(model.UserName, model.Password, out user))
            {
                IdentitySignin(user, model.CompanyId);
                return Redirect("/");
            }
            else
            {
                ViewBag.Message = "Invalid User Name or Password";
                return View(GetViewModel());
            }
        }

        [Authorize]
        public ActionResult End()
        {
            IdentitySignout();
            return RedirectToAction("Index", "Login", new { area = "Security" });
        }
    }
}