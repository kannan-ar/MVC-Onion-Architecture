namespace iH.UI.Areas.Payroll.Controllers
{
    using System.Web.Mvc;

    using Domain.Security.Entities;

    public class SalaryController : Controller
    {
        [Authorize(Roles = PermissionLevels.PayrollManager)]
        [HttpGet]
        public ActionResult Definition()
        {
            return View();
        }

        [Authorize(Roles = PermissionLevels.PayrollManager)]
        [HttpGet]
        public ActionResult Revision()
        {
            return View();
        }
    }
}