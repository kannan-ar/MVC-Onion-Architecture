namespace iH.UI.Areas.Security.Controllers
{
    using System.Web.Mvc;

    using Domain.Security.Services;
    using ViewModels;
    using Code;

    public class MenuController : Controller
    {
        private readonly IMenuService menuService;

        public MenuController(IMenuService menuService)
        {
            this.menuService = menuService;
        }

        [ChildActionOnly]
        public ActionResult List()
        {
            return View(MenuViewModel.GetViewModel(menuService.GetMenuByUser(iHUser.GetCurrentUserId())));
        }
    }
}