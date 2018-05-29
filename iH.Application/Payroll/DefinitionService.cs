namespace iH.Application.Payroll
{
    using System;
    using System.Collections.Generic;

    using Domain.Payroll.Entities;

    using Domain.Core.Repositories;
    using Domain.Payroll.Repositories;

    using Domain.Payroll.Services;
    using Domain.Core.Services;

    public class DefinitionService : IService, IDefinitionService
    {
        private readonly IDefinitionRepository repository;

        public IRepository Repository
        {
            get
            {
                return repository;
            }
        }

        public DefinitionService(IDefinitionRepository repository)
        {
            this.repository = repository;
        }

        public IList<SalaryDefinition> GetDefinitionByCompany(Int64 companyId)
        {
            return repository.GetDefinitionByCompany(companyId);
        }

        public SalaryDefinition Save(SalaryDefinition def)
        {
            if (def.DefinitionId == 0)
            {
                def.DefinitionId = repository.Save(def);
            }
            else
            {
                repository.Update(def);
            }

            return def;
        }
    }
}
