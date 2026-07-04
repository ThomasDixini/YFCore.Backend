using System;

namespace YFCore.Application.Appointment.DTOs
{
    public sealed record AppointmentDTO(
        Guid Id,
        decimal PriceAmount,
        string PriceCurrency,
        Guid ProcedureTypeId,
        Guid UserId,
        string Token,
        string Status,
        string TimeSlots
    );
}
