using System;
using Sitecore.Data;
using Sitecore.Sites;
using Sitecore.Data.Items;

namespace accounts.tac.local.Extensions.SitecoreExtensions
{
    public static class SiteExtensions
    {
        public static Item GetContextItem(this SiteContext site, ID derivedFromTemplateID)
        {
            if (site == null)
                throw new ArgumentNullException(nameof(site));

            var startItem = site.GetStartItem();
            return startItem?.GetAncestorOrSelfOfTemplate(derivedFromTemplateID);
        }

        public static Item GetRootItem(this SiteContext site)
        {
            if (site == null)
                throw new ArgumentNullException(nameof(site));

            return site.Database.GetItem(site.RootPath);
        }

        public static Item GetStartItem(this SiteContext site)
        {
            if (site == null)
                throw new ArgumentNullException(nameof(site));

            return site.Database.GetItem(site.StartPath);
        }
    }
}