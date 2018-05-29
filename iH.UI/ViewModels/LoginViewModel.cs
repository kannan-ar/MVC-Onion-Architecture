namespace iH.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Domain.Org.Entities;

    public class LoginViewModel
    {
        [Required(ErrorMessage = "Select a company to proceed")]
        public Int64 CompanyId { get; set; }

        [Required(ErrorMessage = "User Name is required")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public IList<Company> Companies { get; set; }
    }
}