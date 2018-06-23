namespace iH.Domain.Employee.Repositories
{
    using System;
    using System.Collections.Generic;

    using Entities;
    using Core.Repositories;

    public interface IEmployeeRepository : IRepository
    {
        IList<Nationality> GetNationalities();
        IList<City> GetCities();
        IList<EmployeeStatus> GetEmployeeStatus();
        IList<Department> GetDepartments();
        IList<Designation> GetDesignations();
        IList<Location> GetLocations();

        Int64 Save(Employee employee);
        void UpdateUser(Int64 employeeId, Int64 userId);

        Employee Get(Int64 employeeId);
        void Update(Employee employee);
        Employee Detail(Int64 employeeId);
    }
}
