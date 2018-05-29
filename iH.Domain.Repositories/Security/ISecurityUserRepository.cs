namespace iH.Domain.Security.Repositories
{
    using System;
    using System.Collections.Generic;

    using Entities;
    using Core.Repositories;

    public interface ISecurityUserRepository : IRepository
    {
        SecurityUser GetByUserName(string userName);
        Int64 CreateUser(SecurityUser user, int[] roles);
        IList<Role> GetUserRoles(string userName);
        IList<Role> GetUserRoles(int userId);
        IList<SecurityUser> GetUsers(int fromRecords, int toRecords, out int totalRecords);
        SecurityUser GetUserById(int userId);
        void ChangePassword(SecurityUser user);
        void ChangeRoles(int userId, int[] roles);
    }
}
