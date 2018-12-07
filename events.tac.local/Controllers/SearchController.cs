using System.Linq;
using System.Web.Mvc;
using events.tac.local.Business;

namespace events.tac.local.Controllers
{
    public class SearchController : Controller
    {
        private readonly EventsProvider _provider;

        public SearchController() : this(new EventsProvider())
        {
        }

        public SearchController(EventsProvider provider)
        {
            _provider = provider;
        }
        
        [HttpGet]
        public ActionResult Index(int page = 1, string name = "")
        {
            page = page < 1 ? 1 : page;
            return View(string.IsNullOrEmpty(name)
                ? _provider.GetEventsList(page - 1)
                : _provider.FindEvents(page - 1, name: name));
        }
        
        [HttpPost]
        public JsonResult GetEvents(string name, int number = 3)
        {
            var events = _provider.FindEvents(0, number, name);
            return Json(new
            {
                number = events.Events.Count(),
                events = events.Events.ToList().ConvertAll(e => new {name = e.Name})
            });
        }
    }
}