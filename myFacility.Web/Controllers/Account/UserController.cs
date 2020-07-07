using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using myFacility.Models.DataObjects.User;
using myFacility.Models.Domains.Account;
using myFacility.Services.Contract;
using myFacility.Services.Handler;
using myFacility.Utilities.ExceptionUtility;
using myFacility.Utilities.WebUtility;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace myFacility.Web.Controllers.Account
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserManagementServices _userService;
        private readonly ILocationManagementService _locationService;
        private readonly StatisticsService _statisticService;
        private readonly UserManager<BtUser> _userManager;
        private readonly IMailManagementService _mailService;
        private readonly RoleManager<BtRole> _roleManager;

        public UserController(IUserManagementServices userService,
            IMailManagementService mailService,
            UserManager<BtUser> userManager,
            RoleManager<BtRole> roleManager,
            ILocationManagementService locationService,
            StatisticsService statisticsService
            )
        {
            _userManager = userManager;
            _mailService = mailService;
            _roleManager = roleManager;
            _userService = userService;
            _locationService = locationService;
            _statisticService = statisticsService;
        }
        public IActionResult Index()
        {
            return View();
        }

        #region Users
        [HttpGet("create/user")]
        public async Task<IActionResult> CreateUserView()
        {
            //ViewBag.Tenants = await _statisticService.GetTenantSelectList();
            return View();
        }

        [HttpPost("createuser")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser([Bind("tenantid,phonenumber,email,stateid,lgaid,firstname,lastname")]UserDTO model,long tenantid)
        {
            //Check the model state of the Request
            if (!ModelState.IsValid)
            {
                this.AddNotification(ResponseErrorMessageUtility.RecordNotSaved, NotificationType.ERROR);
                return View(model);
            }

            //pass the values to be created to the service
            //var create = await _userService.CreateUser(model,tenantid);

            //if result does not return 1 as string , display error to user
            //if (create.Item1 != null)
            //{
            //    //Generate Call back url
            //    var code = await _userManager.GenerateEmailConfirmationTokenAsync(create.Item1);
            //    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { UserId = create.Item1.Id, code }, protocol: Request.Scheme);
            //    await _mailService.SendWelcomeEmail(create.Item1.Email, create.Item1.UserName, create.Item2, callbackUrl);
            //    this.AddNotification(ResponseSuccessMessageUtility.RecordSaved, NotificationType.SUCCESS);
            //    return RedirectToAction(nameof(Users));
            //}
            this.AddNotification(ResponseErrorMessageUtility.RecordNotSaved, NotificationType.ERROR);
            return View(model);
        }

        [HttpGet("users")]
        public IActionResult Users()
        {     
            return View();
        }

        [HttpGet("travellers")]
        public IActionResult Travellers()
        {
            return View();
        }
        #endregion

        #region Supervisors
        //[HttpGet("addsupervisorpartial")]
        //public async Task<IActionResult> AddSupervisorPartial()
        //{
        //    ViewBag.Users = (await _userService.GetAllUsers()).Where(x => x.usernname != "admin").Select(x => new SelectListItem
        //    {
        //        Value = x.userid.ToString(),
        //        Text = x.FullName
        //    });
        //    return PartialView("_AddSupervisorPartial");
        //}

        //[HttpPost("createsupervisor")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> CreateSupervisor(SupervisorDTO model, long userextid)
        //{
        //    //Check the model state of the Request
        //    if (!ModelState.IsValid)
        //    {
        //        return View();
        //    }

        //    var create = await _userService.AddSingleSupervisor(model, userextid);

        //    //if result does not return 1 as string , display error to user
        //    if (!string.IsNullOrEmpty(create) && create != "1")
        //    {
        //        this.AddNotification(ResponseErrorMessageUtility.RecordNotSaved, NotificationType.WARNING);
        //        return RedirectToAction(nameof(Supervisors));
        //    }

        //    //else redirect back to Course grid 
        //    this.AddNotification(ResponseSuccessMessageUtility.RecordSaved, NotificationType.SUCCESS);
        //    return RedirectToAction(nameof(Supervisors));
        //}
        #endregion

        #region Regulator
        //[HttpGet("regulator")]
        //public async Task<IActionResult> Regulator()
        //{
        //    var fetchdata = await _regulatorService.GetRegulator();
        //    return View(fetchdata);
        //}



        //[HttpPost("manageregulator")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> ManageRegulator(RegulatorViewModel model, IFormFile file)
        //{
        //    //Check the model state of the Request
        //    if (!ModelState.IsValid)
        //    {
        //        return View();
        //    }

        //    if (file != null && file.Length > 0 && !string.IsNullOrEmpty(file.Name))
        //    {
        //        //
        //    }
        //    else
        //    {
        //        RegulatorDTO regulator = new RegulatorDTO
        //        {
        //            address = model.address,
        //            email = model.email,
        //            name = model.name,
        //            regcode = model.regcode
        //        };
        //        var check = await _regulatorService.Checkexist();
        //        if (check == true)
        //        {

        //            var modify = await _regulatorService.UpdateRegulator(regulator);
        //        }
        //        else
        //        {
        //            var modify = await _regulatorService.CreateRegulator(regulator);
        //        }
        //    }
        //    this.AddNotification(ResponseSuccessMessageUtility.RecordSaved, NotificationType.SUCCESS);
        //    return RedirectToAction(nameof(Regulator));
        //}

        #endregion

        #region My Profile
        [HttpGet("myprofile")]
        public IActionResult MyProfile()
        {
            return View();
        }

        [HttpGet("EditMyDetailsPartial")]
        public async Task<IActionResult> EditMyDetailsPartial(string id)
        {
            var fetchstate = await _locationService.GetStates();
            ViewBag.State = (fetchstate.Where(x => x.countrycode == "NG")).Select(x => new SelectListItem
            {
                Value = x.stateid.ToString(),
                Text = x.name
            });
            var fetchdata = await _userService.GetUser(id);
            return PartialView("_EditMyDetailsPartial", fetchdata);
        }

        [HttpPost("updatemyprofile")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateMyProfile(UserViewModel model)
        {
            //Check the model state of the Request
            if (!ModelState.IsValid)
            {
                return View();
            }

            var create = await _userService.UpdateMyProfileAsync(model);

            //if result does not return 1 as string , display error to user
            if (!string.IsNullOrEmpty(create) && create != "1")
            {
                this.AddNotification(create, NotificationType.WARNING);
                return RedirectToAction(nameof(MyProfile));
            }

            //else redirect back to Course grid 
            this.AddNotification(ResponseSuccessMessageUtility.RecordSaved, NotificationType.SUCCESS);
            return RedirectToAction(nameof(MyProfile));
        }

        [HttpPost]
        public async Task<IActionResult> UserProfileImage(IFormFile file)
        {
            //Check the model state of the Request
            if (!ModelState.IsValid)
            {
                return View();
            }
            //get the username
            var name = User.Identity.IsAuthenticated ? User.Identity.Name : "Anonymous";
            if (file.Length > 0 && !string.IsNullOrEmpty(file.FileName) && (file.FileName.EndsWith(".jpg") || file.FileName.EndsWith(".png")))
            {
                var currentDirectory = Directory.GetCurrentDirectory();
                //To avoid disparity in time of upload do this second
                string sWebRootFoldeer = Path.Combine(currentDirectory + @"\wwwroot\images\users");
                Directory.CreateDirectory(sWebRootFoldeer);
                var filename = name.Replace(" ", "_") + Path.GetExtension(file.FileName);
                using var fileStream = new FileStream(Path.Combine(sWebRootFoldeer, filename), FileMode.Create);
                await file.CopyToAsync(fileStream);
                return Ok("Image Successfully Updated");
            }
            return BadRequest("Image Not Updated Updated");

        }
        #endregion
    }
}