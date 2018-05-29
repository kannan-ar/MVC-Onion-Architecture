namespace iH.UI
{
    using System.Web.Mvc;
    using System.Web.Routing;
    using iH.UI.App_Start;
    using System.Web.Optimization;
    using System.Web.Http;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //AreaRegistration.RegisterAllAreas();

            GlobalConfiguration.Configure(WebApiConfig.Register);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            ViewModelConfig.RegisterViewModels();
        }
    }
}
