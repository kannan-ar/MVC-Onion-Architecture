namespace iH.UI.Controllers
{
    using System;
    using System.Web.Mvc;
    using ViewModels;
    using Code;

    public class ErrorController : Controller
    {
        private ErrorViewModel model;

        private void ReadErrorMessage()
        {
            if (TempData[iHSettings.ErrorMessage] != null)
            {
                model.Message = Convert.ToString(TempData[iHSettings.ErrorMessage]);
                TempData[iHSettings.ErrorMessage] = null;
            }
        }

        public ErrorController()
        {
            model = new ErrorViewModel();
            ReadErrorMessage();
        }

        public ActionResult NotFound()
        {
            return View(model);
        }
    }
}