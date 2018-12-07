using System.Web.Mvc;
using accounts.tac.local.Models;

namespace accounts.tac.local.Extensions
{
    public static class AlertControllerExtensions
    {
        public static ViewResult InfoMessage(this Controller controller, InfoMessage infoMessage)
        {
            if (infoMessage != null)
            {
                controller.ViewData.Model = infoMessage;
            }

            return new ViewResult
            {
                ViewName = Constants.InfoMessageView,
                ViewData = controller.ViewData,
                TempData = controller.TempData,
                ViewEngineCollection = controller.ViewEngineCollection
            };
        }
    }
    
    public class Constants
    {
        public const string InfoMessageView = "~/Views/Accounts/Alerts/InfoMessage.cshtml";
    }
}