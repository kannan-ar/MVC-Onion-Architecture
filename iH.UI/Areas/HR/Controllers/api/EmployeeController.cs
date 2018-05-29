namespace iH.UI.Areas.HR.Controllers.api
{
    using System.Collections.Generic;
    using System.Web.Http;

    using Domain.Employee.Entities;
    using Domain.Security.Entities;
    using Domain.Employee.Services;
    using ViewModels;

    public class EmployeeController : ApiController
    {
        private readonly IEmployeeSearchService searchService;

        public EmployeeController(IEmployeeSearchService searchService)
        {
            this.searchService = searchService;
        }

        [Authorize(Roles = PermissionLevels.EmployeeManager)]
        [HttpGet]
        public IHttpActionResult List(int start, int length, [FromUri(Name = "search.value")] string search)
        {
            IList<Employee> list = searchService.Search(start, length, search);
            
            DataTableViewModel vm = DataTableViewModel.GetViewModel(() =>
            {
                List<List<string>> data = new List<List<string>>();

                foreach(Employee emp in list)
                {
                    List<string> item = new List<string>();

                    item.Add(emp.EmployeeName);
                    item.Add(emp.Designation?.DesignationName);
                    item.Add(emp.Department?.DepartmentName);
                    item.Add(emp.User?.UserName);
                    item.Add(emp.EmployeeId.ToString());

                    data.Add(item);
                }

                return data;
            });

            return Json(vm);
        }

        [Authorize(Roles = PermissionLevels.EmployeeManager)]
        [HttpGet]
        public IHttpActionResult List(string query)
        {
            return Json(searchService.Search(10, query));
        }
    }
}
