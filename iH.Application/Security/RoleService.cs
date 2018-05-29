namespace iH.Application
{
    using System.Collections.Generic;
    using Domain.Security.Repositories;
    using Domain.Security.Entities;
    using Domain.Security.Services;

    public class RoleService : IRoleService
    {
        private readonly IRoleRepository repository;

        public RoleService(IRoleRepository repository)
        {
            this.repository = repository;
        }

        public IList<Role> GetRoles()
        {
            return repository.GetRoles();
        }

        public void Save(Role role)
        {
            if(role.RoleId == 0)
            {
                repository.InsertRole(role);
            }
            else
            {
                repository.UpdateRole(role);
            }
        }

        public Role GetRole(int roleId)
        {
            return repository.GetRole(roleId);
        }
    }
}
