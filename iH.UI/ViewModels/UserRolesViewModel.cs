namespace iH.ViewModels
{
    using System.Collections.Generic;

    using Domain.Security.Entities;

    public class UserRolesViewModel
    {
        public SecurityUser User { get; set; }
        public IList<Role> Roles { get; set; }
        public List<int> SelectedRoles { get; set; }
    }
}