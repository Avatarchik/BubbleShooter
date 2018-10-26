using System.Text.RegularExpressions;

namespace LimboFramework.Utils
{
    public static class StringHelper
    {
        public static int FilterNumbersFromString(string beFilterString)
        {
            int result = 0;
            if (!string.IsNullOrEmpty(beFilterString))
            {
                beFilterString = Regex.Replace(beFilterString, @"[^0-9]+", "");
                if (Regex.IsMatch(beFilterString, @"^[+-]?\d*[.]?\d*$"))
                {
                    result = int.Parse(beFilterString);
                }
            }
            return result;
        }
    }
}