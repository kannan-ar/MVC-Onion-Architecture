using Owin;
using Microsoft.Owin;

[assembly: OwinStartup(typeof(iH.UI.Startup))]
namespace iH.UI
{
    using Microsoft.Owin.Security.Cookies;
    using Microsoft.AspNet.Identity;
    using System.Web.Helpers;
    using System.Security.Claims;

    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Security/Login")
            });
            
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
        }
    }
}
