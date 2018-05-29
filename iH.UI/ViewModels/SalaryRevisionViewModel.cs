namespace iH.UI.ViewModels
{
    using System.Collections.Generic;

    using Domain.Employee.Entities;
    using Domain.Payroll.Entities;

    public class SalaryRevisionViewModel
    {
        public Employee Employee { get; set; }
        public IList<SalaryDefinition> RevisionList { get; set; }
    }
}