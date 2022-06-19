using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using SchoolWeb.Helpers;

namespace SchoolWeb.Models
{
    public class RegisterModel
    {
        [DataType(DataType.EmailAddress)]
        // Data Annotation Attribute "Name" to set Label and Error messages
        // Data Annotation Attribute "Prompt to set Placeholder
        [Display(Name = "E-Mail", Prompt = "Enter your e-mail address.")]
        [DisplayFormat(ConvertEmptyStringToNull = true)]
        [EmailAddress(ErrorMessage = "Invalid Email.")]         // Redounds with RegularExpression, but it is only as an example
        [MaxLength(100)]
        [RegularExpression(@"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-*)|(\w+\.))*\w+\.[a-zA-Z]{2,6}$",
            ErrorMessage = "Email appears to be invalid.")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "E-Mail is required.")]
       public string Email { get; set; } = string.Empty;

        [DataType(DataType.Password)]
        [Display(Name = "Password", Prompt = "Please enter a strong password.")]
        [Required(ErrorMessage = "Password is required.")]
        [StringLengthAttributeHelper(minimumLength: 3, maximumLength: 100)]   // StringLengthAttributeHelper extends StringLength base data annotation and will be used for validation of user input.
        public string Password { get; set; } = string.Empty;

        [Compare("Password", ErrorMessage = "The password and the password confirmation do not match.")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm", Prompt = "Please confirm your password.")]
        [Required(ErrorMessage = "Password confirmation is required.")]
        [StringLengthAttributeHelper(minimumLength: 3, maximumLength: 100)]   // StringLengthAttributeHelper extends StringLength base data annotation and will be used for validation of user input.
        public string ConfirmPassword { get; set; } = string.Empty;

        public IList<AuthenticationScheme> ExternalLogins { get; set; } = new List<AuthenticationScheme>();

        public string ReturnUrl { get; set; } = "/Home";
    }
}
