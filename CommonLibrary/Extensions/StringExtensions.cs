﻿using System.Globalization;
using System.Text.RegularExpressions;

namespace CommonLibrary.Extensions
{
    /// <summary>
    /// Visit https://www.codeproject.com/articles/692603/csharp-string-extensions
    /// </summary>
    public static class StringExtensions
    {
        public static DateTime ToDate(this string s, bool throwExceptionIfFailed = false)
        {
            var valid = DateTime.TryParse(s, out var result);
            if (valid) return result;
            
            if (throwExceptionIfFailed)
                throw new FormatException($"'{s}' cannot be converted as DateTime");
            return result;
        }

        public static double ToDouble(this string s, bool throwExceptionIfFailed = false, double defaultValue = 0)
        {
            var valid = double.TryParse(s, NumberStyles.AllowDecimalPoint,
                new NumberFormatInfo { NumberDecimalSeparator = "." }, out double result);
            if (valid) return result;

            if (throwExceptionIfFailed)
                throw new FormatException($"'{s}' cannot be converted as double");
            return defaultValue;
        }

        public static int ToInt(this string s, bool throwExceptionIfFailed = false, int defaultValue = 0)
        {
            var valid = int.TryParse(s, out int result);
            if (valid) return result;
            
            if (throwExceptionIfFailed)
                throw new FormatException($"'{s}' cannot be converted as int");
            return defaultValue;
        }

        /// <summary>
        /// Matching all capital letters in the s and separate them with spaces to form a sentence.
        /// If the s is an abbreviation text, no space will be added and returns the same s.
        /// </summary>
        /// <param name="s">Input string</param>
        /// <returns>A string with a space added  before each capital letter, but not the first one</returns>
        public static string ToSentence(this string s)
        {
            s = s.ToUpper()[0] + s[1..];        // range indexer: s[1..] is equal to s.Substring(1)

            if (string.IsNullOrWhiteSpace(s))
                return s;
            // return as is if the s is just an abbreviation
            if (Regex.Match(s, "[0-9A-Z]+$").Success)
                return s;
            // add a space before each capital letter, but not the first one.
            var result = Regex.Replace(s, "(\\B[A-Z])", " $1");
            return result;
        }

        public static string Reverse(this string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return string.Empty;
            char[] chars = s.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        /// <summary>
        /// Returns the last howMany characters of the string
        /// </summary>
        /// <param name="s"></param>
        /// <param name="howMany"></param>
        /// <returns></returns>
        public static string GetLast(this string s, int howMany)
        {
            if (string.IsNullOrWhiteSpace(s)) return string.Empty;
            var value = s.Trim();
            return howMany >= value.Length ? value : value.Substring(value.Length - howMany);
        }

        /// <summary>
        /// Returns the first howMany characters of the string
        /// </summary>
        /// <param name="s"></param>
        /// <param name="howMany"></param>
        /// <returns></returns>
        public static string GetFirst(this string s, int howMany)
        {
            if (string.IsNullOrWhiteSpace(s)) return string.Empty;
            var value = s.Trim();
            return howMany >= value.Length ? value : s[..howMany];  // without range indexer Substring(0, howMany)
        }

        /// <summary>
        /// Pass in a string and this method will determine if it is all lower case characters or not.
        /// </summary>
        /// <param name="s">The string to check</param>
        /// <returns>True if the string passed in is all lower case, otherwise false.</returns>
        public static bool IsAllLowerCase(this string s)
        {
            return new Regex(@"^([^A-Z])+$").IsMatch(s);
        }

        /// <summary>
        /// Pass in a string and this method will determine if it is all upper case characters or not.
        /// </summary>
        /// <param name="s">The string to check.</param>
        /// <returns>True if the string passed in is all upper case, otherwise false.</returns>
        public static bool IsAllUpperCase(this string s)
        {
            return new Regex(@"^([^a-z])+$").IsMatch(s);
        }
        
        public static bool IsEmail(this string s)
        {
            var match = Regex.Match(s, 
                @"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-*)|(\w+\.))*\w+\.[a-zA-Z]{2,6}$", 
                RegexOptions.IgnoreCase);
            return match.Success;
        }
        
        public static bool IsNumber(this string s)
        {
            var match = Regex.Match(s, @"^[0-9]+$", RegexOptions.IgnoreCase);
            return match.Success;
        }
        
        public static bool IsPhone(this string s)
        {
            var match = Regex.Match(s, @"^\+?(\d[\d-. ]+)?(\([\d-. ]+\))?[\d-. ]+\d$", RegexOptions.IgnoreCase);
            return match.Success;
        }

        public static string ExtractEmail(this string? s)
        {
            if (s == null || string.IsNullOrWhiteSpace(s)) return string.Empty;

            var match = Regex.Match(s, @"^(([A-Za-z0-9]+_+)|([A-Za-z0-9]+\-+)|([A-Za-z0-9]+\.+)|([A-Za-z0-9]+\++))*[A-Za-z0-9]+@((\w+\-*)|(\w+\.))*\w+\.[a-zA-Z]{2,6}$", RegexOptions.IgnoreCase);
            return match.Success ? match.Value : string.Empty;
        }

        public static int ExtractNumber(this string s)
        {
            if (string.IsNullOrWhiteSpace(s)) return 0;

            var match = Regex.Match(s, "[0-9]+", RegexOptions.IgnoreCase);
            return match.Success ? match.Value.ToInt() : 0;
        }

        public static string ExtractQueryStringParamValue(this string queryString, string paramName)
        {
            if (string.IsNullOrWhiteSpace(queryString) || string.IsNullOrWhiteSpace(paramName)) return string.Empty;

            var query = queryString.Replace("?", "");
            if (!query.Contains("=")) return string.Empty;
            var queryValues = query.Split('&').Select(piQ => piQ.Split('=')).ToDictionary(piKey => piKey[0].ToLower().Trim(), piValue => piValue[1]);
            var found = queryValues.TryGetValue(paramName.ToLower().Trim(), out var result);
            return found ? result : string.Empty;

        }
    }
}
