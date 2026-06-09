using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace YFCore.Domain.Shared.ValueObjects
{
    public sealed class UnavailableTimeSlots
    {
        public Date Date { get; private set; }
        public IReadOnlyList<TimeOnly> TimeSlots { get; private set; }

        public UnavailableTimeSlots(Date date, IEnumerable<TimeOnly> timeSlots)
        {
            ArgumentNullException.ThrowIfNull(date, nameof(date));
            ArgumentNullException.ThrowIfNull(timeSlots, nameof(timeSlots));

            var slots = timeSlots
                .OrderBy(slot => slot)
                .Distinct()
                .ToList();

            if (!slots.Any())
                throw new ArgumentException("At least one time slot must be provided.", nameof(timeSlots));

            this.Date = date;
            this.TimeSlots = slots.AsReadOnly();
        }

        public static UnavailableTimeSlots Create(Date date, params TimeOnly[] timeSlots)
        {
            return new UnavailableTimeSlots(date, timeSlots);
        }

        public bool Contains(TimeOnly timeSlot)
        {
            return TimeSlots.Contains(timeSlot);
        }

        public string Format()
        {
            string formattedDate = Date.Format();
            string formattedSlots = string.Join(", ", TimeSlots.Select(slot => slot.ToString("HH:mm", CultureInfo.InvariantCulture)));
            return $"{formattedDate}: {formattedSlots}";
        }

        public override string ToString()
        {
            return Format();
        }
    }
}
