using System;

namespace BackendApartmentReservation.Infrastructure.Utilities
{
    public class DateTimeFormatter
    {
        public static string GetStandardDateTimeString(DateTimeOffset? date)
        {
            return date?.ToString("yyyy-MM-ddThh:mm:ss");
        }
    }
}
