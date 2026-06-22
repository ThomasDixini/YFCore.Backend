using System;

namespace YFCore.Domain.Shared.Helpers
{
    public static class TimeZoneHelper
    {
        private const string DefaultEnvVar = "DEFAULT_TIMEZONE_ID";

        public static TimeZoneInfo GetAppTimeZone()
        {
            var id = Environment.GetEnvironmentVariable(DefaultEnvVar);
            if (!string.IsNullOrWhiteSpace(id))
            {
                try
                {
                    return TimeZoneInfo.FindSystemTimeZoneById(id);
                }
                catch (TimeZoneNotFoundException)
                {
                    return TimeZoneInfo.Local;
                }
                catch (InvalidTimeZoneException)
                {
                    return TimeZoneInfo.Local;
                }
            }

            return TimeZoneInfo.Local;
        }

        public static DateOnly GetTodayInAppTimeZone()
        {
            var tz = GetAppTimeZone();
            var nowUtc = DateTimeOffset.UtcNow;
            var nowInTz = TimeZoneInfo.ConvertTime(nowUtc, tz);
            return DateOnly.FromDateTime(nowInTz.Date);
        }
    }
}
