using System;

namespace Vivacity.Library.Utils
{
    public static class StringExtension
    {
        /// <summary>
        /// Contains the specified str and values.
        /// </summary>
        /// <param name="str">String.</param>
        /// <param name="values">Values.</param>
        public static bool Contains(this String str, params char[] values)
        {
            if (values.Length == 1)
                return  str.IndexOf(values[0].ToString(), StringComparison.Ordinal) >= 0;
            
            foreach (char item in values)
            {
                if (str.Contains(item))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Contains the specified str and values.
        /// </summary>
        /// <param name="str">String.</param>
        /// <param name="values">Values.</param>
        public static bool Contains(this String str, params string[] values)
        {
            if (values.Length == 1)
                return  str.IndexOf(values[0], StringComparison.Ordinal) >= 0;
            
            foreach (string item in values)
            {
                if (str.Contains(item))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// Remove the specified str and values.
        /// </summary>
        /// <param name="str">String.</param>
        /// <param name="values">Values.</param>
        public static string Remove(this String str, params string[] values)
        {
            foreach (string item in values)
                str = str.Replace(item, "");

            return str;
        }

        /// <summary>
        /// Remove the specified str and values.
        /// </summary>
        /// <param name="str">String.</param>
        /// <param name="values">Values.</param>
        public static string Remove(this String str, params char[] values)
        {
            foreach (char item in values)
                str = str.Replace(item, '\0');

            return str;
        }
    }
}

