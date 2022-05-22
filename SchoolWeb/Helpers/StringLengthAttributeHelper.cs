using System.ComponentModel.DataAnnotations;

namespace SchoolWeb.Helpers
{
    /// <summary>
    /// Customized StringLengthAttribute Class. Extends StringLengthAttribute to validate minimun and maximun length of an user input.
    /// Visit: https://stackoverflow.com/questions/18276853/string-minlength-and-maxlength-validation-dont-work-asp-net-mvc#answer-18276949
    /// </summary>
    public class StringLengthAttributeHelper : StringLengthAttribute
    {
        /// <summary>
        /// Initializes a new instance of the System.ComponentModel.DataAnnotations.StringLengthAttribute class by using a 
        ///  specified minimum and maximum length.
        /// </summary>
        /// <param name="minimumLength">The minimum length of a string.</param>
        /// <param name="maximumLength">The maximum length of a string.</param>
        public StringLengthAttributeHelper(int minimumLength, int maximumLength) : base(maximumLength)
        {
            base.MinimumLength = minimumLength;
        }

        /// <summary>
        /// Determines whether a specified object is valid
        /// </summary>
        /// <param name="value">The object to validate.</param>
        /// <returns>True if the specified object is valid; otherwise, false.</returns>
        public override bool IsValid(object? value)
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
