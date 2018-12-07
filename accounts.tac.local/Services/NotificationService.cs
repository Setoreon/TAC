using accounts.tac.local.Attributes;
using Sitecore;
//using Sitecore.Foundation.DependencyInjection;

namespace accounts.tac.local.Services
{
    [Service(typeof(INotificationService))]
    public class NotificationService : INotificationService
    {
        private readonly IAccountsSettingsService _siteSettings;

        public NotificationService(IAccountsSettingsService siteSettings)
        {
            _siteSettings = siteSettings;
        }

        public void SendPassword(string email, string newPassword)
        {
            var mail = _siteSettings.GetForgotPasswordMailTemplate();
            mail.To.Add(email);
            mail.Body = mail.Body.Replace("$password$", newPassword);

            MainUtil.SendMail(mail);
        }
    }
}