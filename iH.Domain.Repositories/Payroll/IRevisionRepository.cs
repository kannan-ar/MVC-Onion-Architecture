namespace iH.Domain.Payroll.Repositories
{
    using System;
    using System.Collections.Generic;

    using Entities;

    public interface IRevisionRepository
    {
        IList<SalaryRevision> GetRevisionByEmployee(Int64 employeeId);
        IList<SalaryRevisionDetails> GetRevisionDetails(Int64 revisionId);
    }
}
