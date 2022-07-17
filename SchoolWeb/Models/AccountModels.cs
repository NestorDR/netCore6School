using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Common.Library.Helpers;

namespace SchoolWeb.Models
{
    // Visit:
    // https://code-maze.com/identity-asp-net-core-project/
    // https://www.c-sharpcorner.com/article/custom-login-register-with-identity-in-asp-net-core-3-1/

    public class User : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }

    public class RegisterModel
    {
        [Column(TypeName = "nvarchar")]
        // Data Annotation Attribute "Name" to set Label and Error messages
        // Data Annotation Attribute "Prompt to set Placeholder
        [Display(Name = "First name", Prompt = "Enter your first name")]
        [MaxLength(100)]                                        // MaxLength is used for the EF to decide how large to make a string value field when it creates the DB.
        [Required(ErrorMessage = "First name is required.")]
        [StringLengthAttributeHelper(minimumLength: 1, maximumLength: 100)]   // StringLengthAttributeHelper extends StringLength base data annotation and will be used for validation of user input.
        public string FirstName { get; set; } = string.Empty;

        [Column(TypeName = "nvarchar")]
        // Data Annotation Attribute "Name" to set Label and Error messages
        // Data Annotation Attribute "Prompt to set Placeholder
        [Display(Name = "Last name", Prompt = "Enter your last name")]
        [MaxLength(100)]                                        // MaxLength is used for the EF to decide how large to make a string value field when it creates the DB.
        [Required(ErrorMessage = "Last name is required.")]
        [StringLengthAttributeHelper(minimumLength: 1, maximumLength: 100)]   // StringLengthAttributeHelper extends StringLength base data annotation and will be used for validation of user input.
        public string LastName { get; set; } = string.Empty;
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

        public string ReturnUrl { get; set; } = "./login";
    }

    public class LoginModel
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
        [Display(Name = "Password", Prompt = "Please enter a password.")]
        [Required(ErrorMessage = "Password is required.")]
        public string Password { get; set; } = string.Empty;

        [Display(Name = "Remember me")]
        public bool RememberMe { get; set; }
        
        public IList<AuthenticationScheme> ExternalLogins { get; set; } = new List<AuthenticationScheme>();
        public string ReturnUrl { get; set; } = "./register";

    }

    public class ForgotPasswordModel
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

    }

}
