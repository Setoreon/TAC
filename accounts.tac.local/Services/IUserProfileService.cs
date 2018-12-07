using System.Collections.Generic;
using System.Web.Mvc;
using accounts.tac.local.Models;
using Sitecore.Security;
using Sitecore.Security.Accounts;

namespace accounts.tac.local.Services
{
    public interface IUserProfileService
    {
        string GetUserDefaultProfileId();
        EditProfile GetEmptyProfile();
        EditProfile GetProfile(User user);
        void SaveProfile(UserProfile userProfile, EditProfile model);
    }
}