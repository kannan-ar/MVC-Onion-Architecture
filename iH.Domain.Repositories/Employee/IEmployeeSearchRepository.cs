namespace iH.Domain.Employee.Repositories
{
    using System.Collections.Generic;

    using Entities;

    public interface IEmployeeSearchRepository
    {
        IList<Employee> Search(int from, int count, string nameFilter);
        IList<Employee> Search(int count, string nameFilter);
    }
}
