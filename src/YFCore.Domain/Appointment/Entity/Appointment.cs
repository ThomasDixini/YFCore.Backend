using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using YFCore.Domain.Appointment.Enum;
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
        public Appointment(Money price, Guid procedureTypeId, Guid userId)
        {
            this.Validate(price, procedureTypeId, userId);
            this.Status = AppointmentStatus.AwaitingConfirmation;
            this.Price = price;
            this.ProcedureTypeId = procedureTypeId;
            this.UserId = userId;
        }

        public void Validate(Money price, Guid procedureTypeId, Guid userId)
        {
            ArgumentNullException.ThrowIfNull(price.Currency, nameof(price.Currency));
            if (price.Amount < 0)
                throw new ArgumentException("Price cannot be negative.");
            if (procedureTypeId == Guid.Empty)
                throw new ArgumentException("ProcedureTypeId cannot be empty.");
            if (userId == Guid.Empty)
                throw new ArgumentException("UserId cannot be empty.");
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
        }

        public void Cancel()
        {
            if (this.Status == AppointmentStatus.Cancelled)
                throw new DomainException("Appointment is already cancelled.");
            if (this.Status == AppointmentStatus.Completed)
                throw new DomainException("A completed appointment cannot be cancelled.");
            this.Status = AppointmentStatus.Cancelled;
        }

        public void Complete()
        {
            if (this.Status != AppointmentStatus.Scheduled)
                throw new DomainException("Only scheduled appointments can be completed.");
            this.Status = AppointmentStatus.Completed;
        }
    }
}