using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SOCAUD.Intranet.Helper
{
    public class WebHelper
    {
        public static string GetStringDateTimeYMD(string datetime)
        {
            if(string.IsNullOrEmpty(datetime)) return "";
            var values = datetime.Split('/');
            return values[2] + values[1] + values[0];
        }


        public static DateTime GetDateTime(string datetime)
        {
            if (string.IsNullOrEmpty(datetime)) return new DateTime();
            var values = datetime.Split('/');
            return Convert.ToDateTime(values[2] + '-' + values[1] + '-' + values[0]);
        }

        public static DateTime? GetDateTimeOrNull(string datetime)
        {
            if (string.IsNullOrEmpty(datetime)) return default(DateTime?);
            var values = datetime.Split('/');
            return Convert.ToDateTime(values[2] + '-' + values[1]+ '-' + values[0]);
        }

        public static string GetShortDateString(DateTime? datetime)
        {
            if (datetime.HasValue)
                return datetime.Value.ToString("dd/MM/yyyy");

            return "";
        }

        public static string GetBooleanString(string booleanstring)
        {
            return string.IsNullOrEmpty(booleanstring) ? "NO" : (booleanstring.Equals("S") ? "SI" : "NO");
        }
    }
}