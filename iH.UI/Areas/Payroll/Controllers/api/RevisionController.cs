namespace iH.UI.Areas.Payroll.Controllers.api
{
    using System;
    using System.Web.Http;

    using Domain.Security.Entities;
    using Domain.Payroll.Services;

    public class RevisionController : ApiController
    {
        private readonly IRevisionService revisionService;

        public RevisionController(IRevisionService revisionService)
        {
            this.revisionService = revisionService;
        }

        [Route("api/Revision/List")]
        [Authorize(Roles = PermissionLevels.PayrollManager)]
        [HttpGet]
        public IHttpActionResult List(Int64 id)
        {
            var result = revisionService.GetRevisionByEmployee(id);
            return Json(result);
        }

        [Route("api/Revision/Details")]
        [Authorize(Roles = PermissionLevels.PayrollManager)]
        [HttpGet]
        public IHttpActionResult Details(Int64 id)
        {
            var result = revisionService.GetRevisionDetails(id);
            return Json(result);
        }
    }
}
