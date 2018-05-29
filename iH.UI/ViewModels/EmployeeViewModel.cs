namespace iH.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Domain.Employee.Entities;

    public class EmployeeViewModel
    {
        [Required(ErrorMessage = "Employee Name is required")]
        public string EmployeeName { get; set; }

        [Required(ErrorMessage = "Employee Code is required")]
        public string EmployeeNo { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public string Gender { get; set; }

        [Required(ErrorMessage = "Permanent Address is required")]
        public string PermanentAddress { get; set; }

        [Required(ErrorMessage = "Communication Address is required")]
        public string CommunicationAddress { get; set; }

        [Required(ErrorMessage = "Nationality is required")]
        [Range(1, Int64.MaxValue, ErrorMessage = "Nationality is required")]
        public Int64 NationalityId { get; set; }

        [Required(ErrorMessage = "Permanent City is required")]
        [Range(1, Int64.MaxValue, ErrorMessage = "Permanent City is required")]
        public Int64 PermanentCityId { get; set; }

        [Required(ErrorMessage = "Communication City is required")]
        [Range(1, Int64.MaxValue, ErrorMessage = "Communication City is required")]
        public Int64 CommunicationCityId { get; set; }

        [Required(ErrorMessage = "Status is required")]
        public string Status { get; set; }

        [Required(ErrorMessage = "Designation is required")]
        [Range(1, Int64.MaxValue, ErrorMessage = "Designation is required")]
        public Int64 DesignationId { get; set; }

        [Required(ErrorMessage = "Department is required")]
        [Range(1, Int64.MaxValue, ErrorMessage = "Department is required")]
        public Int64 DepartmentId { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [Range(1, Int64.MaxValue, ErrorMessage = "Location is required")]
        public Int64 LocationId { get; set; }

        public string CardNumber { get; set; }

        [Required(ErrorMessage = "Date of Join is required")]
        public DateTime? DateOfJoin { get; set; }

        [Required(ErrorMessage = "Offcial Date of birth is required")]
        public DateTime? OffcialDOB { get; set; }

        public string Email { get; set; }
        public Int64? UserId { get; set; }

        public bool CreateUser { get; set; }

        public IList<Nationality> Nationalities { get; set; }
        public IList<City> Cities { get; set; }
        public IList<EmployeeStatus> EmployeeStatus { get; set; }
        public IList<Designation> Designations { get; set; }
        public IList<Department> Departments { get; set; }
        public IList<Location> Locations { get; set; }
    }
}
