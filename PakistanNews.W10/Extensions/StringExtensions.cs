using System;

namespace PakistanNews
{
    public static class StringExtensions
    {
        public static string SafeType(this string s)
        {
            return s;
        }

        public static DateTime? SafeDateTime(this string s)
        {            
            DateTime date;
            if (DateTime.TryParse(s, out date))
            {
                return date;
            }
            return null;
        }
    }
}
