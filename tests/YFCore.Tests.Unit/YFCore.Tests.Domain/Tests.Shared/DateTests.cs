using System;

using FluentAssertions;

using Xunit;

using YFCore.Domain.Shared.ValueObjects;

namespace YFCore.Tests.Unit.YFCore.Tests.Domain.TestsShared
{
    public class DateTests
    {
        [Fact]
        public void Create_ShouldInitializeDate_WhenValidValuesAreProvided()
        {
            var date = Date.Create(2026, 6, 9);

            date.Value.Should().Be(new DateOnly(2026, 6, 9));
            date.Locale.Should().Be("ISO");
            date.ToString().Should().Be("2026-06-09");
        }

        [Fact]
        public void Format_ShouldReturnUsStyle_WhenLocaleIsUs()
        {
            var date = new Date(2026, 6, 9, "US");

            date.Format().Should().Be("06/09/2026");
        }

        [Fact]
        public void Format_ShouldReturnBrStyle_WhenLocaleIsBr()
        {
            var date = new Date(2026, 6, 9, "BR");

            date.Format().Should().Be("09/06/2026");
        }

        [Fact]
        public void Format_ShouldFallbackToIso_WhenLocaleIsUnknown()
        {
            var date = new Date(2026, 6, 9, "XYZ");

            date.Format().Should().Be("2026-06-09");
        }
    }
}
