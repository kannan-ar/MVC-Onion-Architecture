namespace iH.Domain.Security.Services
{
    using System.Collections.Generic;
    using Entities;

    public interface IUserSearchService
    {
        IList<SecurityUser> GetUsersByName(string query, int fetchCount);
    }
}
