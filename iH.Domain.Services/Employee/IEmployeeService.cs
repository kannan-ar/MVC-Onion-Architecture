namespace iH.Domain.Employee.Services
{
    using System;
    using System.Collections.Generic;

    using Entities;
    using Security.Services;
    using Security.Entities;
    using Core.Services;

    public interface IEmployeeService : IService
    {
        void GetEmployeeDetails(out IList<Nationality> nationalities, out IList<City> cities,
            out IList<EmployeeStatus> employeeStatus, out IList<Department> departments, out IList<Designation> designations,
            out IList<Location> locations);
        Int64 Save(Employee employee);
        void UpdateUser(Int64 employeeId, ISecurityUserService userService, SecurityUser user, int[] roles);
        Employee Get(Int64 employeeId);
        void Update(Employee employee);
        Employee Detail(Int64 employeeId);
    }
}
