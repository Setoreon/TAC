using System.Collections.Generic;
using Sitecore.Data.Items;

namespace accounts.tac.local.Services
{
    public interface IProfileSettingsService
    {
        IEnumerable<string> GetInterests();
        Item GetUserDefaultProfile();
    }
}