namespace iH.ViewModels
{
    using System;

    public class SalaryDefinitionViewModel
    {
        public Int64? DefinitionId { get; set; }
        public Int16 RowId { get; set; }
        public string DefinitionName { get; set; }
        public Int64? CompanyId { get; set; }
        public string Formula { get; set; }
        public decimal FormulaPercentage { get; set; }
        public decimal Minimum { get; set; }
        public decimal Maximum { get; set; }
        public bool IsDeduction { get; set; }
        public bool IsVariable { get; set; }
        public bool IsIncentive { get; set; }
        public bool IsLOP { get; set; }
    }
}