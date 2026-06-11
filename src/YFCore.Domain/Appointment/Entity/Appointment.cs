using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using YFCore.Domain.Appointment.Enum;
using YFCore.Domain.Appointment.Events;
using YFCore.Domain.Shared.Base;
using YFCore.Domain.Shared.Exceptions;
using YFCore.Domain.Shared.ValueObjects;

namespace YFCore.Domain.Appointments.Entity
{
    public class Appointment : BaseEntity
    {
        public AppointmentStatus Status { get; private set; }
        public Money Price { get; private set; }
        public Guid ProcedureTypeId { get; private set; }
        public Guid UserId { get; private set; }
        public UnavailableTimeSlots TimeSlots { get; private set; }
        public Appointment(Money price, Guid procedureTypeId, Guid userId, UnavailableTimeSlots timeSlots)
        {
            this.Validate(price, procedureTypeId, userId, timeSlots);
            this.Status = AppointmentStatus.AwaitingConfirmation;
            this.Price = price;
            this.ProcedureTypeId = procedureTypeId;
            this.UserId = userId;
            this.TimeSlots = timeSlots;
        }

        public void Validate(Money price, Guid procedureTypeId, Guid userId, UnavailableTimeSlots timeSlots)
        {
            ArgumentNullException.ThrowIfNull(price.Currency, nameof(price.Currency));
            if (price.Amount < 0)
                throw new ArgumentException("Price cannot be negative.");
            if (procedureTypeId == Guid.Empty)
                throw new ArgumentException("ProcedureTypeId cannot be empty.");
            if (userId == Guid.Empty)
                throw new ArgumentException("UserId cannot be empty.");
            if (timeSlots == null || timeSlots.TimeSlots == null || !timeSlots.TimeSlots.Any())
                throw new ArgumentException("TimeSlots cannot be null or empty.");
        }

        public void ChangePrice(Money price)
        {
            ArgumentNullException.ThrowIfNull(price.Currency, nameof(price.Currency));
            if (price.Amount < 0)
                throw new ArgumentException("Price cannot be negative.");
            this.Price = price;
        }

        public void ChangeProcedureType(Guid procedureTypeId)
        {
            if (procedureTypeId == Guid.Empty)
                throw new ArgumentException("ProcedureTypeId cannot be empty.");
            this.ProcedureTypeId = procedureTypeId;
        }

        public void Schedule()
        {
            if (this.Status != AppointmentStatus.AwaitingConfirmation)
                throw new DomainException("Only appointments in awaiting confirmation can be scheduled.");
            this.Status = AppointmentStatus.Scheduled;
            AddDomainEvent(new AppointmentConfirmed(this.Id, DateTime.UtcNow));
        }

        public void Cancel()
        {
            if (this.Status == AppointmentStatus.Cancelled)
                throw new DomainException("Appointment is already cancelled.");
            if (this.Status == AppointmentStatus.Completed)
                throw new DomainException("A completed appointment cannot be cancelled.");
            this.Status = AppointmentStatus.Cancelled;
            AddDomainEvent(new AppointmentCancelled(this.Id, DateTime.UtcNow));
        }

        public void Complete()
        {
            if (this.Status != AppointmentStatus.Scheduled)
                throw new DomainException("Only scheduled appointments can be completed.");
            this.Status = AppointmentStatus.Completed;
            AddDomainEvent(new AppointmentFinished(this.Id, DateTime.UtcNow));
        }
    }
}
