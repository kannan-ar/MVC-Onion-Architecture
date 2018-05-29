namespace iH.Application.Employee
{
    using System.Collections.Generic;

    using Domain.Employee.Entities;
    using Domain.Employee.Repositories;
    using Domain.Employee.Services;

    public class EmployeeSearchService : IEmployeeSearchService
    {
        private readonly IEmployeeSearchRepository repository;

        public EmployeeSearchService(IEmployeeSearchRepository repository)
        {
            this.repository = repository;
        }

        public IList<Employee> Search(int from, int count, string nameFilter)
        {
            return repository.Search(from, count, nameFilter);
        }

        public IList<Employee> Search(int count, string nameFilter)
        {
            return repository.Search(count, nameFilter);
        }
    }
}
