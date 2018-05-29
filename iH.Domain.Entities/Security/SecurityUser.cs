namespace iH.Domain.Security.Entities
{
    using System;
    using System.Collections.Generic;

    public class SecurityUser
    {
        public Int64 UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public List<string> Roles { get; set; }
    }
}
