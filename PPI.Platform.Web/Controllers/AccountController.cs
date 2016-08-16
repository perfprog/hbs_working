using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using PPI.Platform.Mvc.Extentions;

namespace PPI.Platform.Web.Controllers
{
    using PPI.Platform.Web.Models;
    using PPI.Platform.Web;
    using PPI.Platform.Web.Properties;
    using PPI.Platform.Core.Domain;
    using PPI.Platform.Mvc.Attributes;
    using MvcSiteMapProvider;
    using PPI.Platform.Mvc.Controller;
    using PPI.Platform.Core.Utility;
    using System.Configuration;


    [ExportController("Account")]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class AccountController : BaseController
    {
        private ApplicationUserManager _userManager;
        
        public AccountController()
        {
        }
        
        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;            
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ??HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }
       

        //
        // GET: /Account/Login
        [AllowAnonymous]
        [RestoreModelStateFromTempData]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        private ApplicationSignInManager _signInManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set { _signInManager = value; }
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        [SetTempDataModelState]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // This doesn't count login failures towards account lockout
            // To enable password failures to trigger account lockout, change to shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            _Logging.LogInfo("User Login - {0} Result - {1}", model.Email, result.ToString());
            switch (result)
            {
                case SignInStatus.Success:
                    var CurrentUser = UserManager.Users.FirstOrDefault(m => m.Email == model.Email);
                    if (CurrentUser != null)
                    {
                        if (CurrentUser.EmailConfirmed == false)
                        {
                            ModelState.AddModelError("", "You Most Confirm Your Email Address Before Loggin In");
                            AuthenticationManager.SignOut();
                            return RedirectToAction("Login");
                        }                        
                    }
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", Resources_Web.Account_Login_SignInError);
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            var user = await UserManager.FindByIdAsync(await SignInManager.GetVerifiedUserIdAsync());
            if (user != null)
            {
                var code = await UserManager.GenerateTwoFactorTokenAsync(user.Id, provider);
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", Resources_Web.Account_Login_SignInError);
                    return View(model);
            }
        }

        //
        // GET: /Account/Register        
        [Authorize(Roles = "SiteAdmin,Admin,SuperAdmin,SystemID")]
        public ActionResult Register()
        {
            //need to figure out how to control whats availible
            var model = new RegisterViewModel();            
            var Roles = new List<SelectListItem> { };            
            Roles.Add(new SelectListItem() { Text = Resources_Web.Roles_Participant, Value = "Participant" });
            //Roles.Add(new SelectListItem() { Text = Resources_Web.Roles_Rater, Value = "Rater" });
            Roles.Add(new SelectListItem() { Text = Resources_Web.Roles_GroupAdmin, Value = "GroupAdmin" });
            //Roles.Add(new SelectListItem() { Text = Resources_Web.Roles_SiteAdmin, Value = "SiteAdmin" });
            Roles.Add(new SelectListItem() { Text = Resources_Web.Roles_Admin, Value = "Admin" });
            Roles.Add(new SelectListItem() { Text = Resources_Web.Roles_SuperAdmin, Value = "SuperAdmin" });
            Roles.Add(new SelectListItem() { Text = Resources_Web.Roles_SystemIT, Value = "SystemIT" });

            ViewBag.Roles = Roles;
            return View(model);
        }
        [HttpGet]
        [AllowAnonymous]
        [SetTempDataModelState]
        public ActionResult RegisterParticipant(int participantId, string token)
        {
            var model = new RegisterParticipantViewModel();
            model.ParticipantId = participantId;
            model.Token = token;            
            string ValidateToken;
            ViewBag.Enabled = false;
            try
            {
                ValidateToken = MachineKeyWrapper.UnprotectUrlSafeString(token, participantId.ToString());
            }
            catch
            {
                //if encryption fails for any reason don't tell them just add the model error
                _Logging.LogError("Token failed because of bad token decryption");
                _Logging.LogInfo("register failed because of bad token decryption");
                ValidateToken = "Invalid";
            }
            if (ValidateToken != "Authenticated")
            {
                ModelState.AddModelError("", "Your Security Token Is Invalid Please Contact The Administrator");                
            }
            else
            { 
                var currentPerson = _IPlatformUnitOfWork.IParticipantRepository.FirstOrDefault(m => m.Id == participantId).Person;
                model.Email = currentPerson.Email;
                if (!System.String.IsNullOrWhiteSpace(currentPerson.AspNetUsersId))
                {
                    ModelState.AddModelError("", "You have been redirected to the login page because you have successfully completed the registration process already. Click ‘Forgot Password’ below if you need to reset your password.");
                    return RedirectToAction("Login");
                }
                else
                    ViewBag.Enabled = true;
            }
            //HARD CODE PER USER REQUEST
            model.UserName = "HBS" + participantId;
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RegisterParticipant(RegisterParticipantViewModel participant)
        {
            string ValidateToken;
            try
            {
                ValidateToken = MachineKeyWrapper.UnprotectUrlSafeString(participant.Token, participant.ParticipantId.ToString());
            }
            catch
            {
                //if encryption fails for any reason don't tell them just add the model error
                ValidateToken = "Invalid";
            }
            if (ValidateToken == "Authenticated")
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser();
                    user.UserName = participant.UserName;
                    user.Email = participant.Email;
                    user.EmailConfirmed = true;
                    var result = UserManager.Create(user, participant.Password);
                    if (result.Succeeded)
                    {
                        //string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        //var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        //await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");                        
                        var result2 = UserManager.AddToRole(user.Id, "Participant");                        
                        var person = _IPlatformUnitOfWork.IParticipantRepository.AsQueryable().Where(m => m.Id == participant.ParticipantId).Select(s => s.Person).FirstOrDefault();                        
                        person.AspNetUsersId = user.Id;
                        _IPlatformUnitOfWork.Commit();                                                
                        AuthenticationManager.SignOut();
                        HttpContext.Session.Clear();
                        SignInManager.SignIn(user, true, false);
                        return RedirectToAction("Index", "Home");
                    }
                    AddErrors(result);
                    return View(participant);
                }
            }
            else
                ModelState.AddModelError("","Your Security Token Is Invalid Please Contact The Administrator");
            return View(participant);
        }
        
        // POST: /Account/Register
        [HttpPost]
        [Authorize(Roles="SiteAdmin,Admin,SuperAdmin,SystemID")]
        [ValidateAntiForgeryToken]
        [MvcSiteMapNodeAttribute(Title = "Add Users", ParentKey = "Home", Order = 10)]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);                
                if (result.Succeeded)
                {
                    var result2 = UserManager.AddToRole(user.Id,model.Role);
                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                     string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                     var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                     await UserManager.SendEmailAsync(user.Id, ConfigurationManager.AppSettings["OWINAccountSystemSubjectBusinessName"] + "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");                     
                     return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }
            var Roles = new List<SelectListItem> { };
            Roles.Add(new SelectListItem() { Text = Resources_Web.Roles_Participant, Value = "Participant" });
            //Roles.Add(new SelectListItem() { Text = Resources_Web.Roles_Rater, Value = "Rater" });
            Roles.Add(new SelectListItem() { Text = Resources_Web.Roles_GroupAdmin, Value = "GroupAdmin" });
            //Roles.Add(new SelectListItem() { Text = Resources_Web.Roles_SiteAdmin, Value = "SiteAdmin" });
            Roles.Add(new SelectListItem() { Text = Resources_Web.Roles_Admin, Value = "Admin" });
            Roles.Add(new SelectListItem() { Text = Resources_Web.Roles_SuperAdmin, Value = "SuperAdmin" });
            Roles.Add(new SelectListItem() { Text = Resources_Web.Roles_SystemIT, Value = "SystemIT" });

            ViewBag.Roles = Roles;

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.UserName);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed         
                    var ForgotPasswordConfirmationModel = new ForgotPasswordViewModel() { UserName = model.UserName, Email = model.Email };
                    return View("ForgotPasswordConfirmation", ForgotPasswordConfirmationModel);
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                 string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                 var emailmodel = new ForgotViewModel();
                 var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme); 
                 emailmodel.Link = callbackUrl;
                 emailmodel.Email = user.Email;
                 emailmodel.UserName = user.UserName;
                 var email = this.RenderPartialToString("_PartialForgotEmail", emailmodel);
                 await UserManager.SendEmailAsync(user.Id, ConfigurationManager.AppSettings["OWINAccountSystemSubjectBusinessName"] + "Forgot Password Request", email);
                 return RedirectToAction("ForgotPasswordConfirmation", "Account", emailmodel);
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation(ForgotPasswordViewModel model)
        {
            return View(model);
        }

        [AllowAnonymous]
        public ActionResult RegisterResetPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut();
            HttpContext.Session.Clear();
            return RedirectToAction("Login", "Account");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion
    }
}