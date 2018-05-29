namespace iH.Application
{
    using System.Collections.Generic;

    using Domain.Security.Repositories;
    using Domain.Security.Services;
    using Domain.Security.Entities;

    public sealed class UserSearchService : IUserSearchService
    {
        private readonly IUserSearchRepository searchRepository;

        public UserSearchService(IUserSearchRepository searchRepository)
        {
            this.searchRepository = searchRepository;
        }

        public IList<SecurityUser> GetUsersByName(string query, int fetchCount)
        {
            return searchRepository.GetUsersByName(query, fetchCount);
        }
    }
}
