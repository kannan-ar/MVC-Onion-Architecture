namespace iH.Domain.Security.Services
{
    using System.Collections.Generic;
    using Entities;

    public interface IRoleService
    {
        IList<Role> GetRoles();
        void Save(Role role);
        Role GetRole(int roleId);
    }
}
