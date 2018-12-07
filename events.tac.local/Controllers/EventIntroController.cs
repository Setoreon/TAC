using System;
using events.tac.local.Models;
using Sitecore.Mvc.Presentation;
using Sitecore.Web.UI.WebControls;
using System.Web;
using System.Web.Mvc;
using events.tac.local.Business;
using Sitecore;

namespace events.tac.local.Controllers
{
    public class EventIntroController : Controller
    {
        private readonly LikeService _likeService;

        public EventIntroController() : this(new LikeService())
        {
        }

        public EventIntroController(LikeService likeService)
        {
            _likeService = likeService;
        }


        public ActionResult Index()
        {
            bool isLiked = false;
            try
            {
                isLiked = _likeService.IsLiked(RenderingContext.Current.ContextItem.ID.ToString(),
                    Context.User.Profile);
            }
            catch (Exception)
            {
                // ignored
            }

            ViewBag.IsLiked = isLiked;
            return View(CreateModel());
        }

        public static EventIntro CreateModel()
        {
            var item = RenderingContext.Current.ContextItem;

            return new EventIntro
            {
                ContentHeading = new HtmlString(FieldRenderer.Render(item, "ContentHeading")),
                ContentBody = new HtmlString(FieldRenderer.Render(item, "ContentBody")),
                EventImage = new HtmlString(FieldRenderer.Render(item, "EventImage", "mw=400")),
                Highlights = new HtmlString(FieldRenderer.Render(item, "Highlights")),
                ContentIntro = new HtmlString(FieldRenderer.Render(item, "ContentIntro")),
                StartDate = new HtmlString(FieldRenderer.Render(item, "StartDate")),
                Duration = new HtmlString(FieldRenderer.Render(item, "Duration")),
                DifficultyLevel = new HtmlString(FieldRenderer.Render(item, "DifficultyLevel")),
                LikeCount = new HtmlString(FieldRenderer.Render(item, "LikeCount"))
            };
        }
    }
}