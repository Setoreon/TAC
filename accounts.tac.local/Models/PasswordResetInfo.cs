using System.ComponentModel.DataAnnotations;
using accounts.tac.local.Repositories;

namespace accounts.tac.local.Models
{
    public class PasswordResetInfo
    {
        /*[Display(Name = nameof(EmailCaption), ResourceType = typeof(PasswordResetInfo))]
        [Required(ErrorMessageResourceName = nameof(Required), ErrorMessageResourceType = typeof(PasswordResetInfo))]
        [EmailAddress(ErrorMessageResourceName = nameof(InvalidEmailAddress), ErrorMessageResourceType = typeof(PasswordResetInfo))]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }*/
        
        public string UserName { get; set; }

        //public static string EmailCaption => "E-mail";
        //public static string InvalidEmailAddress => "Please enter a valid email address";
        
        public static string UserNameCaption => "UserName";
        public static string Required => "Please enter a value for {0}";
        public static string InvalidUserName => "Please enter a valid username";   
    }
}