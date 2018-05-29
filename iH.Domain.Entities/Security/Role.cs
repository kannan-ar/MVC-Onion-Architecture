namespace iH.Domain.Security.Entities
{
    public class Role
    {
        public Role() { }

        public Role(int id, string roleName, string[] permissionLevels)
        {
            RoleId = id;
            RoleName = roleName;
            PermissionLevels = string.Join(",", permissionLevels);
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string PermissionLevels { get; set; }
    }
}
