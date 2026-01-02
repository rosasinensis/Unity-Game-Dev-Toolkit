using UnityEngine;
using System.Collections.Generic;

namespace MK.TextUtil
{
    public class TextBuilder
    {
        private System.Text.StringBuilder sb = new System.Text.StringBuilder();
        public void Clear() => sb.Clear();
        public void Space() => sb.Append(" ");

        public void Append(string text) => sb.Append(text);
        public void Append(char character) => sb.Append(character);

        public string ToText()
        {
            string final = sb.ToString();
            Clear();
            return final;
        }

        public string Replace(string original, Dictionary<string, string> dictionary)
        {
            Clear();
            sb.Append(original);
            foreach (KeyValuePair<string, string> pair in dictionary)
            {
                sb.Replace(pair.Key, pair.Value);
            }
            return ToText();
        }
        public string Replace(string original, string toReplace, string replaceWith)
        {
            Clear();
            sb.Append(original).Replace(toReplace, replaceWith);
            return ToText();
        }
        public string Inject(string toReplace, string replaceWith)
        {
            sb.Replace(toReplace, replaceWith);
            return ToText();
        }
        public string Combine(params string[] strings)
        {
            Clear();
            int length = strings.Length;
            for (int i = 0; i < length; i++)
            {
                sb.Append(strings[i]);
            }
            return ToText();
        }
        public string Combine(params char[] characters)
        {
            Clear();
            int length = characters.Length;
            for (int i = 0; i < length; i++)
            {
                sb.Append(characters[i]);
            }
            return ToText();
        }
        public string Format(string format, params object[] args)
        {
            Clear();
            sb.AppendFormat(format, args);
            return ToText();
        }
    }
    public static class To
    {
        // Utility class for strings.

        private static TextBuilder tb = new TextBuilder();

        public static string LowerAndRemove(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            tb.Clear();
            foreach (char c in input)
            {
                if (!char.IsWhiteSpace(c))
                {
                    tb.Append(char.ToLowerInvariant(c));
                }
            }
            return tb.ToText();
        }
        public static string LowerAndRemoveUpTo(string input, int startIndex, int endIndex)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            int length = input.Length;
            int sIndex = Mathf.Clamp(startIndex, 0, length);
            int eIndex = Mathf.Clamp(endIndex, 0, length - sIndex);

            string sub = input.Substring(sIndex, eIndex);
            return LowerAndRemove(sub);
        }

        public static string Capitalize(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            tb.Clear();
            tb.Append(char.ToUpperInvariant(input[0]));
            var length = input.Length;
            for (int i = 1; i < length; i++)
            {
                tb.Append(input[i]);
            }
            return tb.ToText();
        }
        public static string Lower(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            tb.Clear();

            var length = input.Length;
            for (int i = 0; i < length; i++)
            {
                char character = input[i];
                tb.Append(char.ToLowerInvariant(character));
            }
            return tb.ToText();
        }

        public static string RemoveSpace(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            tb.Clear();
            foreach (char c in input)
            {
                if (!char.IsWhiteSpace(c))
                {
                    tb.Append(c);
                }
            }
            return tb.ToText();
        }

        public static bool Equal(string firstString, object secondObject)
        {
            // Example:
            // ironsword == ItemType.IronSword

            return LowerAndRemove(firstString) == LowerAndRemove(secondObject.ToString());
        }
        public static bool Compare(string first, string second) => string.Compare(first, second, true) == 0;

        public static string ToStaticText(params char[] characters)
        {
            tb.Clear();
            int length = characters.Length;
            for (int i = 0; i < characters.Length; i++)
            {
                tb.Append(characters[i]);
            }
            return tb.ToText();
        }
    }

}
