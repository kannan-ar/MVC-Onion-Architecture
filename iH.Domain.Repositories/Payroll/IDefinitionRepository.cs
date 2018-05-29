namespace iH.Domain.Payroll.Repositories
{
    using System;
    using System.Collections.Generic;

    using Entities;
    using Core.Repositories;

    public interface IDefinitionRepository : IRepository
    {
        IList<SalaryDefinition> GetDefinitionByCompany(Int64 companyId);
        Int64 Save(SalaryDefinition def);
        void Update(SalaryDefinition def);
    }
}
