using System;
using System.Web.Mvc;
using events.tac.local.Business;
using Sitecore;

namespace events.tac.local.Controllers
{
    public class LikeController : Controller
    {
        private readonly LikeService _likeService;

        public LikeController() : this(new LikeService())
        {
        }

        public LikeController(LikeService likeService)
        {
            _likeService = likeService;
        }

        [HttpPost]
        public JsonResult Like(string itemID)
        {
            if (!Context.User.IsAuthenticated) return Json(new { error = "Current user is not authenticated" });
            
            int result;
            bool isLiked;
            try
            {
                if (_likeService.IsLiked(itemID, Context.User?.Profile))
                {
                    result = _likeService.Unlike(itemID, Context.User?.Profile);
                    isLiked = false;
                }
                else
                {
                    result = _likeService.Like(itemID, Context.User?.Profile);
                    isLiked = true;
                }
            }
            catch (Exception e)
            {
                return Json(new { error = e.Message });
            }

            return Json(new { itemID = itemID, count = result, isLiked = isLiked });
        }

        [HttpGet]
        public JsonResult IsLiked(string itemID)
        {
            if (!Context.User.IsAuthenticated) return Json(new { error = "Current user is not authenticated" });

            bool result;
            try
            {
                result = _likeService.IsLiked(itemID, Context.User?.Profile);
            }
            catch (Exception e)
            {
                return Json(new { error = e.Message });
            }

            return Json(new { itemID = itemID, isLiked = result });
        }
    }
}