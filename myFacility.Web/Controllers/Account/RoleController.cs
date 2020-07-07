//using Microsoft.AspNetCore.Authorization;
//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Logging;
//using myFacility.Models.DataObjects.Account;
//using myFacility.Services.Contract;
//using myFacility.Services.Handler;
//using myFacility.Utilities.ExceptionUtility;
//using myFacility.Utilities.WebUtility;
//using System.Linq;
//using System.Threading.Tasks;

//namespace myFacility.Web.Controllers.Account
//{
//    [Authorize]
//    public class RoleController : Controller
//    {
//        private readonly IAccountManagementService _accountRepo;
//        private readonly ILogger<RoleController> _logger;
//        private readonly StatisticsService _statisticsService;

//        public RoleController(IAccountManagementService accountRepo, ILogger<RoleController> logger,
//            StatisticsService statisticsService)
//        {
//            _accountRepo = accountRepo;
//            _logger = logger;
//            _statisticsService = statisticsService;
//        }
//        public IActionResult Index()
//        {
//            return View();
//        }

//        #region Role
//        public async Task<IActionResult> AddRole()
//        {
//           // ViewBag.Tenants = await _statisticsService.GetTenantSelectList();
//            return PartialView("_AddRolePartial");
//        }
//        /// <summary>
//        /// Create New Role
//        /// </summary>
//        /// <param name="model"></param>
//        /// <returns></returns>
//        [HttpPost("CreateRole")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> CreateRole([Bind("rolename,roledesc,tenantid")]RoleDTO model, long tenantid)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View(model);
//            }

//            var create = await _accountRepo.CreateRoleAsync(model, tenantid);
//            //if result does not return 1 as string , display error to user
//            if (!string.IsNullOrEmpty(create) && create != "1")
//            {
//                return RedirectToAction(nameof(GetAllRoles));
//            }

//            //else redirect back to Course grid 
//            return RedirectToAction(nameof(GetAllRoles));
//        }

//        /// <summary>
//        /// Retrieve All Active Roles
//        /// </summary>
//        /// <returns></returns>
//        [HttpGet]
//        [Route("getallroles")]
//        public IActionResult GetAllRoles()
//        {
//            return View();
//        }

//        /// <summary>
//        /// Retrieve Single Role
//        /// </summary>
//        /// <param name="roleid"></param>
//        /// <returns></returns>
//        [HttpGet("getrole")]
//        public async Task<IActionResult> GetRolePartial(string id)
//        {
//            var getData = await _accountRepo.GetRole(id);
//            return PartialView("_GetRolePartial", getData);
//        }

//        /// <summary>
//        /// Retrieve Single Role
//        /// </summary>
//        /// <param name="roleid"></param>
//        /// <returns></returns>
//        [HttpGet("updaterole")]
//        public async Task<IActionResult> UpdateRolePartial(string id)
//        {
//            ViewBag.Tenants = await _statisticsService.GetTenantSelectList();
//            if (HttpContext.User?.FindFirst(PermissionConstants.TenantID).Value == "0")
//            {
//                var getData = await _accountRepo.GetAllTenantRole(id);
//                return PartialView("_UpdateRoleIIPartial", getData);      
//            }
//            else
//            {
//                var getData = await _accountRepo.GetRole(id);
//                return PartialView("_UpdateRolePartial", getData);
//            }
//        }
//        /// <summary>
//        /// Update Role
//        /// </summary>
//        /// <param name="id"></param>
//        /// <param name="model"></param>
//        /// <returns></returns>
//        [HttpPost("updaterole")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> UpdateRole([Bind("roleid,rolename,roledesc,tenantid")]RoleViewModel model, long tenantid)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View(model);
//            }

//            //pass the values to be created to the service
//            RoleDTO rolemodel = new RoleDTO
//            {
//                rolename = model.rolename,
//                roledesc = model.roledesc,
//            };

//            var update = await _accountRepo.UpdateRoleAsync(rolemodel, model.roleid, tenantid);
//            //if result does not return 1 as string , display error to user
//            if (!string.IsNullOrEmpty(update) && update != "1")
//            {
//                this.AddNotification(ResponseErrorMessageUtility.RecordNotSaved, NotificationType.WARNING);
//                return RedirectToAction(nameof(GetAllRoles));
//            }

//            //else redirect back to Institution grid 
//            this.AddNotification(ResponseSuccessMessageUtility.RecordSaved, NotificationType.SUCCESS);
//            return RedirectToAction(nameof(GetAllRoles));
//        }

//        /// <summary>
//        /// Update Role
//        /// </summary>
//        /// <param name="id"></param>
//        /// <param name="model"></param>
//        /// <returns></returns>
//        [HttpPost("Mupdaterole")]
//        [ValidateAntiForgeryToken]
//        public async Task<IActionResult> MUpdateRole([Bind("roleid,rolename,roledesc,tenantid")]AllTenantRoleViewModel model)
//        {
//            if (!ModelState.IsValid)
//            {
//                return View(model);
//            }

//            //pass the values to be created to the service
//            RoleDTO rolemodel = new RoleDTO
//            {
//                rolename = model.rolename,
//                roledesc = model.roledesc,
//            };

//            var update = await _accountRepo.UpdateRoleAsync(rolemodel, model.roleid, model.tenantid);
//            //if result does not return 1 as string , display error to user
//            if (!string.IsNullOrEmpty(update) && update != "1")
//            {
//                this.AddNotification(ResponseErrorMessageUtility.RecordNotSaved, NotificationType.WARNING);
//                return RedirectToAction(nameof(GetAllRoles));
//            }

//            //else redirect back to Institution grid 
//            this.AddNotification(ResponseSuccessMessageUtility.RecordSaved, NotificationType.SUCCESS);
//            return RedirectToAction(nameof(GetAllRoles));
//        }
//        public async Task<IActionResult> Manage_role_users(string id)
//        {
//            var u2role = await _accountRepo.GetAssignedAndUnAssignedUserToRole(id);
//            return View("manage_role_users", u2role);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        //[Authorize(nameof(PermissionEnum.AddRoleToPermission))]
//        public async Task<IActionResult> Assign_user2_role([Bind("Chkbx,RoleId,RoleName")] string[] Chkbx, string RoleId, string RoleName)
//        {
//            if (ModelState.IsValid)
//            {
//                var asign = await _accountRepo.AssignUsersToRole(RoleId, Chkbx);

//                if (!string.IsNullOrEmpty(asign) && asign != "1")
//                {
//                    this.AddNotification(ResponseErrorMessageUtility.RecordNotSaved, NotificationType.WARNING);
//                    return RedirectToAction("manage_role_users", new { id = RoleId });
//                }

//                this.AddNotification($"Application Role: {RoleName}, {Chkbx.Count()} associated Users was added Successfully", NotificationType.SUCCESS);
//                return RedirectToAction("manage_role_users", new { id = RoleId });
//            }
//            this.AddNotification($"Bad Request", NotificationType.ERROR);
//            return RedirectToAction("manage_role_users", new { id = RoleId });
//        }


//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        //[Authorize(nameof(PermissionEnum.RemoveUserRole))]
//        public async Task<IActionResult> Remove_role_users([Bind("Chkbx,RoleId,RoleName")] string[] Chkbx, string RoleId, string RoleName)
//        {
//            if (ModelState.IsValid)
//            {
//                var asign = await _accountRepo.RemoveUsersFromRole(RoleId, Chkbx);
//                if (!string.IsNullOrEmpty(asign) && asign != "1")
//                {
//                    this.AddNotification(ResponseErrorMessageUtility.RecordNotSaved, NotificationType.WARNING);
//                    return RedirectToAction(nameof(GetAllRoles));
//                }
//                this.AddNotification($"Application Role: {RoleName}, {Chkbx.Count()} associated User(s) was removed Successfully", NotificationType.SUCCESS);
//                return RedirectToAction("manage_role_users", new { id = RoleId });
//            }
//            this.AddNotification($"Bad Request", NotificationType.ERROR);
//            return RedirectToAction("manage_role_users", new { id = RoleId });
//        }
//        #endregion

//        [HttpGet("managerolepermissions")]
//        public async Task<IActionResult> Manage_Role_Permissions(string id)
//        {
//            var perroles = await _accountRepo.GetMerchantPermissionsByRole(id);
//            return View("manage_role_permissions", perroles);
//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        //[Authorize(nameof(GetSkilledPermissions.AddRoleToPermission))]
//        public async Task<IActionResult> Assign_Permissions([Bind("Chkbx,RoleId,RoleName")] string[] Chkbx, string RoleId, string RoleName)
//        {
//            if (!ModelState.IsValid)
//            {
//                this.AddNotification($"Bad Request", NotificationType.ERROR);
//                return RedirectToAction("manage_role_permissions", new { id = RoleId });
//            }

//            if (Chkbx.Count() > 0)
//            {
//                var perms = await _accountRepo.AddPermissionsToRole(RoleId, Chkbx);
//                if (!string.IsNullOrEmpty(perms) && perms != "1")
//                {
//                    this.AddNotification(ResponseErrorMessageUtility.RecordNotSaved, NotificationType.WARNING);
//                    return RedirectToAction(nameof(Manage_Role_Permissions));
//                }
//                this.AddNotification($"Application Role: {RoleName}, {Chkbx.Count()} associated Permissions was added Successfully", NotificationType.SUCCESS);
//                return RedirectToAction("manage_role_permissions", new { id = RoleId });
//            }
//            this.AddNotification($"Sorry, Role or Associated Permission cannot be empty", NotificationType.WARNING);
//            return RedirectToAction("manage_role_permissions", new { id = RoleId });


//        }

//        [HttpPost]
//        [ValidateAntiForgeryToken]
//        //[Authorize(nameof(PermissionEnum.RemoveRolePermissions))]
//        public async Task<IActionResult> Remove_Permissions([Bind("Chkbx,RoleId,RoleName")] string[] Chkbx, string RoleId, string RoleName)
//        {
//            if (!ModelState.IsValid)
//            {
//                this.AddNotification($"Bad Request", NotificationType.ERROR);
//                return RedirectToAction("manage_role_permissions", new { id = RoleId });
//            }
//            if (Chkbx.Count() > 0)
//            {
//                var perms = await _accountRepo.RemovePermissionsFromRole(RoleId, Chkbx);
//                if (!string.IsNullOrEmpty(perms) && perms != "1")
//                {
//                    this.AddNotification(ResponseErrorMessageUtility.RecordNotSaved, NotificationType.WARNING);
//                    return RedirectToAction(nameof(Manage_Role_Permissions));
//                }
//                this.AddNotification($"Application Role: {RoleName}, {Chkbx.Count()} associated Permissions was removed Successfully", NotificationType.SUCCESS);
//                return RedirectToAction("manage_role_permissions", new { id = RoleId });
//            }
//            this.AddNotification($"Sorry, Role or Associated Permission cannot be empty", NotificationType.WARNING);
//            return RedirectToAction("manage_role_permissions", new { id = RoleId });

//        }
//    }
//}