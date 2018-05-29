namespace iH.Domain.Payroll.Services
{
    using System;
    using System.Collections.Generic;

    using Entities;

    public interface IDefinitionService
    {
        IList<SalaryDefinition> GetDefinitionByCompany(Int64 companyId);
        SalaryDefinition Save(SalaryDefinition def);
    }
}
