using events.tac.local.Models;
using Sitecore.ContentSearch;
using Sitecore.ContentSearch.Linq;
using Sitecore.Mvc.Presentation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore;

namespace events.tac.local.Business
{
    public class EventsProvider
    {
        private const int PageSize = 4;

        public EventsList GetEventsList(int pageNo)
        {
            var indexname = $"events_{RenderingContext.Current.ContextItem.Database.Name.ToLower()}_index";
            var index = ContentSearchManager.GetIndex(indexname);
            using (var context = index.CreateSearchContext())
            {
                var results = context.GetQueryable<EventDetails>()
                    .Where(i => i.Paths.Contains(RenderingContext.Current.ContextItem.ID))
                    .Where(i => i.Language == RenderingContext.Current.ContextItem.Language.Name)
                    .Page(pageNo, PageSize)
                    .GetResults();
                return new EventsList
                {
                    Events = results.Hits.Select(h => h.Document).ToArray(),
                    TotalResultCount = results.TotalSearchResults,
                    PageSize = PageSize
                };
            }
        }

        public EventsList FindEvents(int pageNo = 0, int pageSize = 4, string name = "")
        {
            pageSize = pageSize > 10 ? 10 : pageSize;
            pageSize = pageSize < 1 ? 1 : pageSize;

            var indexname = $"events_{Context.Database.Name.ToLower()}_index";
            var index = ContentSearchManager.GetIndex(indexname);
            using (var context = index.CreateSearchContext())
            {
                var query = context.GetQueryable<EventDetails>()
                    .Where(i => i.Language == Context.Language.Name);

                if (!string.IsNullOrEmpty(name))
                {
                    query = query.Where(i => i.ContentHeading.Contains(name));
                }

                var results = query.Page(pageNo, pageSize).GetResults();

                return new EventsList
                {
                    Events = results.Hits.Select(h => h.Document).ToArray(),
                    TotalResultCount = results.TotalSearchResults,
                    PageSize = PageSize
                };
            }
        }
    }
}