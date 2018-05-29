namespace iH.UI.Areas.Payroll.Controllers.api
{
    using System.Web.Http;
    using AutoMapper;

    using Domain.Security.Entities;
    using Domain.Payroll.Entities;
    using Domain.Payroll.Services;
    using Code;
    using iH.ViewModels;

    public class DefinitionController : ApiController
    {
        private readonly IDefinitionService definitionService;

        public DefinitionController(IDefinitionService definitionService)
        {
            this.definitionService = definitionService;
        }

        [Authorize(Roles = PermissionLevels.PayrollManager)]
        [HttpGet]
        public IHttpActionResult List()
        {
           var definitions = definitionService.GetDefinitionByCompany(iHUser.GetCurrentCompanyId());

            return Json(definitions);
        }

        [Authorize(Roles = PermissionLevels.PayrollManager)]
        [HttpPost]
        public IHttpActionResult Save(SalaryDefinitionViewModel def)
        {
            SalaryDefinition definition = Mapper.Map<SalaryDefinition>(def);

            definition.CompanyId = iHUser.GetCurrentCompanyId();

            return Json(definitionService.Save(definition));
        }
    }
}
