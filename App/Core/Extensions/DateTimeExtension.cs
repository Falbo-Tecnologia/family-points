namespace Core.Extensions;

public static class DateTimeExtension
{
    private static readonly TimeZoneInfo _brZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
    public static DateTime BrNow(this DateTime dateTime) => TimeZoneInfo.ConvertTimeFromUtc(DateTime.Now, _brZone);
    public static DateTime HoraBr(this DateTime dateTime) => TimeZoneInfo.ConvertTimeFromUtc(dateTime, _brZone);
}
