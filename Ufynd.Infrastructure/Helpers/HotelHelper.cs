using System;

namespace Ufynd.Infrastructure.Helpers
{
    public static class HotelHelper
    {
        public static DateTime GetDepartureDate(int los, DateTime ArrivalDate)
        {
            return ArrivalDate.AddDays(los);
        }

        public static string Format(this DateTime date)
        {
            return date.ToString("dd.MM.yy");
        }
    }
}