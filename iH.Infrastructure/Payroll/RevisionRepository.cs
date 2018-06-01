namespace iH.Infrastructure.Payroll
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    using MariGold.Data;

    using Domain.Payroll.Repositories;
    using Domain.Payroll.Entities;
    using Domain.Employee.Entities;
    using Domain.Core.Repositories;
    using Newtonsoft.Json.Serialization;

    public class RevisionRepository : Repository, IRevisionRepository
    {
        public RevisionRepository(IHDatabase db) : base(db) { }

        public IList<SalaryRevision> GetRevisionByEmployee(Int64 employeeId)
        {
            using (var conn = db.Connection)
            {
                return conn.Query<SalaryRevision>(@"select sr.employee_id, e.employee_name, dprt.department_name, desig.designation_name, loc.location_name,
                        sr.revision_id, revised_on, rd.definition_id, rd.amount, rb.employee_id remp_id, rb.employee_name remp_name from 
                        payroll.salary_revision sr inner join payroll.revision_details rd on sr.revision_id = rd.revision_id
                        inner join hr.employees e on sr.employee_id = e.employee_id
                        left outer join hr.employees rb on sr.revised_by = rb.employee_id
                        left outer join hr.departments dprt on e.department_id = dprt.department_id
                        left outer join hr.designations desig on e.designation_id = desig.designation_id
                        left outer join hr.locations loc on e.location_id = loc.location_id
                        where sr.employee_id = @employeeId", new { employeeId = employeeId })
                    .Group(r => r.RevisionId)
                    .Property<Employee>(r => r.Of(e => e.Employee))
                    .Property<Department>(r => r.Of(e => e.Employee).Of(d => d.Department), m => m.Map("department_name", d => d.DepartmentName))
                    .Property<Designation>(r => r.Of(e => e.Employee).Of(d => d.Designation), m => m.Map("designation_name", d => d.DesignationName))
                    .Property<Location>(r => r.Of(e => e.Employee).Of(l => l.Location), m => m.Map("location_name", d => d.LocationName))
                    .Property<Employee>(r => r.Of(e => e.RevisedEmployee), m => m.Map("remp_id", rb => rb.EmployeeId).Map("remp_name", rb => rb.EmployeeName))
                    .Property<SalaryRevisionDetails>(r => r.Of(sr => sr.Details), filter => filter(f => f.RevisionId))
                    //.Single<Employee>(e => e.Employee)
                    //.Single<Employee, Department>(e => e.Department, m => m.Map("department_name", d => d.DepartmentName))
                    //.Single<Designation>(e => e.Employee.Designation, m => m.Map("designation_name", d => d.DesignationName))
                    //.Single<Location>(e => e.Employee.Location, m => m.Map("location_name", d => d.LocationName))
                    //.Single<Employee>(e => e.RevisedEmployee, m => m.Map("remp_id", rb => rb.EmployeeId).Map("remp_name", rb => rb.EmployeeName))
                    //.Many<SalaryRevisionDetails>(e => e.Details, filter => filter(f => f.RevisionId))
                    .GetList();
            }
        }

        public IList<SalaryRevisionDetails> GetRevisionDetails(Int64 revisionId)
        {
            using (var conn = db.Connection)
            {
                return conn.GetList<SalaryRevisionDetails>("select definition_id, amount from payroll.revision_details where revision_id = @revision_id",
                    new { revision_id = revisionId });
            }
        }

        public void SaveRevisionDetails(List<SalaryRevisionDetails> details)
        {
            if (details == null && details.Count == 0)
            {
                return;
            }

            using (var conn = db.Connection)
            {
                //var transation = conn.BeginTransaction();

                try
                {
                    var dezerializerSettings = new JsonSerializerSettings
                    {
                        ContractResolver = new DefaultContractResolver
                        {
                            NamingStrategy = new SnakeCaseNamingStrategy()
                        }
                    };

                    conn.Execute("delete from payroll.revision_details where revision_id = @revision_id",
                        new { revision_id = details[0].RevisionId });

                   // transaction.Commit();

                    conn.Execute(string.Concat("insert into payroll.revision_details(revision_id, definition_id, amount) select revision_id, definition_id, amount from json_populate_recordset(null::payroll.revision_details, '",
                        JsonConvert.SerializeObject(details, dezerializerSettings), "')"));

                }
                catch(Exception ex)
                {
                   // transaction.Rollback();
                }

            }
        }
    }
}
