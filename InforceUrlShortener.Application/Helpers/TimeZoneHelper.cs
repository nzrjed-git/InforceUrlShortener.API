namespace InforceUrlShortener.Application.Helpers
{
    public static class TimeZoneHelper
    {
        private static readonly TimeZoneInfo KyivTimeZone =
            TimeZoneInfo.FindSystemTimeZoneById("FLE Standard Time");

        public static DateTime ConvertUtcToKyiv(DateTime utcDateTime)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(utcDateTime, KyivTimeZone);
        }
    }
}
