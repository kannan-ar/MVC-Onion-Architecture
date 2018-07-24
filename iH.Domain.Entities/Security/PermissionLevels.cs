namespace iH.Domain.Security.Entities
{
    public static class PermissionLevels
    {
        public const string UserManager = "UserManager";
        public const string EmployeeManager = "EmployeeManager";
        public const string PayrollManager = "PayrollManager";
        public const string RoleManager = "RoleManager";
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
