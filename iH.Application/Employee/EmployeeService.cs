﻿namespace iH.Application.Employee
{
    using System;
    using System.Text;
    using System.Collections.Generic;

    using Domain.Core.Services;

    using Domain.Core.Repositories;
    using Domain.Employee.Entities;
    using Domain.Employee.Repositories;
    using Domain.Employee.Services;

    using Domain.Security.Entities;
    using Domain.Security.Services;

    public class EmployeeService : IService, IEmployeeService
    {
        private readonly IEmployeeRepository repository;

        public IRepository Repository
        {
            get
            {
                return repository;
            }
        }

        public EmployeeService(IEmployeeRepository repository)
        {
            this.repository = repository;
        }

        public bool IsValidNewEmployee(Employee employee, out Dictionary<string, string> messages)
        {
            bool isValid = true;
            messages = new Dictionary<string, string>();

            if (employee == null || string.IsNullOrWhiteSpace(employee.EmployeeNo))
            {
                messages.Add("ObjectEmpty", "Server Error!");
                return false;
            }

            if(repository.Get(employee.EmployeeNo) != null)
            {
                messages.Add("EmployeeNo", "Employee No is already exists");
                isValid = false;
            }

            return isValid;
        }

        public void GetEmployeeDetails(out IList<Nationality> nationalities, out IList<City> cities,
            out IList<EmployeeStatus> employeeStatus, out IList<Department> departments, out IList<Designation> designations,
            out IList<Location> locations)
        {
            nationalities = repository.GetNationalities();
            cities = repository.GetCities();
            employeeStatus = repository.GetEmployeeStatus();
            departments = repository.GetDepartments();
            designations = repository.GetDesignations();
            locations = repository.GetLocations();
        }

        public Int64 Save(Employee employee)
        {
            Dictionary<string, string> messages;

            if (!IsValidNewEmployee(employee, out messages))
            {
                StringBuilder sb = new StringBuilder();

                foreach(var message in messages)
                {
                    sb.AppendLine(message.Value);
                }

                throw new Exception(sb.ToString());
            }

            return this.repository.Save(employee);
        }

        public void UpdateUser(Int64 employeeId, ISecurityUserService userService, SecurityUser user, int[] roles)
        {
            RepositoryUOW uow = new RepositoryUOW();

            uow.AddService(userService);
            uow.AddService(this);

            uow.Begin();

            try
            {
                Int64 userId = userService.CreateUser(user, roles);
                repository.UpdateUser(employeeId, userId);

                uow.Commit();
            }
            catch
            {
                uow.Rollback();
                throw;
            }
        }

        public Employee Get(Int64 employeeId)
        {
            return repository.Get(employeeId);
        }

        public void Update(Employee employee)
        {
            repository.Update(employee);
        }

        public Employee Detail(Int64 employeeId)
        {
            return repository.Detail(employeeId);
        }
    }
}
