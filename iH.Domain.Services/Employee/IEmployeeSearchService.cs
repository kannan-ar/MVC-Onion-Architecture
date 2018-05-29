namespace iH.Domain.Employee.Services
{
    using System.Collections.Generic;

    using Entities;

    public interface IEmployeeSearchService
    {
        IList<Employee> Search(int from, int count, string nameFilter);
        IList<Employee> Search(int count, string nameFilter);
    }
}
