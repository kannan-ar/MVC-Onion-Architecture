namespace iH.Domain.Payroll.Entities
{
    using System;

    public class SalaryRevisionDetails
    {
        public Int64 RevisionId { get; set; }
        public Int64 DefinitionId { get; set; }
        public decimal Amount { get; set; }
        public SalaryDefinition Definition { get; set; }
    }
}
