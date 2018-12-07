using System;
using System.Web.Security;
using accounts.tac.local.Attributes;
using Sitecore.Diagnostics;
using accounts.tac.local.Services;
using Sitecore;
using Sitecore.Pipelines;
using Sitecore.Security.Accounts;
using Sitecore.Security.Authentication;

namespace accounts.tac.local.Repositories
{
    [Service(typeof(IAccountRepository))]
    public class AccountRepository : IAccountRepository
    {
        public string RestorePassword(string userName)
        {
            var domainName = Context.Domain.GetFullName(userName);
            var user = Membership.GetUser(domainName);
            if (user == null)
                throw new ArgumentException($"Could not reset password for user '{userName}'", nameof(userName));
            return user.ResetPassword();
        }

        public void RegisterUser(string email, string password, string profileId)
        {
            Assert.ArgumentNotNullOrEmpty(email, nameof(email));
            Assert.ArgumentNotNullOrEmpty(password, nameof(password));

            var fullName = Context.Domain.GetFullName(email);
            Assert.IsNotNullOrEmpty(fullName, "Can't retrieve full userName");

            var user = User.Create(fullName, password);
            user.Profile.Email = email;
            if (!string.IsNullOrEmpty(profileId))
            {
                user.Profile.ProfileItemId = profileId;
            }

            user.Profile.Save();
            this.Login(email, password);
        }

        public bool Exists(string userName)
        {
            var fullName = Context.Domain.GetFullName(userName);

            return User.Exists(fullName);
        }

        public void Logout()
        {
            var user = AuthenticationManager.GetActiveUser();
            AuthenticationManager.Logout();
        }

        public User Login(string userName, string password)
        {
            var accountName = userName;
            var domain = Context.Domain;
            if (domain != null)
            {
                accountName = domain.GetFullName(userName);
            }

            var result = AuthenticationManager.Login(accountName, password);
            return !result ? null : AuthenticationManager.GetActiveUser();
        }
    }
}