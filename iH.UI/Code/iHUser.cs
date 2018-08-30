namespace iH.UI.Code
{
    using System;
    using System.Security.Claims;
    using System.Web;
    using System.Collections.Generic;

    public static class iHUser
    {
        public const string CompanyIdName = "CompanyId";

        public static string GetCurrentUserName()
        {
            ClaimsIdentity claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;

            if (claimsIdentity != null)
            {
                return claimsIdentity.FindFirst(ClaimTypes.Name)?.Value;
            }

            return string.Empty;
        }

        public static Int64 GetCurrentUserId()
        {
            Int64 userId = 0;
            ClaimsIdentity claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;

            if (claimsIdentity != null)
            {
                string value = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier)?.Value;

                Int64 temp;

                if(Int64.TryParse(value, out temp))
                {
                    userId = temp;
                }
            }

            return userId;
        }

        public static Int64 GetCurrentCompanyId()
        {
            Int64 companyId = 0;
            ClaimsIdentity claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;

            if (claimsIdentity != null)
            {
                string value = claimsIdentity.FindFirst(CompanyIdName)?.Value;

                Int64 temp;

                if (Int64.TryParse(value, out temp))
                {
                    companyId = temp;
                }
            }

            return companyId;
        }

        public static List<string> GetCurrentUserRoles()
        {
            List<string> roles = new List<string>();
            ClaimsIdentity claimsIdentity = HttpContext.Current.User.Identity as ClaimsIdentity;

            var claims = claimsIdentity.FindAll(ClaimTypes.Role);

            foreach(var claim in claims)
            {
                roles.Add(claim.Value);
            }

            return roles;
        }
    }
}