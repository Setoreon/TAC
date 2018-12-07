using System.Collections.Generic;
using System.Linq;
using accounts.tac.local.Attributes;
using accounts.tac.local.Extensions.SitecoreExtensions;
using Sitecore;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Diagnostics;
using Sitecore.SecurityModel;

namespace accounts.tac.local.Services
{
    [Service(typeof(IProfileSettingsService))]
    public class ProfileSettingsService : IProfileSettingsService
    {
        public virtual Item GetUserDefaultProfile()
        {
            using (new SecurityDisabler())
            {
                var database = Database.GetDatabase(Settings.ProfileItemDatabase);
                var targetItem = database.GetItem(Templates.UserProfileDefault);

                return targetItem;
            }
            
            /*using (new SecurityDisabler())
            {
                var item = GetSettingsItem();
                Assert.IsNotNull(item, "Page with profile settings isn't specified");
                var database = Database.GetDatabase(Settings.ProfileItemDatabase);
                var profileField = item.Fields[Templates.ProfileSettigs.Fields.UserProfile];
                var targetItem = database.GetItem(profileField.Value);

                return targetItem;
            }*/
        }

        public virtual IEnumerable<string> GetInterests()
        {
            var item = GetSettingsItem();

            return item?.TargetItem(Templates.ProfileSettigs.Fields.InterestsFolder)?.Children
                       ?.Select(i => i.Fields[Templates.Interest.Fields.Title].Value) ?? Enumerable.Empty<string>();
        }

        private static Item GetSettingsItem()
        {
            return Context.Database.GetItem(Context.Site.RootPath + "/Repository/Profile Settings");
        }
    }
}