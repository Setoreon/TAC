using System.Web.Mvc;
using Sitecore.DependencyInjection;
using accounts.tac.local.Services;
using Microsoft.Extensions.DependencyInjection;
using Sitecore;

namespace accounts.tac.local.Attributes
{

    public class RedirectAuthenticatedAttribute : ActionFilterAttribute
    {
        private readonly IGetRedirectUrlService getRedirectUrlService;

        public RedirectAuthenticatedAttribute() : this(ServiceLocator.ServiceProvider.GetService<IGetRedirectUrlService>())
        {
        }

        public RedirectAuthenticatedAttribute(IGetRedirectUrlService getRedirectUrlService)
        {
            this.getRedirectUrlService = getRedirectUrlService;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!Context.PageMode.IsNormal)
                return;
            if (!Context.User.IsAuthenticated)
                return;
            var link = this.getRedirectUrlService.GetRedirectUrl(AuthenticationStatus.Authenticated);
            filterContext.Result = new RedirectResult(link);
        }
    }
}