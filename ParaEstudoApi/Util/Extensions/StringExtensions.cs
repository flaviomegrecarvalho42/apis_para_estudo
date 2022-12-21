using System;
using System.Globalization;

namespace ParaEstudoApi.Util.Extensions
{
    public static class StringExtensions
    {
        public static DateTime ToDateTime(this string oString)
        {
            return DateTime.ParseExact(oString, "ddMMyy", CultureInfo.InvariantCulture);
        }
    }
}
