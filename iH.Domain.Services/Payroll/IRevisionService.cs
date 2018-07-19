namespace iH.Domain.Payroll.Services
{
    using System;
    using System.Collections.Generic;

    using Entities;

    public interface IRevisionService
    {
        IList<SalaryRevision> GetRevisionByEmployee(Int64 employeeId);
        IList<SalaryRevisionDetails> GetRevisionDetails(Int64 revisionId);
        void SaveRevisionDetails(Int64 employeeId, DateTime revisedOn, Int64 revisedBy, List<SalaryRevisionDetails> details);
    }
}
