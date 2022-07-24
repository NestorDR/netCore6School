using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace CommonLibrary.Extensions
{
    public static class EnumExtension
    {
        /// <summary>
        /// Gets friendly name of an enum value from its [Display(Name=...)] or [Description(...)] attributes
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="e">Enum value to get its friendly name</param>
        /// <param name="defaultValue">Default value to return</param>
        /// <returns>Friendly name</returns>
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
        /// Gets friendly name of an enum value from its [Description(...)] attribute
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="e">Enum value to get its friendly name</param>
        /// <param name="defaultValue">Default value to return</param>
        /// <returns>Friendly name</returns>
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
        /// Gets friendly name of an enum value from its [Display(Name=...)] data annotation
        /// Visit https://stackoverflow.com/questions/13099834/how-to-get-the-display-name-attribute-of-an-enum-member-via-mvc-razor-code#answer-13100409
        /// </summary>
        /// <typeparam name="T">Enum type</typeparam>
        /// <param name="e">Enum value to get its friendly name</param>
        /// <param name="defaultValue">Default value to return</param>
        /// <returns>Friendly name</returns>

        public static string GetDisplayName<T>(this T e, string defaultValue = "") where T : IConvertible
        {
            if (e is Enum)
            {
                FieldInfo? fieldInfo = e.GetType().GetField($"{e}");
                if (fieldInfo == null) goto done;

                DisplayAttribute[] displayAttributes = (DisplayAttribute[])fieldInfo.GetCustomAttributes(typeof(DisplayAttribute), false);
                if (displayAttributes.Length > 0) return $"{displayAttributes[0].Name}";
            }

        done:
            return defaultValue;
        }
    }
}
