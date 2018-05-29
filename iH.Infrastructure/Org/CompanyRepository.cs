namespace iH.Infrastructure.Org
{
    using System.Collections.Generic;

    using Domain.Org.Entities;
    using Domain.Core.Repositories;
    using Domain.Org.Repositories;

    public class CompanyRepository : Repository, ICompanyRepository
    {
        public CompanyRepository(IHDatabase db) : base(db) { }

        public IList<Company> GetCompanies()
        {
            return db.GetList<Company>("select company_id, company_name from org.companies");
        }
    }
}
