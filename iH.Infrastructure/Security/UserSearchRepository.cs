namespace iH.Infrastructure
{
    using System.Collections.Generic;

    using Domain.Security.Entities;
    using Domain.Security.Repositories;
    using Domain.Core.Repositories;

    public sealed class UserSearchRepository : IUserSearchRepository
    {
        private readonly IHDatabase db;

        public UserSearchRepository(IHDatabase db)
        {
            this.db = db;
        }

        public IList<SecurityUser> GetUsersByName(string query, int fetchCount)
        {
            return db.GetList<SecurityUser>(string.Concat("select user_id, user_name from security.security_users where user_name like @text || '%' order by user_name fetch first ", fetchCount.ToString(), " rows only"),
                new { text = query });
        }
    }
}
