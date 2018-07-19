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
                return conn.Query<SalaryRevision>(@"select revised_on, rb.employee_id, rb.employee_name from payroll.salary_revision sr
                        left outer join hr.employees rb on sr.revised_by = rb.employee_id
                        where sr.employee_id = @employeeId", new { employeeId = employeeId })
                    .Property<Employee>(r => r.Of(e => e.RevisedEmployee))
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

        public void SaveRevisionDetails(Int64 employeeId, DateTime revisedOn, Int64 revisedBy, List<SalaryRevisionDetails> details)
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

                    Int64 revisionId = details[0].RevisionId;

                    if (revisionId != 0)
                    {
                        conn.Execute("delete from payroll.revision_details where revision_id = @revision_id",
                            new { revision_id = details[0].RevisionId });
                    }
                    else
                    {
                        revisionId = Convert.ToInt64(conn.GetScalar("INSERT INTO payroll.salary_revision(employee_id, revised_on, revised_by) VALUES (@employee_id, @revised_on, @revised_by) RETURNING revision_id;",
                            new { employee_id = employeeId, revised_on = revisedOn, revised_by = revisedBy }));

                        foreach(var detail in details)
                        {
                            detail.RevisionId = revisionId;
                        }
                    }

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
