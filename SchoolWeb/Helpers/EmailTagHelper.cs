using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SchoolWeb.Helpers
{
    /// <summary>
    /// Customized TagHelper class. Shows an email link using an anchor tag (<a>)
    /// </summary>
    public class EmailTagHelper : TagHelper
    {
        /// <summary>
        /// Content to show
        /// </summary>
        
        public string Content { get; set; } = "";
        /// <summary>
        /// Email to use
        /// </summary>
        public string Email { get; set; } = "";

        /// <summary>
        /// Synchronously executes the Microsoft.AspNetCore.Razor.TagHelpers.TagHelper with the given context and output.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            // Configure email tag helper
            output.TagName = "a";
            output.Attributes.SetAttribute("href", $"mailto:{Email}");
            output.Attributes.Add("id", "email-id");
            
            // Process attributes
            if (string.IsNullOrWhiteSpace(Content)) Content = Email;
            output.Content.SetContent(Content);
            
            //base.Process(context, output);
        }
    }
}
