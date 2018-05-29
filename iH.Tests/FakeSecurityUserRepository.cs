namespace iH.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Domain.Security.Entities;
    using Domain.Security.Repositories;
    using Domain.Core.Repositories;

    public class FakeSecurityUserRepository : Repository, ISecurityUserRepository
    {
        public List<SecurityUser> list;

        public FakeSecurityUserRepository(IHDatabase db) : base(db)
        {
            list = new List<SecurityUser>();
        }

        public SecurityUser GetByUserName(string userName)
        {
            return list.Where(l => l.UserName == userName).FirstOrDefault();
        }

        public Int64 CreateUser(SecurityUser user, int[] roles)
        {
            Int64 userId = list.Count + 1;

            list.Add(new SecurityUser()
            {
                UserId = userId,
                UserName = user.UserName,
                Password = user.Password,
                Salt = user.Salt
            });

            return userId;
        }

        public IList<Role> GetUserRoles(string userName)
        {
            throw new NotImplementedException();
        }

        public IList<Role> GetUserRoles(int userId)
        {
            throw new NotImplementedException();
        }

        public IList<SecurityUser> GetUsers(int fromRecords, int toRecords, out int totalRecords)
        {
            throw new NotImplementedException();
        }

        public SecurityUser GetUserById(int userId)
        {
            throw new NotImplementedException();
        }

        public void ChangePassword(SecurityUser user)
        {
            throw new NotImplementedException();
        }

        public void ChangeRoles(int userId, int[] roles)
        {
            throw new NotImplementedException();
        }
    }
}
