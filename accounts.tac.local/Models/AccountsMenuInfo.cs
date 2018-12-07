using System.Web.Mvc;

namespace accounts.tac.local.Models
{
    public class AccountsMenuInfo
    {
        public bool IsLoggedIn { get; set; }
        public LoginInfo LoginInfo { get; set; }
        public string UserFullName { get; set; }
        public string UserEmail { get; set; }
        public string AccountsDetailsPageUrl { get; set; }
    }
}