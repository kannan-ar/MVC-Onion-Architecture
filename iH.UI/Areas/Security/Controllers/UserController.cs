namespace iH.UI.Areas.Security.Controllers
{
    using System.Web.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using Omu.ValueInjecter;

    using Domain.Security.Entities;
    using iH.ViewModels;
    using Domain.Security.Services;
    using Domain.Employee.Services;
    using UI.ViewModels;
    using Code;

    [RouteArea("Security")]
    [RoutePrefix("User")]
    [Route("{action}")]
    public class UserController : Controller
    {
        private const int noOfRecords = 25;
        private const int displayRecords = 5;

        private readonly ISecurityUserService userService;
        private readonly IRoleService roleService;
        private readonly IUserSearchService searchService;
        private readonly IEmployeeService employeeService;

        public UserController(
            ISecurityUserService userService,
            IRoleService roleService,
            IUserSearchService searchService,
            IEmployeeService employeeService)
        {
            this.userService = userService;
            this.roleService = roleService;
            this.searchService = searchService;
            this.employeeService = employeeService;
        }

        [Route("Register/{employeeId?}")]
        [HttpGet]
        [Authorize(Roles = PermissionLevels.UserManager)]
        public ActionResult Register(int? employeeId)
        {
            ViewBag.Roles = roleService.GetRoles();
            return View();
        }

        [Route("Register/{employeeId?}")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = PermissionLevels.UserManager)]
        public ActionResult Register(int? employeeId, RegisterUserViewModel user)
        {
            ViewBag.Roles = roleService.GetRoles();

            if (ModelState.IsValid)
            {
                IList<string> messages;

                SecurityUser securityUser = Mapper.Map<RegisterUserViewModel, SecurityUser>(user);

                if (!userService.IsValidSecurityUser(securityUser, out messages))
                {
                    ViewBag.Message = messages;
                    return View();
                }

                if (employeeId != null)
                {
                    employeeService.UpdateUser(employeeId.Value, userService, securityUser, user.SelectedRoles.ToArray());
                    return RedirectToAction("List", "Employee", new { area = "HR" });
                }
                else
                {
                    userService.CreateUser(securityUser, user.SelectedRoles.ToArray());

                    return RedirectToAction("List");
                }


            }
            else
            {
                return View();
            }
        }

        [Route("List/{page?}")]
        [HttpGet]
        [Authorize(Roles = PermissionLevels.UserManager)]
        public ActionResult List(int page = 1)
        {
            int totalRecords;

            IList<SecurityUser> users = userService.GetUsers(page, noOfRecords, out totalRecords);

            ViewBag.Paging = Pagination.GetPaging(page, noOfRecords, totalRecords, displayRecords);
            return View(users);
        }

        [Route("ListUser/{userId}")]
        [Authorize(Roles = PermissionLevels.UserManager)]
        public ActionResult ListUser(int userId)
        {
            SecurityUser user = userService.GetUserById(userId);

            if (user != null)
            {
                ViewBag.SelectedUser = user;
                return View("List", new List<SecurityUser>() { user });
            }
            else
            {
                return View("List");
            }
        }

        [Route("ChangePassword/{id}")]
        [HttpGet]
        [Authorize(Roles = PermissionLevels.UserManager)]
        public ActionResult ChangePassword(int id)
        {
            SecurityUser user = userService.GetUserById(id);

            if (user == null)
            {
                return HttpNotFound("User with id = " + id.ToString() + " not found");
            }
            else
            {
                RegisterUserViewModel securityUser = Mapper.Map<SecurityUser, RegisterUserViewModel>(user);
                securityUser.Password = string.Empty;

                return View(securityUser);
            }
        }

        [Route("ChangePassword/{id}")]
        [HttpPost]
        [Authorize(Roles = PermissionLevels.UserManager)]
        public ActionResult ChangePassword(int id, RegisterUserViewModel user)
        {
            SecurityUser securityUser = Mapper.Map<RegisterUserViewModel, SecurityUser>(user);
            securityUser.UserId = id;

            userService.ChangePassword(securityUser);

            return RedirectToAction("List");
        }

        [Route("ChangeRoles/{id}")]
        [HttpGet]
        [Authorize(Roles = PermissionLevels.UserManager)]
        public ActionResult ChangeRoles(int id)
        {
            ViewBag.Roles = roleService.GetRoles();

            SecurityUser user = userService.GetUserById(id);

            if (user == null)
            {
                return HttpNotFound("User with id = " + id.ToString() + " not found");
            }
            else
            {
                UserRolesViewModel userRoles = new UserRolesViewModel();

                userRoles.User = user;
                userRoles.Roles = roleService.GetRoles();
                userRoles.SelectedRoles = userService.GetUserRoles(id).Select(r => r.RoleId).ToList();

                return View(userRoles);
            }
        }

        [Route("ChangeRoles/{id}")]
        [HttpPost]
        [Authorize(Roles = PermissionLevels.UserManager)]
        public ActionResult ChangeRoles(int id, UserRolesViewModel model)
        {
            userService.ChangeRoles(id, model.SelectedRoles.ToArray());
            return RedirectToAction("List");
        }

        [HttpGet]
        [Authorize(Roles = PermissionLevels.UserManager)]
        public ActionResult SearchUsers(string query)
        {
            return Json(searchService.GetUsersByName(query, 10), JsonRequestBehavior.AllowGet);
        }
    }
}