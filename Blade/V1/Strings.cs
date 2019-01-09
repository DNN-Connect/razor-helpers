﻿using System.Text.RegularExpressions;

namespace Connect.Razor.V1
{
    public static partial class Blade
    {

        #region Ellipsis
        public static string Ellipsis(string valToShow, int maxChars, string trailer = null)
        {
            return valToShow.Length > maxChars
                ? valToShow.Substring(0, maxChars) + (trailer ?? BladeDefaults.HtmlEllipsisCharacter)
                : valToShow;
        }

        #endregion

        #region Replace helpers
        public static string Replace(this string input, string search, string replacement, bool caseSensitive)
        {
            input = input ?? "";
            search = search ?? "";
            replacement = replacement ?? "";

            if (caseSensitive)
                return input.Replace(search, replacement);

            string result = Regex.Replace(
                input,
                Regex.Escape(search ),
                (replacement).Replace("$", "$$"),
                RegexOptions.IgnoreCase
            );
            return result;
        }
        #endregion
    }
}