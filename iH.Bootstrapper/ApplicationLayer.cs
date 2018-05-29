namespace iH.Bootstrapper
{
    using Ninject;

    using Domain.Core.Services;
    using Domain.Security.Services;
    using Domain.Employee.Services;
    using Domain.Org.Services;
    using Domain.Payroll.Services;

    using Application;
    using Application.Core;
    using Application.Security;
    using Application.Employee;
    using Application.Org;
    using Application.Payroll;

    internal static class ApplicationLayer
    {
        internal static void Register(IKernel kernel)
        {
            kernel.Bind<ISecurityUserService>().To<SecurityUserService>();
            kernel.Bind<IErrorLogService>().To<ErrorLogService>();
            kernel.Bind<IRoleService>().To<RoleService>();
            kernel.Bind<IEmployeeService>().To<EmployeeService>();
            kernel.Bind<IMenuService>().To<MenuService>();
            kernel.Bind<IUserSearchService>().To<UserSearchService>();
            kernel.Bind<IEmployeeSearchService>().To<EmployeeSearchService>();
            kernel.Bind<ICompanyService>().To<CompanyService>();
            kernel.Bind<IDefinitionService>().To<DefinitionService>();
            kernel.Bind<IRevisionService>().To<RevisionService>();
        }
    }
}
