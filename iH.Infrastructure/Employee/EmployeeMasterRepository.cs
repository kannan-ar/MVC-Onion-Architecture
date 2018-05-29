namespace iH.Infrastructure.Employee
{
    using System;
    using System.Collections.Generic;

    using Domain.Employee.Entities;
    using Domain.Employee.Repositories;
    using Domain.Core.Repositories;

    public class EmployeeMasterRepository : Repository, IEmployeeRepository
    {
        public EmployeeMasterRepository(IHDatabase db) : base(db)
        {
        }

        public IList<Nationality> GetNationalities()
        {
            return db.GetList<Nationality>("select nationality_id, nationality_name from hr.nationalities");
        }

        public IList<City> GetCities()
        {
            return db.GetList<City>("select city_id, city_name from hr.cities");
        }

        public IList<EmployeeStatus> GetEmployeeStatus()
        {
            return db.GetList<EmployeeStatus>("select status_id, status_name from hr.employee_status");
        }

        public IList<Department> GetDepartments()
        {
            return db.GetList<Department>("select department_id, department_name from hr.departments");
        }

        public IList<Designation> GetDesignations()
        {
            return db.GetList<Designation>("select designation_id, designation_name from hr.designations");
        }

        public IList<Location> GetLocations()
        {
            return db.GetList<Location>("select location_id, location_name from hr.locations");
        }

        public Int64 Save(Employee employee)
        {
            return Convert.ToInt64(db.GetScalar(@"INSERT INTO hr.employees(employee_name, employee_no, gender, permanent_address, 
                communication_address, nationality_id, permanent_city_id, communication_city_id, status, designation_id, department_id, 
                location_id, card_number, date_of_join, offcial_dob, email)
                VALUES(@employee_name, @employee_no, @gender, @permanent_address, @communication_address, @nationality_id, @permanent_city_id, 
                @communication_city_id, @status, @designation_id, @department_id, @location_id, @card_number, @date_of_join, @offcial_dob, 
                @email) RETURNING employee_id;", new
            {
                employee_name = employee.EmployeeName,
                employee_no = employee.EmployeeNo,
                gender = employee.Gender,
                permanent_address = employee.PermanentAddress,
                communication_address = employee.CommunicationAddress,
                nationality_id = employee.NationalityId,
                permanent_city_id = employee.PermanentCityId,
                communication_city_id = employee.CommunicationCityId,
                status = employee.Status,
                designation_id = employee.DesignationId,
                department_id = employee.DepartmentId,
                location_id = employee.LocationId,
                card_number = employee.CardNumber ?? string.Empty,
                date_of_join = employee.DateOfJoin,
                offcial_dob = employee.OffcialDob,
                email = employee.Email ?? string.Empty
            }));
        }

        public void UpdateUser(Int64 employeeId, Int64 userId)
        {
            db.Execute("update hr.employees set user_id = @user_id where employee_id = @employee_id",
                new { user_id = userId, employee_id = employeeId });
        }

        public Employee Get(Int64 employeeId)
        {
            return db.Get<Employee>(@"SELECT employee_id, employee_name, employee_no, gender, permanent_address, 
                communication_address, nationality_id, permanent_city_id, communication_city_id, status, designation_id, 
                department_id, location_id, card_number, date_of_join, offcial_dob, email, user_id FROM hr.employees
                WHERE employee_id = @employee_id", new { employee_id = employeeId });
        }

        public void Update(Employee employee)
        {
            db.Execute(@"UPDATE hr.employees
                SET employee_name=@employee_name, gender=@gender, permanent_address=@permanent_address, 
                communication_address=@communication_address, nationality_id=@nationality_id, permanent_city_id=@permanent_city_id,
                communication_city_id=@communication_city_id, status=@status, designation_id=@designation_id,
                department_id=@department_id, location_id=@location_id, card_number=@card_number, date_of_join=@date_of_join,
                offcial_dob=@offcial_dob, email=@email
                WHERE employee_id=@employee_id", new
            {
                employee_name = employee.EmployeeName ?? string.Empty,
                gender = employee.Gender,
                permanent_address = employee.PermanentAddress ?? string.Empty,
                communication_address = employee.CommunicationAddress ?? string.Empty,
                nationality_id = employee.NationalityId,
                permanent_city_id = employee.PermanentCityId,
                communication_city_id = employee.CommunicationCityId,
                status = employee.Status,
                designation_id = employee.DesignationId,
                department_id = employee.DepartmentId,
                location_id = employee.LocationId,
                card_number = employee.CardNumber ?? string.Empty,
                date_of_join = employee.DateOfJoin,
                offcial_dob = employee.OffcialDob,
                email = employee.Email ?? string.Empty,
                employee_id = employee.EmployeeId
            });
        }
    }
}
