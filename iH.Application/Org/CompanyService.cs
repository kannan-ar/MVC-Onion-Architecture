namespace iH.Application.Org
{
    using System.Collections.Generic;

    using Domain.Org.Entities;

    using Domain.Core.Repositories;
    using Domain.Org.Repositories;

    using Domain.Org.Services;
    using Domain.Core.Services;

    public class CompanyService : IService, ICompanyService
    {
        private readonly ICompanyRepository repository;

        public IRepository Repository
        {
            get
            {
                return repository;
            }
        }

        public CompanyService(ICompanyRepository repository)
        {
            this.repository = repository;
        }

        public IList<Company> GetCompanies()
        {
            return repository.GetCompanies();
        }
    }
}
