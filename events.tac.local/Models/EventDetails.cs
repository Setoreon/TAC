using Sitecore.Web.UI.WebControls;
using Sitecore.ContentSearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Sitecore.ContentSearch.SearchTypes;
using Sitecore.Links;

namespace events.tac.local.Models
{
    public class EventDetails : SearchResultItem
    {
        public string ContentHeading { get; set; }
        public string ContentIntro { get; set; }
        public DateTime StartDate { get; set; }
        public int LikeCount { get; set; }
        public string Url_ => LinkManager.GetItemUrl(GetItem());
        public HtmlString EventImage =>
            new HtmlString(FieldRenderer.Render(GetItem(), "EventImage", "DisableWebEditing=true&mw=150"));
    }
}