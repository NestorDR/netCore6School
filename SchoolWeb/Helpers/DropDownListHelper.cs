using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolWeb.Helpers
{
    public static class DropDownListHelper
    {
        /// <summary>
        /// Itemlist with the gender of the people
        /// </summary>
        /// <returns>Item-list [("Female", "F)", ("Male", "M)", ("Other", "X)"]</returns>
        public static IEnumerable<SelectListItem> GetGenderList()
        {
            return new[] {
                new SelectListItem() { Text = "Select gender", Value = null },
                new SelectListItem() { Text = "Female", Value = "F" },
                new SelectListItem() { Text = "Male", Value = "M" },
                new SelectListItem() { Text = "Other", Value = "X" }
            };
        }

        /// <summary>
        /// In the itemlist, searches the Text for the item Value received in the input parameter. If the item Value is not founded, it will return the default text.
        /// </summary>
        /// <param name="itemlist">Itemlist where search.</param>
        /// <param name="value">Item value to match</param>
        /// <param name="defaultText">Value to return if the item value is not founded</param>
        /// <returns>The item Text for the item Value received.</returns>
        public static string SearchText(IEnumerable<SelectListItem>? itemlist, string? value, string defaultText = "")
        {
            if (itemlist == null) return defaultText;

            foreach (SelectListItem item in itemlist)
                if (item.Value == value) return item.Text;

            return defaultText;
        }
    }
}
