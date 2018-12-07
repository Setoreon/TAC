namespace accounts.tac.local.Services
{
    public interface INotificationService
    {
        void SendPassword(string email, string newPassword);
    }
}