using Microsoft.AspNetCore.Mvc.Rendering;

namespace SchoolWeb.Helper
{
    public static class DropDownListHelper
    {
        /// <summary>
        /// Itemlist with the gender of the people
        /// </summary>
        /// <returns>Itemlist [("Female", "F)", ("Mmale", "M)", ("Other", "X)"]</returns>
        public static IEnumerable<SelectListItem> GetGenderList()
        {
            return new[] {
                new SelectListItem() { Text = "Select gender", Value = null },
                new SelectListItem() { Text = "Female", Value = "F" },
                new SelectListItem() { Text = "Male", Value = "M" },
                new SelectListItem() { Text = "Other", Value = "X" }
        };
        }
    }
}
