namespace iH.Bootstrapper
{
    using System.Configuration;

    using Ninject;

    using Domain.Core.Repositories;
    using Domain.Security.Repositories;
    using Domain.Employee.Repositories;
    using Domain.Org.Repositories;
    using Domain.Payroll.Repositories;

    using Infrastructure;
    using Infrastructure.Core;
    using Infrastructure.Employee;
    using Infrastructure.Org;
    using Infrastructure.Payroll;

    internal static class RepositoryLayer
    {
        internal static void Register(IKernel kernel)
        {
            kernel.Bind<IHDatabase>().To<Database>().WithConstructorArgument("connectionString",
                ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

            kernel.Bind<ISecurityUserRepository>().To<SecurityUserRepository>();
            kernel.Bind<IErrorRepository>().To<ErrorRepository>();
            kernel.Bind<IRoleRepository>().To<RoleRepository>();
            kernel.Bind<IEmployeeRepository>().To<EmployeeMasterRepository>();
            kernel.Bind<IMenuRepository>().To<MenuRepository>();
            kernel.Bind<IUserSearchRepository>().To<UserSearchRepository>();
            kernel.Bind<IEmployeeSearchRepository>().To<EmployeeSearchRepository>();
            kernel.Bind<ICompanyRepository>().To<CompanyRepository>();
            kernel.Bind<IDefinitionRepository>().To<DefinitionRepository>();
            kernel.Bind<IRevisionRepository>().To<RevisionRepository>();
        }
    }
}
