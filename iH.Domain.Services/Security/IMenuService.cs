namespace iH.Domain.Security.Services
{
    using System;
    using System.Collections.Generic;
    using Entities;

    public interface IMenuService
    {
        IList<Menu> GetAll();
        IList<Menu> GetMenuByRole(Int64 roleId);
        IList<Menu> GetMenuByUser(Int64 userId);
        void SaveRoleMenus(Int64 roleId, Int64[] menuIds);
    }
}
