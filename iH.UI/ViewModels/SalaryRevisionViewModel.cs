namespace iH.ViewModels
{
    using System;
    using System.Collections.Generic;

    using Domain.Employee.Entities;
    using Domain.Payroll.Entities;

    public class SalaryRevisionViewModel
    {
        public Int64 EmployeeId { get; set; }
        public List<SalaryRevisionDetails> RevisionList { get; set; }
    }
}