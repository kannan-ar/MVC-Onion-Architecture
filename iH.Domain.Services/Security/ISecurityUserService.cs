namespace iH.Domain.Security.Services
{
    using System;
    using System.Collections.Generic;

    using Entities;
    using Core.Services;

    public interface ISecurityUserService : IService
    {
        bool Authenticate(string userName, string password, out SecurityUser user);
        Int64 CreateUser(SecurityUser user, int[] roles);
        bool IsValidSecurityUser(SecurityUser user, out IList<string> messages);
        string[] GetUserRoles(string userName);
        IList<Role> GetUserRoles(int userId);
        IList<SecurityUser> GetUsers(int currentPage, int noRecords, out int totalRecords);
        SecurityUser GetUserById(int userId);
        string[] GetPermissionLevels(string currentUserName = null);
        void ChangePassword(SecurityUser user);
        void ChangeRoles(int userId, int[] roles);
    }
}
