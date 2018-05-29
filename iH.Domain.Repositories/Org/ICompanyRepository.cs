namespace iH.Domain.Org.Repositories
{
    using System.Collections.Generic;

    using Entities;
    using Core.Repositories;

    public interface ICompanyRepository : IRepository
    {
        IList<Company> GetCompanies();
    }
}
