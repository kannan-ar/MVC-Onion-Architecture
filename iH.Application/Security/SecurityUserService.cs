namespace iH.Application
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Core;
    using Domain.Security.Entities;
    using Domain.Security.Repositories;
    using Domain.Security.Services;
    using Domain.Core.Repositories;

    public sealed class SecurityUserService : ISecurityUserService
    {
        public IRepository Repository
        {
            get
            {
                return userRepository;
            }
        }

        private readonly ISecurityUserRepository userRepository;

        public SecurityUserService(ISecurityUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public bool Authenticate(string userName, string password, out SecurityUser user)
        {
            user = userRepository.GetByUserName(userName);

            if (user == null)
            {
                return false;
            }

            if (!PasswordManager.MatchPassword(password, user.Password, user.Salt))
            {
                user = null;
                return false;
            }

            user.UserName = userName;

            return true;
        }

        public Int64 CreateUser(SecurityUser user, int[] roles)
        {
            string salt;

            user.Password = PasswordManager.HashPassword(user.Password, out salt);
            user.Salt = salt;

            return userRepository.CreateUser(user, roles);
        }

        public bool IsValidSecurityUser(SecurityUser user, out IList<string> messages)
        {
            messages = new List<string>();
            bool isValid = true;

            var dbUser = userRepository.GetByUserName(user.UserName);

            if (dbUser != null)
            {
                messages.Add("User Name already exists");
                isValid = false;
            }

            return isValid;
        }

        public string[] GetPermissionLevels(string currentUserName = null)
        {
            if (!string.IsNullOrEmpty(currentUserName))
            {
                string[] roles = GetUserRoles(currentUserName);

                if (roles.Contains(PermissionLevels.Administrator))
                {
                    return roles;
                }
            }

            return PermissionLevels.GetPermissionLevels();
        }

        public string[] GetUserRoles(string userName)
        {
            IList<Role> userRoles = userRepository.GetUserRoles(userName);
            List<string> roles = new List<string>();

            foreach (Role role in userRoles)
            {
                string[] permissions = role.PermissionLevels.Split(new char[] { ',' });

                //Administrator will have all the permissions
                if (permissions.Contains(PermissionLevels.Administrator))
                {
                    roles = new List<string>(PermissionLevels.GetPermissionLevels());
                    roles.Add(PermissionLevels.Administrator);
                    break;
                }

                roles.AddRange(permissions);
            }

            return roles.Distinct().ToArray();
        }

        public IList<SecurityUser> GetUsers(int currentPage, int noRecords, out int totalRecords)
        {
            int fromRecord = (currentPage - 1) * noRecords;
            int toRecord = currentPage * noRecords;

            return userRepository.GetUsers(fromRecord, toRecord, out totalRecords);
        }

        public SecurityUser GetUserById(int userId)
        {
            return userRepository.GetUserById(userId);
        }

        public void ChangePassword(SecurityUser user)
        {
            string salt;

            user.Password = PasswordManager.HashPassword(user.Password, out salt);
            user.Salt = salt;

            userRepository.ChangePassword(user);
        }

        public IList<Role> GetUserRoles(int userId)
        {
            return userRepository.GetUserRoles(userId);
        }

        public void ChangeRoles(int userId, int[] roles)
        {
            userRepository.ChangeRoles(userId, roles);
        }
    }
}
