using System;

namespace Sales.UI.Helpers
{
    public class DateHelper
    {
        public static long ToUnixTimespan(DateTime date)
        {
            TimeSpan tspan = date.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0));
            return (long)Math.Truncate(tspan.TotalSeconds);
        } 
    }
}