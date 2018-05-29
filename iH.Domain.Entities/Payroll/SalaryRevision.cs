namespace iH.Domain.Payroll.Entities
{
    using System;
    using System.Collections.Generic;

    using Employee.Entities;

    public class SalaryRevision
    {
        public Int64 RevisionId { get; set; }
        public Int64 EmployeeId { get; set; }
        public DateTime RevisedOn { get; set; }
        public Int64 RevisedBy { get; set; }
        public Employee Employee { get; set; }
        public Employee RevisedEmployee { get; set; }
        public IList<SalaryRevisionDetails> Details { get; set; }
    }
}
