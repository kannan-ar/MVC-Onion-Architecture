namespace iH.Infrastructure.Employee
{
    using System.Collections.Generic;

    using Domain.Core.Repositories;
    using Domain.Employee.Entities;
    using Domain.Security.Entities;
    using Domain.Employee.Repositories;

    using MariGold.Data;

    public class EmployeeSearchRepository : Repository, IEmployeeSearchRepository
    {
        public EmployeeSearchRepository(IHDatabase db) : base(db) { }

        public IList<Employee> Search(int from, int count, string nameFilter)
        {
            return db.Connection.Query<Employee>(string.Concat(@"
                    select employee_id, employee_name, designation_name, department_name, user_name from hr.employees e
                    left outer join hr.designations desig on e.designation_id = desig.designation_id
                    left outer join hr.departments depart on e.department_id = depart.department_id
                    left outer join security.security_users u on e.user_id = u.user_id",
                    string.IsNullOrEmpty(nameFilter) ? string.Empty : " where employee_name ilike @text || '%'",
                    " offset ", from.ToString(), " limit ", count.ToString()))
                    .Property<SecurityUser>(e => e.Of(u => u.User))
                    .Property<Department>(q => q.Of(e => e.Department))
                    .Property<Designation>(q => q.Of(e => e.Designation))
                    //.Single<SecurityUser>(e => e.User)
                    //.Single<Department>(e => e.Department)
                    //.Single<Designation>(e => e.Designation)
                    .GetList();
        }

        public IList<Employee> Search(int count, string nameFilter)
        {
            return db.GetList<Employee>(string.Concat("select employee_id, employee_name from hr.employees where employee_name ilike @text || '%'",
                " limit ", count.ToString()), new { text = nameFilter ?? string.Empty });
        }
    }
}
