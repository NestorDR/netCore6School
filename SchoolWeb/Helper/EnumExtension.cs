using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace SchoolWeb.Helper
{
    public static class EnumExtension
    {
        /// <summary>
        /// // Get friendly name from [Display(Name=...)] or [Description(...)] attributes
        /// </summary>
        public static string GetFriendlyText<T>(this T e, string defaultValue = "") where T : IConvertible
        {
            // Get friendly text from [Display(Name=...)] Data Annotation
            string result = e.GetDisplayName();
            if (!string.IsNullOrWhiteSpace(result)) return result;
                    
            // Get friendly text from [Description(...)] Data Annotation
            result = e.GetDescription();
            if (!string.IsNullOrWhiteSpace(result)) return result;
            
            // Friendly name didn't get, return default value
            return defaultValue;
        }

        /// <summary>
        /// Get friendly name from [Description(...)] attribute
        /// </summary>
        public static string GetDescription<T>(this T e, string defaultValue = "") where T : IConvertible
        {
            if (e is Enum)
            {
                FieldInfo? fieldInfo = e.GetType().GetField($"{e}");
                if (fieldInfo == null) goto done;

                DescriptionAttribute[] descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
                if (descriptionAttributes.Length > 0) return descriptionAttributes[0].Description;
            }

        done:
            return defaultValue;
        }

        /// <summary>
        /// Get friendly name from [Display(Name=...)] data annotation
        /// Visit https://stackoverflow.com/questions/13099834/how-to-get-the-display-name-attribute-of-an-enum-member-via-mvc-razor-code#answer-13100409
        /// </summary>
        public static string GetDisplayName<T>(this T e, string defaultValue = "") where T : IConvertible
        {
            if (e is Enum)
            {
                FieldInfo? fieldInfo = e.GetType().GetField($"{e}");
                if (fieldInfo == null) goto done;

                DisplayAttribute[] displayAttributes = (DisplayAttribute[])fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false);
                if (displayAttributes.Length > 0) return $"{displayAttributes[0].Name}";

                //if (string.IsNullOrWhiteSpace(displayValue)) displayValue = GetDescription(value, defaultValue);
            }

        done:
            return defaultValue;
        }
    }
}
