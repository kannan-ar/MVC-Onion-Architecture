namespace iH.Infrastructure
{
    using System;

    using Domain.Security.Repositories;
    using System.Collections.Generic;
    using Domain.Security.Entities;
    using Domain.Core.Repositories;

    public class MenuRepository : IMenuRepository
    {
        private readonly IHDatabase db;

        public MenuRepository(IHDatabase db)
        {
            this.db = db;
        }

        public IList<Menu> GetAll()
        {
            return db.GetList<Menu>("SELECT menu_id, menu_name, menu_url, parent_id FROM security.menus");
        }

        public IList<Menu> GetMenuByRole(Int64 roleId)
        {
            return db.GetList<Menu>("select m.menu_id, m.menu_name from security.role_menus rm left outer join security.menus m on rm.menu_id = m.menu_id where rm.role_id = @role_id",
                new { role_id = roleId });
        }

        public IList<Menu> GetMenuByUser(Int64 userId)
        {
            return db.GetList<Menu>(@"select distinct m.* from security.user_roles ur 
                inner join security.role_menus rm on ur.role_id = rm.role_id
                inner join security.menus m on rm.menu_id = m.menu_id
                where ur.user_id = @user_id", new { user_id = userId });
        }

        public void SaveRoleMenus(Int64 roleId, Int64[] menuIds)
        {
            db.Execute("DELETE FROM security.role_menus WHERE role_id = @role_id", new { role_id = roleId });

            if (menuIds.Length > 0)
            {
                db.Execute(string.Format(
                    "INSERT INTO security.role_menus(role_id, menu_id) SELECT @role_id, menu_id FROM unnest(array[{0}]) AS menu_id",
                    string.Join<Int64>(",", menuIds)), new { role_id = roleId });
            }
        }
    }
}
