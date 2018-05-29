using System.Web.Mvc;

namespace iH.UI.Areas.HR
{
    public class HRAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "HR";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute("Employee_Manage",
              "HR/{controller}/{id}",
              new { controller = "Employee", action = "Index", id = 0 },
              constraints: new { id = @"\d+" }
          );

            context.MapRoute(
                 "HR_default",
                 "HR/{controller}/{action}/{id}",
                 new { controller = "Employee", action = "Index", id = UrlParameter.Optional }
             );

            

        }
    }
}