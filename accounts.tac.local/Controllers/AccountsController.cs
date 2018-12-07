using System;
using System.Web.Mvc;
using System.Web.Security;
using accounts.tac.local.Attributes;
using accounts.tac.local.Extensions;
using accounts.tac.local.Extensions.SitecoreExtensions;
using accounts.tac.local.Models;
using accounts.tac.local.Repositories;
using accounts.tac.local.Services;
using Sitecore;
using Sitecore.Diagnostics;
//using Sitecore.Foundation.Alerts.Extensions;
//using Sitecore.Foundation.Alerts.Models;
//using Sitecore.Foundation.SitecoreExtensions.Attributes;

namespace accounts.tac.local.Controllers
{   
    public class AccountsController : Controller
    {
        private readonly IFedAuthLoginButtonRepository _fedAuthLoginRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly INotificationService _notificationService;
        private readonly IAccountsSettingsService _accountsSettingsService;
        private readonly IGetRedirectUrlService _getRedirectUrlService;
        private readonly IUserProfileService _userProfileService;
        
        private static string UserAlreadyExistsError => "A user with specified e-mail address already exists";
        private static string ForgotPasswordEmailNotConfigured => "The Forgot Password E-mail has not been configured";
        private static string UserDoesNotExistError => "User with specified e-mail address does not exist";
        
        public AccountsController(IAccountRepository accountRepository, INotificationService notificationService, IAccountsSettingsService accountsSettingsService, IGetRedirectUrlService getRedirectUrlService, IUserProfileService userProfileService, IFedAuthLoginButtonRepository fedAuthLoginRepository)
        {
            _fedAuthLoginRepository = fedAuthLoginRepository;
            _accountRepository = accountRepository;
            _notificationService = notificationService;
            _accountsSettingsService = accountsSettingsService;
            _getRedirectUrlService = getRedirectUrlService;
            _userProfileService = userProfileService;
        }
        
        public ActionResult AccountsMenu()
        {
            var isLoggedIn = Context.IsLoggedIn;// && Context.PageMode.IsNormal;
            var accountsMenuInfo = new AccountsMenuInfo
            {
                IsLoggedIn = isLoggedIn,
                LoginInfo = !isLoggedIn ? CreateLoginInfo() : null,
                UserFullName = isLoggedIn ? Context.User.Profile.FullName : null,
                UserEmail = isLoggedIn ? Context.User.Profile.Email : null,
                AccountsDetailsPageUrl = _accountsSettingsService.GetPageLinkOrDefault(Context.Item, Templates.AccountsSettings.Fields.AccountsDetailsPage)
            };
            return View(accountsMenuInfo);
        }
        
        private LoginInfo CreateLoginInfo(string returnUrl = null)
        {
            return new LoginInfo
            {
                ReturnUrl = returnUrl,
                LoginButtons = _fedAuthLoginRepository.GetAll()
            };
        }

        [HttpGet]
        [RedirectAuthenticated]
        public ActionResult Register()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateModel]
        [RedirectAuthenticated]
        [ValidateRenderingId]
        public ActionResult Register(RegistrationInfo registrationInfo)
        {
            if (_accountRepository.Exists(registrationInfo.Email))
            {
                ModelState.AddModelError(nameof(registrationInfo.Email), UserAlreadyExistsError);
                return View(registrationInfo);
            }

            try
            {
                _accountRepository.RegisterUser(registrationInfo.Email, registrationInfo.Password, _userProfileService.GetUserDefaultProfileId());

                var link = _getRedirectUrlService.GetRedirectUrl(AuthenticationStatus.Authenticated);
                return Redirect(link);
            }
            catch (MembershipCreateUserException ex)
            {
                Log.Error($"Can't create user with {registrationInfo.Email}", ex, this);
                ModelState.AddModelError(nameof(registrationInfo.Email), ex.Message);

                return View(registrationInfo);
            }
        }

        [RedirectAuthenticated]
        public ActionResult Login(string returnUrl = null)
        {
            return View(CreateLoginInfo(returnUrl));
        }

        public ActionResult LoginTeaser()
        {
            return View();
        }

        [HttpPost]
        [ValidateModel]
        [ValidateRenderingId]
        public ActionResult Login(LoginInfo loginInfo)
        {
            return Login(loginInfo, redirectUrl => new RedirectResult(redirectUrl));
        }

        protected virtual ActionResult Login(LoginInfo loginInfo, Func<string, ActionResult> redirectAction)
        {
            var user = _accountRepository.Login(loginInfo.UserName, loginInfo.Password);
            if (user == null)
            {
                ModelState.AddModelError("invalidCredentials", "Username or password is not valid.");
                return View(loginInfo);
            }

            var redirectUrl = loginInfo.ReturnUrl;
            if (string.IsNullOrEmpty(redirectUrl))
            {
                redirectUrl = _getRedirectUrlService.GetRedirectUrl(AuthenticationStatus.Authenticated);
            }

            return redirectAction(redirectUrl);
        }

        [HttpPost]
        [ValidateModel]
        public ActionResult _Login(LoginInfo loginInfo)
        {
            return Login(loginInfo, redirectUrl => Json(new LoginResult
            {
                RedirectUrl = redirectUrl
            }));
        }

        [HttpPost]
        [ValidateModel]
        public ActionResult LoginTeaser(LoginInfo loginInfo)
        {
            return Login(loginInfo, redirectUrl => new RedirectResult(redirectUrl));
        }

        [HttpPost]
        public ActionResult Logout()
        {
            _accountRepository.Logout();

            return Redirect(Context.Site.GetRootItem().Url());
        }

        [RedirectAuthenticated]
        public ActionResult ForgotPassword()
        {
            try
            {
                _accountsSettingsService.GetForgotPasswordMailTemplate();
            }
            catch (Exception)
            {
                return this.InfoMessage(InfoMessage.Error(ForgotPasswordEmailNotConfigured));
            }

            return View();
        }

        [HttpPost]
        [ValidateModel]
        [RedirectAuthenticated]
        public ActionResult ForgotPassword(PasswordResetInfo model)
        {
            if (!_accountRepository.Exists(model.UserName))
            {
                ModelState.AddModelError(nameof(model.UserName), UserDoesNotExistError);

                return View(model);
            }

            try
            {
                var newPassword = _accountRepository.RestorePassword(model.UserName);
                _notificationService.SendPassword(model.UserName, newPassword);
                return this.InfoMessage(InfoMessage.Success("Your password has been reset."));
            }
            catch (Exception ex)
            {
                Log.Error($"Can't reset password for user {model.UserName}", ex, this);
                ModelState.AddModelError(nameof(model.UserName), ex.Message);

                return View(model);
            }
        }

        [RedirectUnauthenticated]
        public ActionResult EditProfile()
        {
            /*if (!Context.PageMode.IsNormal)
            {
                return this.View(this.UserProfileService.GetEmptyProfile());
            }*/

            var profile = _userProfileService.GetProfile(Context.User);

            return View(profile);
        }

        [HttpPost]
        [RedirectUnauthenticated]
        public virtual ActionResult EditProfile(EditProfile profile)
        {
            if (!ModelState.IsValid)
            {
                return View(profile);
            }

            _userProfileService.SaveProfile(Context.User.Profile, profile);

            Session["EditProfileMessage"] = new InfoMessage("Profile was successfully updated");
            return Redirect(Request.RawUrl);
        }
    }
}