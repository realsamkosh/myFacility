using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using myFacility.Models.DataObjects.Account;
using myFacility.Models.Domains.Account;
using myFacility.Services.Contract;
using myFacility.Utilities.AuthenticationUtility.AuthUser;
using myFacility.Utilities.WebUtility;

namespace myFacility.Web.Controllers.Account
{
    public class AccountController : Controller
    {
        private readonly UserManager<BtUser> _userManager;
        private readonly SignInManager<BtUser> _signInManager;
        private readonly RoleManager<BtRole> _roleManager;
        private readonly IDistributedCache _memoryCache;
        private readonly IMailManagementService _emailSender;
        private readonly IUserManagementServices _userService;
        private readonly ILocationManagementService _locationService;
        private readonly ILogger _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ISeedingManagementService _seedingService;
        private readonly IAccountManagementService _accountService;
        private readonly IMailManagementService _mailService;
        private readonly IAuthUser _authUser;

        public AccountController(UserManager<BtUser> userManager, RoleManager<BtRole> roleManager,
            SignInManager<BtUser> signInManager, IMailManagementService emailSender,
            ILogger<AccountController> logger, IHttpContextAccessor httpContextAccessor,
            IDistributedCache memoryCache, IUserManagementServices userService, ILocationManagementService locationService,
            ISeedingManagementService seedingService, IAccountManagementService accountService,
            IMailManagementService mailService, IAuthUser authUser)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _memoryCache = memoryCache;
            _emailSender = emailSender;
            _userService = userService;
            _locationService = locationService;
            _signInManager = signInManager;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
            _seedingService = seedingService;
            _accountService = accountService;
            _mailService = mailService;
            _authUser = authUser;
        }
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            await _seedingService.AutoSeedGlobalAdmin();

            //Check if User is Authenticated
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToLocal(returnUrl);
            }
            // Clear the existing external cookie to ensure a clean login process
            //if (HttpContext.Session.IsAvailable)
            //{
            //    await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            //    HttpContext.Session.Clear();
            //}

            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }


        [HttpPost]
        [AllowAnonymous]
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> Login(LoginDTO model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //var tenant = RouteData.Values.SingleOrDefault(r => r.Key == "tenantid");

            //Transform the login details
            model.username = model.username;

            //Verify Username or Email
            var auser = await _userService.CheckUser(model.username);
            if (auser == null)
            {
                this.AddNotification("Invalid login attempt. please try again!", NotificationType.ERROR);
                _logger.LogWarning("Invalid login attempt. please try again!");
                return View(model);
            }
            else if (!(await _userManager.IsEmailConfirmedAsync(auser)))
            {
                this.AddNotification("Invalid login attempt. please try again!", NotificationType.ERROR);
                return View(model);
            }
            else if (auser.UserName.ToLower() != "admin" || auser.Email.ToLower() != "admin@metafora.com")
            {
                //Get Institution Users
                var userri = (await _userService.GetUsers()).Where(v => v.usernname.ToLower() == model.username.ToLower()).FirstOrDefault();
                if (userri.isactive == false)
                {
                    return RedirectToAction(nameof(Lockout));
                }
                else if (userri.usercategory == "V")
                {

                }
                else if (userri.usercategory == "T")
                {

                    return RedirectToAction(nameof(Lockout));
                }
            }

            await _userManager.UpdateSecurityStampAsync(auser);


            //var Lockoutcheck = _context.TSysConfigurationCaps.Where(x => x.ConfigId == 5009 && x.Enabled == 1).Single();
            //if (!string.IsNullOrEmpty(Lockoutcheck.ConfigValue) && int.TryParse(Lockoutcheck.ConfigValue, out int n) == true)
            //{
            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            var result = await _signInManager.PasswordSignInAsync(auser.UserName, model.password, isPersistent: false, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                //Create Last Login Date
                await _accountService.CreateLastLoginDate(auser.Id);

                //Create Distributed Cache Cache
                // _memoryCache.SetString(CacheKeys.Entry, DateTime.UtcNow.ToString());
                //var tenantid = long.Parse(tenant.Value.ToString());
                //var appusers = (await _userService.GetTenantUsers(tenantid)).Where(x => x.appuserid == auser.Id);


                //var userid = _
                _logger.LogInformation("User logged in.");
                return RedirectToLocal(returnUrl);
            }


            if (result.IsLockedOut)
            {
                _logger.LogWarning("User account locked out.");
                return RedirectToAction(nameof(Lockout));
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                return View(model);
            }
        }


        #region Forgot Password
        [AllowAnonymous]
        [HttpGet("forgotpassword")]
        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [AllowAnonymous]
        public async Task<ActionResult> PForgotPassword(string username)
        {
            if (!string.IsNullOrEmpty(username))
            {
                var aspUser = await _userManager.FindByNameAsync(username);
                if (aspUser == null || !(await _userManager.IsEmailConfirmedAsync(aspUser)))
                {
                    this.AddNotification("Please your email or username is not valid or yet to be confirmed.", NotificationType.ERROR);
                    return RedirectToAction("login", "account");
                }
                var userInfo = _userService.GetUser(aspUser.Id);

                ////Password Generator
                //string generatePassword;

                ////Password Requirements
                //var passwrule = _userManager.Options.Password;

                ////Generate Password for User
                //generatePassword = PasswordGenerator.CreateRandom(passwrule.RequiredLength, passwrule.RequireUppercase, passwrule.RequireDigit, passwrule.RequireLowercase);

                var code = await _userManager.GeneratePasswordResetTokenAsync(aspUser);

                var callbackUrl = Url.Action("ResetPassword", "Account", new { UserId = aspUser.Id, code }, protocol: Request.Scheme);
                await _mailService.SendPasswordResetEmail(aspUser.Email, aspUser.UserName, callbackUrl, 0);
                this.AddNotification("An email notification has been sent to your email box!", NotificationType.SUCCESS);
                return RedirectToAction(nameof(ForgotPassword));
            }
            this.AddNotification("Username cannot be empty", NotificationType.WARNING);
            return RedirectToAction("login", "account");
        }
        #endregion

        #region Reset Password
        [AllowAnonymous]
        [HttpGet("resetpassword")]
        public async Task<IActionResult> ResetPassword(string code, string userid)
        {
            //Get USER ID
            var appuser = await _userManager.FindByIdAsync(userid);

            if (!string.IsNullOrEmpty(code) && !string.IsNullOrEmpty(userid) && appuser != null)
            {
                PasswordResetOuterDTO outerDTO = new PasswordResetOuterDTO
                {
                    code = code,
                    userid = userid
                };
                return View("resetpassword", outerDTO);
            }
            return NotFound();
        }

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [AllowAnonymous]
        public async Task<IActionResult> PResetPassword([Bind("password,confirmpassword,userid,code")] PasswordResetOuterDTO model)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("resetpassword", new { model.code, model.userid });
            }

            var auser = await _userManager.FindByIdAsync(model.userid);
            if (auser == null || string.IsNullOrEmpty(model.code))
            {
                ModelState.AddModelError(string.Empty, "Account verification Failed.");
                this.AddNotification("Account verification Failed.", NotificationType.ERROR);
                return View(model);
            }

            //var npass = _userManager.PasswordHasher.HashPassword(auser, model.password);
            var verifywithoriginal = _userManager.PasswordHasher.VerifyHashedPassword(auser, auser?.PasswordHash, model.password);
            //var passHistory = _passwordService.GetUserOldPasswords(userId).Select(x => x.PwdEncrypt);
            if (model.confirmpassword != model.password)
            {
                this.AddNotification("The new password and confirmation password do not match.", NotificationType.ERROR);
                _logger.LogError("The new password and confirmation password do not match.");
                return RedirectToAction("resetpassword", new { model.code, model.userid });
            }
            else if (model.password == auser?.UserName)
            {
                //ModelState.AddModelError(string.Empty, "Sorry! You cannot use your username as password.");
                this.AddNotification("Sorry! You cannot use your username as password.", NotificationType.ERROR);
                return RedirectToAction("resetpassword", new { model.code, model.userid });
            }
            else if (model.password == auser?.Email && auser.EmailConfirmed == true)
            {
                this.AddNotification("Sorry! You cannot use your Email as password.", NotificationType.ERROR);

                return RedirectToAction("resetpassword", new { model.code, model.userid });
            }
            else if (verifywithoriginal == PasswordVerificationResult.Success)
            {
                this.AddNotification("Your Current password cannot be set as new password!. Please Input another Password.", NotificationType.ERROR);
                return RedirectToAction("resetpassword", new { model.code, model.userid });
            }
            //else if (passHistory.Contains(npass))
            //{
            //    this.AddNotification("Your new password already exists in your history of old password please choose another password combination!", NotificationType.ERROR);

            //    //ModelState.AddModelError(string.Empty, "Your new password already exists in your history of old password please choose another password combination!");
            //    //if it fails generate a new token for the user
            //    var resettoken = await _userManager.GenerateTwoFactorTokenAsync(auser, "OneSumXLoginTotpProvider");
            //    return RedirectToAction("user_ochangePassword", new { pkt = resettoken, username });
            //}
            else
            {
                //Update the Identity Password 
                var passwordchange = await _userManager.ResetPasswordAsync(auser, model.code, model.password);
                if (passwordchange.Succeeded)
                {

                    this.AddNotification($"New Password was successfully set.", NotificationType.SUCCESS);
                    return RedirectToAction("login", "account");
                }
                AddErrors(passwordchange);
                return RedirectToAction("resetpassword", new { model.code, model.userid });
            }
            //// If we got this far, something failed, redisplay form
            //this.AddNotification($"Internal Server Error! Please Try again Later", NotificationType.ERROR);
            //return RedirectToAction("login", "account");
        }

        #endregion

        #region In App Password Change
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Profile_ChangePassword([Bind("CurrentPassword, Password, ConfirmPassword")] PasswordInAppResetDTO model)
        {

            if (!ModelState.IsValid)
            {
                // If we got this far, something failed, redisplay form
                return RedirectToAction(nameof(UserController.MyProfile));
            }

            if (model.password != model.confirmpassword)
            {
                this.AddNotification("The new password and confirmation password do not match.", NotificationType.ERROR);
                // Activity Logging format
                _logger.LogError("The new password and confirmation password do not match.");
                return RedirectToAction(nameof(UserController.MyProfile));
            }
            else
            {
                var user = await _userManager.FindByIdAsync(_authUser.UserId);

                //var Oldpassword = _passwordService.GetUserOldPasswords(model.UserId).Select(x => x.PwdEncrypt);
                //var oldPass = _passwordService.GetUserOldPasswords(model.UserId).Where(x => x.PwdEncrypt == model.CurrentPassword);
                //if (Oldpassword.Contains(_userManager.PasswordHasher.HashPassword(user, model.Password)))
                //{
                //    this.AddNotification("Your new password already exists in your history of old password please choose another password combination!", NotificationType.WARNING);
                //    return RedirectToAction(nameof(UserController.MyProfile));
                //}

                //Update the Identity Password 
                var passwordchange = await _userManager.ChangePasswordAsync(user, model.currentpassword, model.password);
                if (passwordchange.Succeeded)
                {
                    this.AddNotification($"New Password was successfully set. Please Re-login", NotificationType.SUCCESS);

                    await _signInManager.SignOutAsync();
                    _logger.LogInformation("User logged out.");
                    return RedirectToAction(nameof(AccountController.Login), "Account");
                }
                AddErrors(passwordchange);
            }
            return View("Error");

        }
        #endregion

        public IActionResult Admin()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Regcomplete()
        {
            return View();
        }

        public IActionResult Recoverpw()
        {
            return View();
        }

        [AllowAnonymous]
        public async Task<IActionResult> ConfirmEmail(string userid, string callcode)
        {
            var user = await _userManager.FindByIdAsync(userid);
            if (user == null)
            {
                return NotFound();
            }

            var confirm = await _userManager.ConfirmEmailAsync(user, callcode);
            if (confirm.Succeeded)
            {
                return View();
            }
            else
                return RedirectToAction(nameof(EmailConfirmed));

        }

        [AllowAnonymous]
        public IActionResult EmailConfirmed()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            //await _memoryCache.RemoveAsync(CacheKeys.Entry);
            await _signInManager.SignOutAsync();
            _logger.LogInformation("User logged out.");
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        #region Helpers

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(DashboardController.Index), "Dashboard");
            }
        }
        #endregion
    }
}
