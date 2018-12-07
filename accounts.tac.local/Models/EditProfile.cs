using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using accounts.tac.local.Repositories;

namespace accounts.tac.local.Models
{
    public class EditProfile
    {
        [Display(Name = nameof(EmailCaption), ResourceType = typeof(EditProfile))]
        public string Email { get; set; }

        [Display(Name = nameof(FirstNameCaption), ResourceType = typeof(EditProfile))]
        public string FirstName { get; set; }

        [Display(Name = nameof(LastNameCaption), ResourceType = typeof(EditProfile))]
        public string LastName { get; set; }

        [Display(Name = nameof(PhoneNumberCaption), ResourceType = typeof(EditProfile))]
        [RegularExpression(@"^\+?\d*(\(\d+\)-?)?\d+(-?\d+)+$", ErrorMessageResourceName = nameof(PhoneNumberFormat), ErrorMessageResourceType = typeof(EditProfile))]
        [MaxLength(20, ErrorMessageResourceName = nameof(MaxLengthExceeded), ErrorMessageResourceType = typeof(EditProfile))]
        public string PhoneNumber { get; set; }

        [Display(Name = nameof(InterestsCaption), ResourceType = typeof(EditProfile))]
        public string Interest { get; set; }

        public IEnumerable<string> InterestTypes { get; set; }
        
        public static string EmailCaption => "E-mail";
        public static string FirstNameCaption =>"First name";
        public static string LastNameCaption => "Last name";
        public static string PhoneNumberCaption => "Phone number";
        public static string InterestsCaption => "Interests";
        public static string MaxLengthExceeded => "{0} length should be less than {1}";
        public static string PhoneNumberFormat => "Phone number should contain only +, ( ) and digits";
    }
}