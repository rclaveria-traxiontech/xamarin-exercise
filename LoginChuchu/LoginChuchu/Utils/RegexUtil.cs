using System;
using System.Text.RegularExpressions;

namespace LoginChuchu.Utils
{
    public static class RegexUtil
    {
        public static Regex ValidEmailAddress()
        {
            return new Regex(@"^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$");
        }
    }
}
