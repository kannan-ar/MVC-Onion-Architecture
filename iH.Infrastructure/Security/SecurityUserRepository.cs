namespace iH.Infrastructure
{
    using System;
    using System.Data;
    using System.Collections.Generic;

    using Domain.Security.Entities;
    using Domain.Security.Repositories;
    using Domain.Core.Repositories;

    public sealed class SecurityUserRepository : Repository, ISecurityUserRepository
    {
        public SecurityUserRepository(IHDatabase db) : base(db)
        {
        }

        public SecurityUser GetByUserName(string userName)
        {
            IList<SecurityUserRole> list = db.GetList<SecurityUserRole>(@"select u.user_id, u.password, u.salt, r.role_name from security.security_users u
                inner join security.user_roles ur on ur.user_id = u.user_id
                inner join security.roles r on ur.role_id = r.role_id
                where user_name = @user_name",
                new { user_name = userName });

            SecurityUser user = null;

            foreach (SecurityUserRole userRole in list)
            {
                if (user == null)
                {
                    user = new SecurityUser();

                    user.UserId = userRole.UserId;
                    user.Password = userRole.Password;
                    user.Salt = userRole.Salt;
                    user.Roles = new List<string>();
                }

                user.Roles.Add(userRole.Role);
            }

            return user;
        }

        public Int64 CreateUser(SecurityUser user, int[] roles)
        {
            return Convert.ToInt64(db.GetScalar("select * from security.insert_user(@user_name, @password, @salt, @roles)", new
            {
                user_name = user.UserName,
                password = user.Password,
                salt = user.Salt,
                roles = string.Join(",", roles)
            }));
        }

        public IList<Role> GetUserRoles(string userName)
        {
            return db.GetList<Role>(@"select r.role_name, permission_levels from security.roles r
                inner join security.user_roles ur on r.role_id = ur.role_id
                inner join security.security_users su on ur.user_id = su.user_id
                where su.user_name = @user_name", new { user_name = userName });
        }

        public IList<SecurityUser> GetUsers(int fromRecords, int toRecords, out int totalRecords)
        {
            totalRecords = Convert.ToInt32(db.GetScalar("select count(user_id) from security.security_users"));

            return db.GetList<SecurityUser>(string.Concat("select user_id, user_name from security.security_users limit ", toRecords.ToString(), " offset ", fromRecords.ToString()));
        }

        public SecurityUser GetUserById(int userId)
        {
            return db.Get<SecurityUser>("select user_id, user_name from security.security_users where user_id = @user_id", new { user_id = userId });
        }

        public void ChangePassword(SecurityUser user)
        {
            db.Execute("update security.security_users set password = @password, salt = @salt where user_id = @user_id",
                new { password = user.Password, salt = user.Salt, user_id = user.UserId, });
        }

        public IList<Role> GetUserRoles(int userId)
        {
            return db.GetList<Role>(@"select r.role_id, r.role_name from security.roles r
                inner join security.user_roles ur on r.role_id = ur.role_id
                where ur.user_id = @user_id", new { user_id = userId });
        }

        public void ChangeRoles(int userId, int[] roles)
        {
            db.Execute("security.update_userroles", CommandType.StoredProcedure,
                  new { id = userId, roles = string.Join(",", roles) });
        }
    }
}
