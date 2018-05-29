namespace iH.Domain.Security.Entities
{
    public static class PermissionLevels
    {
        public const string UserManager = "User Manager";
        public const string EmployeeManager = "Employee Manager";
        public const string PayrollManager = "Payroll Manager";
        public const string RoleManager = "Role Manager";
        public const string Administrator = "Administrator";

        public static string[] GetPermissionLevels()
        {
            return new string[]
            {
                UserManager,
                EmployeeManager,
                PayrollManager,
                RoleManager
            };
        }
    }
}
