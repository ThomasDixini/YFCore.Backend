using System;
using System.Collections.Generic;
using Xunit;
using FluentAssertions;

using YFCore.Domain.Shared.ValueObjects;

namespace YFCore.Tests.Unit.YFCore.Tests.Domain.TestsShared
{
    public class UnavailableTimeSlotsTests
    {
        [Fact]
        public void Create_ShouldInitializeWithSortedDistinctTimeSlots()
        {
            var date = Date.Create(2026, 6, 9);
            var slots = new[] { new TimeOnly(15, 0), new TimeOnly(9, 30), new TimeOnly(9, 30) };

            var unavailable = new UnavailableTimeSlots(date, slots);

            unavailable.Date.Should().Be(date);
            unavailable.TimeSlots.Should().BeEquivalentTo(new[] { new TimeOnly(9, 30), new TimeOnly(15, 0) }, options => options.WithStrictOrdering());
            unavailable.Format().Should().Be("2026-06-09: 09:30, 15:00");
        }

        [Fact]
        public void Create_ShouldThrow_WhenTimeSlotsAreEmpty()
        {
            var date = Date.Create(2026, 6, 9);
            var slots = Array.Empty<TimeOnly>();

            Action act = () => new UnavailableTimeSlots(date, slots);

            act.Should().Throw<ArgumentException>().WithMessage("At least one time slot must be provided.*");
        }

        [Fact]
        public void Create_ShouldThrow_WhenDateIsNull()
        {
            Date date = null!;
            var slots = new[] { new TimeOnly(10, 0) };

            Action act = () => new UnavailableTimeSlots(date, slots);

            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Contains_ShouldReturnTrue_WhenTimeSlotExists()
        {
            var date = Date.Create(2026, 6, 9);
            var unavailable = new UnavailableTimeSlots(date, new[] { new TimeOnly(9, 0), new TimeOnly(10, 0) });

            unavailable.Contains(new TimeOnly(10, 0)).Should().BeTrue();
            unavailable.Contains(new TimeOnly(11, 0)).Should().BeFalse();
        }
    }
}
