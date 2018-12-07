namespace accounts.tac.local.Repositories
{
    using Sitecore.Security.Accounts;
    
    public interface IAccountRepository
    {
        string RestorePassword(string userName);
        void RegisterUser(string email, string password, string profileId);
        bool Exists(string userName);
        void Logout();
        User Login(string userName, string password);
    }
}