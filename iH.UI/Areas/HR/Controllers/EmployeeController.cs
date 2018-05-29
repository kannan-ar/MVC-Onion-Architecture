namespace iH.UI.Areas.HR.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;
    using AutoMapper;

    using Domain.Employee.Services;
    using Domain.Employee.Entities;
    using Domain.Security.Entities;
    using iH.ViewModels;
    using Code;

    [RouteArea("HR")]
    [RoutePrefix("Employee")]
    public class EmployeeController : Controller
    {
        private IEmployeeService employeeService;

        private EmployeeViewModel GetEmployeeViewModel(Int64 id)
        {
            EmployeeViewModel model = default(EmployeeViewModel);
            
            if (id == 0)
            {
                model = new EmployeeViewModel();
            }
            else
            {
                Employee emp = employeeService.Get(id);
                model = Mapper.Map<EmployeeViewModel>(emp);
            }

            if (model != null)
            {
                IList<Nationality> nationalities;
                IList<City> cities;
                IList<EmployeeStatus> employeeStatus;
                IList<Department> departments;
                IList<Designation> designations;
                IList<Location> locations;

                employeeService.GetEmployeeDetails(out nationalities, out cities, out employeeStatus, out departments, out designations,
                   out locations);

                model.Nationalities = nationalities;
                model.Cities = cities;
                model.EmployeeStatus = employeeStatus;
                model.Departments = departments;
                model.Designations = designations;
                model.Locations = locations;
            }

            return model;
        }

        public EmployeeController(IEmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        //[HandleError]
        [Authorize(Roles = PermissionLevels.EmployeeManager)]
        [HttpGet]
        public ActionResult Index(int id)
        {
            EmployeeViewModel model = GetEmployeeViewModel(id);

            if (model != null)
            {
                return View(model);
            }
            else
            {
                TempData[iHSettings.ErrorMessage] = "";
               // throw new HttpException(404, "Not found");
                 return HttpNotFound("Employee Not Found");
                //return Redirect("~/Error/NotFound");

            }
        }

        [Authorize(Roles = PermissionLevels.EmployeeManager)]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult Index(Int64 id, EmployeeViewModel model)
        {
            Employee emp = Mapper.Map<Employee>(model);

            if (id == 0)
            {
                id = employeeService.Save(emp);
            }
            else
            {
                emp.EmployeeId = id;
                employeeService.Update(emp);
            }

            if (model.CreateUser)
            {
                return Redirect(Url.Action("Register", "User", new { area = "Security", employeeId = id }));
            }
            else
            {
                return RedirectToAction("List");
            }
        }

        [Authorize(Roles = PermissionLevels.EmployeeManager)]
        [HttpGet]
        public ActionResult List()
        {
            return View();
        }
    }
}