using System.Web.Mvc;

namespace accounts.tac.local.Services
{
    public interface IGetRedirectUrlService
    {
        string GetRedirectUrl(AuthenticationStatus status, string returnUrl = null);
        string GetDefaultRedirectUrl(AuthenticationStatus status);
    }
}