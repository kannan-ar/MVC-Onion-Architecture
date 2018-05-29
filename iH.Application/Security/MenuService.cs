namespace iH.Application.Security
{
    using System;
    using System.Collections.Generic;

    using Domain.Security.Entities;
    using Domain.Security.Services;
    using Domain.Security.Repositories;

    public class MenuService : IMenuService
    {
        private readonly IMenuRepository db;

        public MenuService(IMenuRepository db)
        {
            this.db = db;
        }

        public IList<Menu> GetAll()
        {
            return db.GetAll();
        }

        public IList<Menu> GetMenuByRole(Int64 roleId)
        {
            return db.GetMenuByRole(roleId);
        }

        public IList<Menu> GetMenuByUser(Int64 userId)
        {
            return db.GetMenuByUser(userId);
        }

        public void SaveRoleMenus(Int64 roleId, Int64[] menuIds)
        {
            db.SaveRoleMenus(roleId, menuIds);
        }
    }
}
