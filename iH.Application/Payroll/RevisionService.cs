namespace iH.Application.Payroll
{
    using System;
    using System.Collections.Generic;

    using Domain.Payroll.Entities;
    using Domain.Payroll.Repositories;
    using Domain.Payroll.Services;

    public class RevisionService : IRevisionService
    {
        private readonly IRevisionRepository repository;

        public RevisionService(IRevisionRepository repository)
        {
            this.repository = repository;
        }

        public IList<SalaryRevision> GetRevisionByEmployee(Int64 employeeId)
        {
            return repository.GetRevisionByEmployee(employeeId);
        }

        public IList<SalaryRevisionDetails> GetRevisionDetails(Int64 revisionId)
        {
            return repository.GetRevisionDetails(revisionId);
        }
    }
}
