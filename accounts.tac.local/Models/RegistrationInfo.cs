using System.ComponentModel.DataAnnotations;
using accounts.tac.local.Attributes;
using accounts.tac.local.Repositories;

namespace accounts.tac.local.Models
{
    public class RegistrationInfo
    {
        [Display(Name = nameof(EmailCaption), ResourceType = typeof(RegistrationInfo))]
    [Required(ErrorMessageResourceName = nameof(Required), ErrorMessageResourceType = typeof(RegistrationInfo))]
    [EmailAddress(ErrorMessageResourceName = nameof(InvalidEmailAddress), ErrorMessageResourceType = typeof(RegistrationInfo))]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; }

    [Display(Name = nameof(PasswordCaption), ResourceType = typeof(RegistrationInfo))]
    [Required(ErrorMessageResourceName = nameof(Required), ErrorMessageResourceType = typeof(RegistrationInfo))]
    [PasswordMinLength(ErrorMessageResourceName = nameof(MinimumPasswordLength), ErrorMessageResourceType = typeof(RegistrationInfo))]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Display(Name = nameof(ConfirmPasswordCaption), ResourceType = typeof(RegistrationInfo))]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessageResourceName = nameof(ConfirmPasswordMismatch), ErrorMessageResourceType = typeof(RegistrationInfo))]
    public string ConfirmPassword { get; set; }

    public static string ConfirmPasswordCaption => "Confirm password";
    public static string EmailCaption => "E-mail";
    public static string PasswordCaption => "Password";
    public static string ConfirmPasswordMismatch => "Your password confirmation does not match. Please enter a new password.";
    public static string MinimumPasswordLength => "Please enter a password with at lease {1} characters";
    public static string Required => "Please enter a value for {0}";
    public static string InvalidEmailAddress => "Please enter a valid email address";
  }
}