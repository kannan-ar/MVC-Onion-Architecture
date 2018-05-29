namespace iH.Domain.Security.Repositories
{
    using System.Collections.Generic;
    using Entities;

    public interface IRoleRepository
    {
        IList<Role> GetRoles();
        void InsertRole(Role role);
        void UpdateRole(Role role);
        Role GetRole(int roleId);
    }
}
