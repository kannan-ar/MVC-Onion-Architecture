namespace iH.Domain.Org.Services
{
    using System.Collections.Generic;

    using Entities;

    public interface ICompanyService
    {
        IList<Company> GetCompanies();
    }
}
