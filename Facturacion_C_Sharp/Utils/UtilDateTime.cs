using System;
using System.Globalization;

namespace Facturacion_C_Sharp.Utils
{
    public static class UtilDateTime
    {
        public static string ToRfc3339String(this DateTime dateTime)
        {
            var formato = "yyyy-MM-dd'T'HH:mm:sszzz";
            //var formato = "yyyy-MM-dd'T'HH:mm:ssZ";

            return dateTime.ToString(formato, DateTimeFormatInfo.InvariantInfo);
        }
    }
}
