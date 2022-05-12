using System.ComponentModel.DataAnnotations;

namespace SchoolWeb.Helper
{
    /// <summary>
    /// Customized StringLengthAttribute Class
    /// Visit: https://stackoverflow.com/questions/18276853/string-minlength-and-maxlength-validation-dont-work-asp-net-mvc#answer-18276949
    /// </summary>
    public class StringLengthCustomAttribute : StringLengthAttribute
    {
        public StringLengthCustomAttribute(int maximumLength) : base(maximumLength)
        {
        }

        public override bool IsValid(object value)
        {
            string val = $"{value}";
                
            if (val.Length < base.MinimumLength)
                base.ErrorMessage = $"Minimum length should be {base.MinimumLength}";
            if (val.Length > base.MaximumLength)
                base.ErrorMessage = $"Maximum length should be {base.MaximumLength}";

            return base.IsValid(value);
        }
    }
}
