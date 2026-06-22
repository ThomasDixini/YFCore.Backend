using System;

using FluentAssertions;
using Xunit;

using YFCore.Domain.Shared.Helpers;

namespace YFCore.Tests.Unit.YFCore.Tests.Domain.TestsShared
{
    public class TimeZoneHelperTests
    {
        [Fact]
        public void GetAppTimeZone_ShouldReturnLocal_WhenEnvVarNotSet()
        {
            Environment.SetEnvironmentVariable("DEFAULT_TIMEZONE_ID", null);

            var timeZone = TimeZoneHelper.GetAppTimeZone();

            timeZone.Should().Be(TimeZoneInfo.Local);
        }

        [Fact]
        public void GetAppTimeZone_ShouldReturnUtc_WhenEnvVarIsUtc()
        {
            Environment.SetEnvironmentVariable("DEFAULT_TIMEZONE_ID", "UTC");

            var timeZone = TimeZoneHelper.GetAppTimeZone();

            timeZone.Should().Be(TimeZoneInfo.FindSystemTimeZoneById("UTC"));
        }

        [Fact]
        public void GetAppTimeZone_ShouldReturnLocal_WhenEnvVarIsInvalid()
        {
            Environment.SetEnvironmentVariable("DEFAULT_TIMEZONE_ID", "Invalid/Timezone");

            var timeZone = TimeZoneHelper.GetAppTimeZone();

            timeZone.Should().Be(TimeZoneInfo.Local);
        }

        [Fact]
        public void GetTodayInAppTimeZone_ShouldReturnDateInConfiguredTimeZone()
        {
            Environment.SetEnvironmentVariable("DEFAULT_TIMEZONE_ID", "UTC");

            var expected = DateOnly.FromDateTime(TimeZoneInfo.ConvertTime(DateTimeOffset.UtcNow, TimeZoneInfo.Utc).Date);
            var actual = TimeZoneHelper.GetTodayInAppTimeZone();

            actual.Should().Be(expected);
        }
    }
}
