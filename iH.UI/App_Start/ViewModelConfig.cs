namespace iH.UI.App_Start
{
    using AutoMapper;
    using Domain.Employee.Entities;
    using Domain.Payroll.Entities;
    using iH.ViewModels;

    public class ViewModelConfig
    {
        public static void RegisterViewModels()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<EmployeeViewModel, Employee>();
                cfg.CreateMap<Employee, EmployeeViewModel>();
                cfg.CreateMap<SalaryDefinitionViewModel, SalaryDefinition>();
            });
        }
    }
}