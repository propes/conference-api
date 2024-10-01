public static class DateTimeHelper
{
    /// <summary>
    /// Converts a local time to UTC using the specified timezone.
    /// </summary>
    public static DateTime ConvertToUtc(DateTime localTime, string timeZone)
    {
        var tz = TimeZoneInfo.FindSystemTimeZoneById(timeZone);
        return TimeZoneInfo.ConvertTimeToUtc(localTime, tz);
    }
}
