namespace iH.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using UI.Code;
    using UI.Code.Validation;
    using Domain.Security.Entities;
    using Domain.Security.Services;

    public class RegisterRoleViewModel
    {
        public RegisterRoleViewModel() { }

        public RegisterRoleViewModel(
            IRoleService roleService,
            ISecurityUserService userService,
            IMenuService menuService)
        {
            this.PermissionLevels = userService.GetPermissionLevels(iHUser.GetCurrentUserName());
            this.Roles = roleService.GetRoles();
            this.Menus = menuService.GetAll();

            SelectedPermissionLevels = new string[] { };
            SelectedMenus = new List<Int64>();
        }

        public RegisterRoleViewModel(
            int id,
            IRoleService roleService,
            ISecurityUserService userService,
            IMenuService menuService) : this(roleService, userService, menuService)
        {
            Role role = roleService.GetRole(id);

            if (role != null)
            {
                if (!string.IsNullOrWhiteSpace(role.PermissionLevels))
                {
                    this.SelectedPermissionLevels = role.PermissionLevels.Split(new char[] { ',' });
                }

                this.SelectedMenus = menuService.GetMenuByRole(id).Select(m => m.MenuId).ToList();

                this.Id = id;
                this.SelectedRoleName = role.RoleName;
            }
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Role name is required")]
        public string SelectedRoleName { get; set; }
        [StringArrayRequired(ErrorMessage = "You must select atleast one permission level")]
        public string[] SelectedPermissionLevels { get; set; }
        public IList<Int64> SelectedMenus { get; set; }

        public IList<Role> Roles { get; set; }
        public string[] PermissionLevels { get; set; }
        public IList<Menu> Menus { get; set; }

       
    }
}