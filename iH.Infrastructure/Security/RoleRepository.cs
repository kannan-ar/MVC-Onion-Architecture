namespace iH.Infrastructure
{
    using System;
    using System.Collections.Generic;

    using Domain.Security.Entities;
    using Domain.Security.Repositories;
    using Domain.Core.Repositories;

    public class RoleRepository : IRoleRepository
    {
        private readonly IHDatabase db;

        public RoleRepository(IHDatabase db)
        {
            this.db = db;
        }

        public IList<Role> GetRoles()
        {
            return db.GetList<Role>("select role_id, role_name from security.roles");
        }

        public void InsertRole(Role role)
        {
            db.Execute("insert into security.roles(role_name, permission_levels) values(@role_name, @permission_levels)",
                new { role_name = role.RoleName, permission_levels = string.Join(",", role.PermissionLevels) });
        }

        public void UpdateRole(Role role)
        {
            db.Execute("update security.roles set role_name = @role_name, permission_levels = @permission_levels where role_id = @role_id",
                new { role_name = role.RoleName, permission_levels = role.PermissionLevels, role_id = role.RoleId });
        }

        public Role GetRole(int roleId)
        {
            return db.Get<Role>("select role_id, role_name, permission_levels from security.roles where role_id = @role_id", new { role_id = roleId });
        }
    }
}
