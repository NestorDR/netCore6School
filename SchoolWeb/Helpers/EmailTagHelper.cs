using Microsoft.AspNetCore.Razor.TagHelpers;

namespace SchoolWeb.Helpers
{
    /// <summary>
    /// Customized TagHelper class. Displays an email link using an anchor tag (<a>)
    /// Visit: https://docs.microsoft.com/en-us/aspnet/core/mvc/views/tag-helpers/authoring?view=aspnetcore-6.0
    /// </summary>
    public class EmailTagHelper : TagHelper
    {
        /// <summary>
        /// Email to use. Can be passed via <email mail-to="..." />. 
        /// PascalCase gets translated into kebab-case.
        /// </summary>
        public string MailTo { get; set; } = "";

        /// <summary>
        /// Synchronously executes the Microsoft.AspNetCore.Razor.TagHelpers.TagHelper with the given context and output.
        /// </summary>
        /// <param name="context">Contains information associated with the current HTML tag.</param>
        /// <param name="output">A stateful HTML element used to generate an HTML tag.</param>
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            // Configure email tag helper
            output.TagName = "a";

            // Use the output parameter to get contents of the HTML element.
            // GetChildContentAsync returns a Task containing the TagHelperContent.
            var content = await output.GetChildContentAsync();

            // Set attributes
            output.Attributes.SetAttribute("href", $"mailto:{MailTo}");
            output.Attributes.Add("id", "email-id");

            //  If content  were empty, set it with email address
            if (string.IsNullOrWhiteSpace(content.GetContent())) output.Content.SetContent(MailTo);
        }
    }
}
