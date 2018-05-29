namespace iH.Infrastructure.Payroll
{
    using System;
    using System.Collections.Generic;

    using Domain.Payroll.Entities;
    using Domain.Payroll.Repositories;
    using Domain.Core.Repositories;

    public class DefinitionRepository : Repository, IDefinitionRepository
    {
        public DefinitionRepository(IHDatabase db) : base(db) { }

        public IList<SalaryDefinition> GetDefinitionByCompany(Int64 companyId)
        {
            return db.GetList<SalaryDefinition>(@"select row_id, definition_id, definition_name, company_id, formula, 
                formula_percentage, minimum, maximum, is_deduction, is_variable, is_incentive, is_lop 
                from payroll.salary_definition
                where company_id = @company_id",
                new { company_id = companyId });
        }

        public Int64 Save(SalaryDefinition def)
        {
            return Convert.ToInt64(db.GetScalar(@"INSERT INTO payroll.salary_definition(row_id, definition_name, 
                company_id, formula, formula_percentage, minimum, maximum, is_deduction, is_variable, is_incentive, is_lop)
                VALUES(@row_id, @definition_name, @company_id, @formula, @formula_percentage, @minimum, @maximum, @is_deduction, 
                @is_variable, @is_incentive, @is_lop) RETURNING definition_id;", new
            {
                row_id = def.RowId,
                definition_name = def.DefinitionName,
                company_id = def.CompanyId,
                formula = def.Formula ?? string.Empty,
                formula_percentage = def.FormulaPercentage,
                minimum = def.Minimum,
                maximum = def.Maximum,
                is_deduction = def.IsDeduction,
                is_variable = def.IsVariable,
                is_incentive = def.IsIncentive,
                is_lop = def.IsLOP
            }));
        }

        public void Update(SalaryDefinition def)
        {
            db.GetScalar(@"UPDATE payroll.salary_definition
	            SET row_id = @row_id, definition_name=@definition_name, company_id=@company_id, formula=@formula, 
                formula_percentage=@formula_percentage, minimum=@minimum, maximum=@maximum, is_deduction=@is_deduction, 
                is_variable=@is_variable, is_incentive=@is_incentive, is_lop=@is_lop
	            WHERE definition_id = @definition_id;", new
            {
                row_id = def.RowId,
                definition_id = def.DefinitionId,
                definition_name = def.DefinitionName,
                company_id = def.CompanyId,
                formula = def.Formula,
                formula_percentage = def.FormulaPercentage,
                minimum = def.Minimum,
                maximum = def.Maximum,
                is_deduction = def.IsDeduction,
                is_variable = def.IsVariable,
                is_incentive = def.IsIncentive,
                is_lop = def.IsLOP
            });
        }
    }
}
