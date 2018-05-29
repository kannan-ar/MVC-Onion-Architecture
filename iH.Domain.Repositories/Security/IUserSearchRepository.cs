namespace iH.Domain.Security.Repositories
{
    using System.Collections.Generic;
    using Entities;
    
    public interface IUserSearchRepository
    {
        IList<SecurityUser> GetUsersByName(string query, int fetchCount);
    }
}
