using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using SwachhBharatAbhiyan.CMS.Models;
using SwachBharat.CMS.Bll.Services.Support;
using SwachhBharatAbhiyan.CMS.Models.SessionHelper;
using SwachBharat.CMS.Bll.Repository.MainRepository;
using SwachBharat.CMS.Bll.ViewModels;
using System.Web.Security;
using SwachBharat.CMS.Bll.ViewModels.MainModel;
using SwachBharat.CMS.Bll.ViewModels.ChildModel.Model;


namespace SwachhBharatAbhiyan.CMS.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private IMainRepository mainrepository;

       
        public AccountController()
        {
           // AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            mainrepository = new MainRepository();
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public async Task<ActionResult> Login(string returnUrl)
        {
            if (returnUrl !=null)
            {
                LoginViewModel model = new LoginViewModel( );
                model.Email = returnUrl;
                   model.Password = "Admin#123";
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, change to shouldLockout: true
                var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
                var UserDetails = await UserManager.FindAsync(model.Email, model.Password);

                switch (result)
                {

                    case SignInStatus.Success:
                        if (UserDetails != null)
                        {
                            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                            var identity = await UserManager.CreateIdentityAsync(UserDetails, DefaultAuthenticationTypes.ApplicationCookie);
                            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, identity);

                            string UserId = UserDetails.Id;
                            string UserRole = UserManager.GetRoles(UserId).FirstOrDefault();
                            string UserEmail = UserDetails.Email;
                            string UserName = identity.Name;

                            Logger.WriteInfoMessage("User  " + model.Email + " is successfully login with user id :" + UserId + " ,User Role : " + UserRole + " ,Email ID :" + UserEmail);

                            AddSession(UserId, UserRole, UserEmail, UserName,model.Type);

                        }
                        return RedirectToLocal(returnUrl);
                    case SignInStatus.LockedOut:
                        return View("Lockout");
                    case SignInStatus.RequiresVerification:
                        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                    case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError("", "Invalid login attempt.");
                        return View(model);
                }


            }

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {

              if (model.AType == "SA")
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }


                //   LoginViewModel Result= new LoginViewModel();
                EmployeeVM Result = new EmployeeVM();
                Result.ADUM_LOGIN_ID = model.Email;
                Result.ADUM_PASSWORD = model.Password;
                Result.AD_USER_TYPE = model.Type;
                Result = mainrepository.LoginSA(Result);
                //var UserDetails = await UserManager.FindAsync(model.Email, model.Password);
                switch (Result.status)
                {
                    case "Success":
                        //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                        //var identity = await UserManager.CreateIdentityAsync(UserDetails, DefaultAuthenticationTypes.ApplicationCookie);
                        //AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, identity);
                        //string UserId = UserDetails.Id;
                        //string UserRole = UserManager.GetRoles(UserId).FirstOrDefault();
                        //string UserEmail = UserDetails.Email;
                        //string UserName = identity.Name;
                        Session["status"] = "Success";
                        TempData["status"] = "Success";
                        Session["ADUM_USER_NAME"] = Result.ADUM_USER_NAME;
                        TempData["ADUM_USER_NAME"] = Result.ADUM_USER_NAME;
                        AddSessionSA(Result.ADUM_USER_CODE.ToString(), Result.AD_USER_TYPE_ID.ToString(), Result.ADUM_LOGIN_ID, Result.ADUM_USER_NAME, Result.APP_ID.ToString(),Result.PRABHAG_ID.ToString());
                        Session["UserID"] = Result.ADUM_USER_CODE.ToString();
                        Session["LoginId"] = Result.ADUM_LOGIN_ID.ToString();
                        Session["PrabhagId"] = Result.PRABHAG_ID;
                        Session["UserProfile"] = Result;
                       
                       
                       return RedirectToComLocal(returnUrl,Result.AD_USER_TYPE);
                       
                    case "LockedOut":
                        return View("Lockout");
                    case "RequiresVerification":

                    case "Failure":
                    default:
                        ModelState.AddModelError("", "Invalid login attempt.");
                        return View(model);

                }
            }
            if (model.Type == "W")
            {

                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, change to shouldLockout: true
                var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
                var UserDetails = await UserManager.FindAsync(model.Email, model.Password);


                switch (result)
                {

                    case SignInStatus.Success:
                        if (UserDetails != null)
                        {
                            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                            var identity = await UserManager.CreateIdentityAsync(UserDetails, DefaultAuthenticationTypes.ApplicationCookie);
                            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, identity);

                            string UserId = UserDetails.Id;
                            string UserRole = UserManager.GetRoles(UserId).FirstOrDefault();
                            string UserEmail = UserDetails.Email;
                            string UserName = identity.Name;
                            Session["PrabhagId"] = 0;

                            Logger.WriteInfoMessage("User  " + model.Email + " is successfully login with user id :" + UserId + " ,User Role : " + UserRole + " ,Email ID :" + UserEmail);

                            AddSession(UserId, UserRole, UserEmail, UserName, model.Type);

                        }
                        return RedirectToLocal(returnUrl);
                    case SignInStatus.LockedOut:
                        return View("Lockout");
                    case SignInStatus.RequiresVerification:
                        return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                    case SignInStatus.Failure:
                    default:
                        ModelState.AddModelError("", "Invalid login attempt.");
                        return View(model);


                }
              

            }
           else if (model.Type == "S")
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }


                //   LoginViewModel Result= new LoginViewModel();
                EmployeeVM Result = new EmployeeVM();
                Result.ADUM_LOGIN_ID = model.Email;
                Result.ADUM_PASSWORD = model.Password;
                Result = mainrepository.LoginStreet(Result);
                //var UserDetails = await UserManager.FindAsync(model.Email, model.Password);
                switch (Result.status)
                {
                    case "Success":
                        //AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                        //var identity = await UserManager.CreateIdentityAsync(UserDetails, DefaultAuthenticationTypes.ApplicationCookie);
                        //AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, identity);
                        //string UserId = UserDetails.Id;
                        //string UserRole = UserManager.GetRoles(UserId).FirstOrDefault();
                        //string UserEmail = UserDetails.Email;
                        //string UserName = identity.Name;
                        Session["status"] = "Success";
                        TempData["status"] = "Success";
                        TempData["ADUM_USER_NAME"] = Result.ADUM_USER_NAME;
                        AddSessionStreet(Result.ADUM_USER_CODE.ToString(), Result.AD_USER_TYPE_ID.ToString(), Result.ADUM_LOGIN_ID, Result.ADUM_USER_NAME, Result.APP_ID.ToString());
                        Session["UserID"] = Result.ADUM_USER_CODE.ToString();
                        Session["LoginId"] = Result.ADUM_LOGIN_ID.ToString();
                        Session["UserProfile"] = Result;
                        Session["PrabhagId"] = 0;
                        return RedirectToLocalStreet(returnUrl);

                    case "LockedOut":
                        return View("Lockout");
                    case "RequiresVerification":

                    case "Failure":
                    default:
                        ModelState.AddModelError("", "Invalid login attempt.");
                        return View(model);

                }
            }
           else if (model.Type == "L")
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }


             //   LoginViewModel Result= new LoginViewModel();
                EmployeeVM Result = new EmployeeVM();
                Result.ADUM_LOGIN_ID = model.Email;
                Result.ADUM_PASSWORD = model.Password;
                Result = mainrepository.Login(Result);
           
                switch (Result.status)
                {
                    case "Success":
                        Session["status"] = "Success";
                        TempData["status"] = "Success";
                        TempData["ADUM_USER_NAME"] = Result.ADUM_USER_NAME;
                        AddSession(Result.ADUM_USER_CODE.ToString(), Result.AD_USER_TYPE_ID.ToString(), Result.ADUM_LOGIN_ID, Result.ADUM_USER_NAME, Result.APP_ID.ToString());
                        Session["UserID"] = Result.ADUM_USER_CODE.ToString();
                        Session["LoginId"] = Result.ADUM_LOGIN_ID.ToString();
                        Session["UserProfile"] = Result;
                        Session["PrabhagId"] = 0;
                        return RedirectToLocalLiquid(returnUrl);

                    case "LockedOut":
                        return View("Lockout");
                    case "RequiresVerification":
                   
                    case "Failure":
                    default:
                        ModelState.AddModelError("", "Invalid login attempt.");
                        return View(model);

                }

            }

           return View(model);
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
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:  model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);
                    
                    // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    return RedirectToAction("Index", "Home");
                }
                AddErrors(result);
            }

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
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit http://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                // string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                // var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                // await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                // return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
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
            var user = await UserManager.FindByNameAsync(model.Email);
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
            Session["__MySession__"] = null; //it's my session variable
            Session["PrabhagId"] = 0;
            Session["LoginId"] = "";
            Session.Clear();
            Session.Abandon();
            FormsAuthentication.SignOut(); //you write this when you use FormsAuthentication
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            AddSession(null,null,null,null,null);
            return RedirectToAction("Login", "Account");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
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

        private ActionResult RedirectToLocalLiquid(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Liquid/LiquidHome");
        }

        private ActionResult RedirectToLocalStreet(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Street/StreetHome");
        }

        private ActionResult RedirectToComLocal(string returnUrl,string CType)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            if(CType=="L")
            {
                return RedirectToAction("Index", "Liquid/LiquidHome");
            } 
            else if (CType == "S")
            {
                return RedirectToAction("Index", "Street/StreetHome");
            }
            else
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

        #region CustomChanges
        [NonAction]
        private void AddSession(string UserId, string UserRole, string UserEmail, string UserName, string usertype)
        {
            try
            {
                int AppId = 0;
                int PrabhagId = 0;
                if(usertype=="W")
                {
                    AppId = mainrepository.GetUserAppId(UserId);
                }
                else
                {
                    AppId = mainrepository.GetUserAppIdL(usertype);
                }
                
                if (AppId!=0)
                {
                AppDetailsVM ApplicationDetails = mainrepository.GetApplicationDetails(AppId);
                string DB_Connect = mainrepository.GetDatabaseFromAppID(AppId);
                SessionHandler.Current.UserId = UserId;
                SessionHandler.Current.UserRole = UserRole;
                SessionHandler.Current.UserEmail = UserEmail;
                SessionHandler.Current.UserName = UserName;
                SessionHandler.Current.AppId = ApplicationDetails.AppId;
                SessionHandler.Current.AppName = ApplicationDetails.AppName;
                SessionHandler.Current.IsLoggedIn = true;
                SessionHandler.Current.Type = ApplicationDetails.Type;
                SessionHandler.Current.Latitude = ApplicationDetails.Latitude;
                SessionHandler.Current.Logitude = ApplicationDetails.Logitude;
                SessionHandler.Current.DB_Name = DB_Connect;
                SessionHandler.Current.YoccClientID = ApplicationDetails.YoccClientID;
                SessionHandler.Current.GramPanchyatAppID = ApplicationDetails.GramPanchyatAppID;
                SessionHandler.Current.YoccFeddbackLink = ApplicationDetails.YoccFeddbackLink;
                SessionHandler.Current.YoccDndLink = ApplicationDetails.YoccDndLink;
                //HttpContext.Session["Apikey"] = (string.IsNullOrEmpty(ApplicationDetails.Apikey)) ? "" : ApplicationDetails.Apikey;

                }
                else
                {
                    SessionHandler.Current.UserId =null;
                    SessionHandler.Current.UserRole = null;
                    SessionHandler.Current.UserEmail = null;
                    SessionHandler.Current.UserName = null;
                    SessionHandler.Current.AppId =0;
                    SessionHandler.Current.PrabhagId = PrabhagId;
                    SessionHandler.Current.AppName = null;
                    SessionHandler.Current.IsLoggedIn = false;
                    SessionHandler.Current.Type = null;
                    //HttpContext.Session["Apikey"] = "";

                }
                // if (SessionHandler.Current.Type.Trim() == "np")
                // {
                //     SessionHandler.Current.sessionType = "नगर पंचायत | Our Nagar Panchayat";
                // }
                // else
                //if (SessionHandler.Current.Type.Trim() == "npp")
                // {
                //     SessionHandler.Current.sessionType = "नगरपरिषद | Municipal Council";
                // }
                // else
                //     if (SessionHandler.Current.Type.Trim() == "gp")
                // {
                //     SessionHandler.Current.sessionType = "ग्रामपंचायत | Gram Panchayat";
                // }
                // else
                // {
                //     SessionHandler.Current.sessionType = "ग्रामपंचायत | Gram Panchayat";
                // }

            }
            catch (Exception exception)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(exception);
            }
        }

        private void AddSessionStreet(string UserId, string UserRole, string UserEmail, string UserName, string usertype)
        {
            try
            {
                int AppId = mainrepository.GetUserAppIdSS(usertype);
                if (AppId != 0)
                {
                    AppDetailsVM ApplicationDetails = mainrepository.GetApplicationDetails(AppId);
                    string DB_Connect = mainrepository.GetDatabaseFromAppID(AppId);
                    SessionHandler.Current.UserId = UserId;
                    SessionHandler.Current.UserRole = UserRole;
                    SessionHandler.Current.UserEmail = UserEmail;
                    SessionHandler.Current.UserName = UserName;
                    SessionHandler.Current.AppId = ApplicationDetails.AppId;
                    SessionHandler.Current.AppName = ApplicationDetails.AppName;
                    SessionHandler.Current.IsLoggedIn = true;
                    SessionHandler.Current.Type = ApplicationDetails.Type;
                    SessionHandler.Current.Latitude = ApplicationDetails.Latitude;
                    SessionHandler.Current.Logitude = ApplicationDetails.Logitude;
                    SessionHandler.Current.DB_Name = DB_Connect;
                    SessionHandler.Current.YoccClientID = ApplicationDetails.YoccClientID;
                    SessionHandler.Current.GramPanchyatAppID = ApplicationDetails.GramPanchyatAppID;
                    SessionHandler.Current.YoccFeddbackLink = ApplicationDetails.YoccFeddbackLink;
                    SessionHandler.Current.YoccDndLink = ApplicationDetails.YoccDndLink;
                    //HttpContext.Session["Apikey"] = (string.IsNullOrEmpty(ApplicationDetails.Apikey)) ? "" : ApplicationDetails.Apikey;

                }
                else
                {
                    SessionHandler.Current.UserId = null;
                    SessionHandler.Current.UserRole = null;
                    SessionHandler.Current.UserEmail = null;
                    SessionHandler.Current.UserName = null;
                    SessionHandler.Current.AppId = 0;
                    SessionHandler.Current.AppName = null;
                    SessionHandler.Current.IsLoggedIn = false;
                    SessionHandler.Current.Type = null;
                    //HttpContext.Session["Apikey"] = "";

                }
                

            }
            catch (Exception exception)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(exception);
            }
        }

        private void AddSessionSA(string UserId, string UserRole, string UserEmail, string UserName, string usertype, string PrabhagId)
        {
            try
            {
                int AppId = mainrepository.GetUserAppIdSA(usertype);
                if (AppId != 0)
                {
                    AppDetailsVM ApplicationDetails = mainrepository.GetApplicationDetails(AppId);
                    string DB_Connect = mainrepository.GetDatabaseFromAppID(AppId);
                    SessionHandler.Current.UserId = UserId;
                    SessionHandler.Current.UserRole = UserRole;
                    SessionHandler.Current.UserEmail = UserEmail;
                    SessionHandler.Current.UserName = UserName;
                    SessionHandler.Current.AppId = ApplicationDetails.AppId;
                    SessionHandler.Current.PrabhagId = Convert.ToInt32(PrabhagId);
                    SessionHandler.Current.AppName = ApplicationDetails.AppName;
                    SessionHandler.Current.IsLoggedIn = true;
                    SessionHandler.Current.Type = ApplicationDetails.Type;
                    SessionHandler.Current.Latitude = ApplicationDetails.Latitude;
                    SessionHandler.Current.Logitude = ApplicationDetails.Logitude;
                    SessionHandler.Current.DB_Name = DB_Connect;
                    SessionHandler.Current.YoccClientID = ApplicationDetails.YoccClientID;
                    SessionHandler.Current.GramPanchyatAppID = ApplicationDetails.GramPanchyatAppID;
                    SessionHandler.Current.YoccFeddbackLink = ApplicationDetails.YoccFeddbackLink;
                    SessionHandler.Current.YoccDndLink = ApplicationDetails.YoccDndLink;
                    //HttpContext.Session["Apikey"] = (string.IsNullOrEmpty(ApplicationDetails.Apikey)) ? "" : ApplicationDetails.Apikey;

                }
                else
                {
                    SessionHandler.Current.UserId = null;
                    SessionHandler.Current.UserRole = null;
                    SessionHandler.Current.UserEmail = null;
                    SessionHandler.Current.UserName = null;
                    SessionHandler.Current.AppId = 0;
                    SessionHandler.Current.PrabhagId = 0;
                    SessionHandler.Current.AppName = null;
                    SessionHandler.Current.IsLoggedIn = false;
                    SessionHandler.Current.Type = null;
                    //HttpContext.Session["Apikey"] = "";

                }


            }
            catch (Exception exception)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(exception);
            }
        }

        #endregion

    }
}