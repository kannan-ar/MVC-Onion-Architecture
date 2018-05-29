namespace iH.UI.Areas.Security.Controllers
{
    using System;
    using System.Web.Mvc;
    using System.Linq;
    using System.Collections.Generic;

    using Domain.Security.Services;
    using Domain.Security.Entities;
    using iH.ViewModels;

    public class RoleController : Controller
    {
        private readonly IRoleService roleService;
        private readonly ISecurityUserService userService;
        private readonly IMenuService menuService;

        public RoleController(
            IRoleService roleService,
            ISecurityUserService userService,
            IMenuService menuService)
        {
            this.roleService = roleService;
            this.userService = userService;
            this.menuService = menuService;
        }

        [HttpGet]
        [Authorize(Roles = PermissionLevels.RoleManager)]
        public ActionResult Manage()
        {
            return View(new RegisterRoleViewModel(roleService, userService, menuService));
        }

        [HttpGet]
        [Authorize(Roles = PermissionLevels.RoleManager)]
        public ActionResult Edit(int id)
        {
            return View("Manage", new RegisterRoleViewModel(id, roleService, userService, menuService));
        }

        [HttpPost]
        [Authorize(Roles = PermissionLevels.RoleManager)]
        public ActionResult Edit(RegisterRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                roleService.Save(new Role(model.Id, model.SelectedRoleName, model.SelectedPermissionLevels));

                if (model.SelectedMenus == null)
                {
                    model.SelectedMenus = new List<Int64>();
                }

                menuService.SaveRoleMenus(model.Id, model.SelectedMenus.ToArray());

                return RedirectToAction("Manage");
            }
            else
            {
                return View("Manage", new RegisterRoleViewModel(model.Id, roleService, userService, menuService));
            }
        }
    }
}