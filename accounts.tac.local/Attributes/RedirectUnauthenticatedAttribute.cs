using System.Web.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Sitecore.DependencyInjection;
using accounts.tac.local.Services;
using Sitecore;

namespace accounts.tac.local.Attributes
{
    public class RedirectUnauthenticatedAttribute : ActionFilterAttribute, IAuthorizationFilter
    {
        private readonly IGetRedirectUrlService getRedirectUrlService;

        public RedirectUnauthenticatedAttribute() : this(ServiceLocator.ServiceProvider.GetService<IGetRedirectUrlService>())
        {
            
        }

        public RedirectUnauthenticatedAttribute(IGetRedirectUrlService getRedirectUrlService)
        {
            this.getRedirectUrlService = getRedirectUrlService;
        }

        public void OnAuthorization(AuthorizationContext context)
        {
            if (Context.User.IsAuthenticated)
            {
                return;
            }
            var link = this.getRedirectUrlService.GetRedirectUrl(AuthenticationStatus.Unauthenticated, context.HttpContext.Request.RawUrl);
            context.Result = new RedirectResult(link);
        }
    }
}