using System.Collections.Generic;
using Sitecore.Security;

namespace accounts.tac.local.Services
{
    public interface IUserProfileProvider
    {
        IDictionary<string, string> GetCustomProperties(UserProfile userProfile);
        void SetCustomProfile(UserProfile userProfile, IDictionary<string, string> properties);
    }
}