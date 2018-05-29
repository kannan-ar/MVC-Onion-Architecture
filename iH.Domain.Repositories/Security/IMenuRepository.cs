namespace iH.Domain.Security.Repositories
{
    using System;
    using System.Collections.Generic;
    using Entities;

    public interface IMenuRepository
    {
        IList<Menu> GetAll();
        IList<Menu> GetMenuByRole(Int64 roleId);
        IList<Menu> GetMenuByUser(Int64 userId);
        void SaveRoleMenus(Int64 roleId, Int64[] menuIds);
    }
}
