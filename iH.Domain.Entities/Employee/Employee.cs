namespace iH.Domain.Employee.Entities
{
    using System;
    using Security.Entities;

    public class Employee
    {
        public Int64 EmployeeId { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeNo { get; set; }
        public char Gender { get; set; }
        public string PermanentAddress { get; set; }
        public string CommunicationAddress { get; set; }
        public Int64 NationalityId { get; set; }
        public Int64 PermanentCityId { get; set; }
        public Int64 CommunicationCityId { get; set; }
        public char Status { get; set; }
        public Int64 DesignationId { get; set; }
        public Int64 DepartmentId { get; set; }
        public Int64 LocationId { get; set; }
        public string CardNumber { get; set; }
        public DateTime? DateOfJoin { get; set; }
        public DateTime? OffcialDob { get; set; }
        public string Email { get; set; }
        public Int64? UserId { get; set; }

        public SecurityUser User { get; set; }
        public Department Department { get; set; }
        public Designation Designation { get; set; }
        public Location Location { get; set; }
    }
}
