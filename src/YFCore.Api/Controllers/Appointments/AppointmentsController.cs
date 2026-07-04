using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using YFCore.Api.Controllers;
using YFCore.Application.Appointment.Commands;
using YFCore.Application.Appointment.Queries;
using YFCore.Application.Appointment.DTOs;

namespace YFCore.Api.Controllers.Appointments
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : BaseController
    {
        private readonly IMediator _mediator;

        public AppointmentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<AppointmentDTO>>>> ListAppointments()
        {
            var appointments = await _mediator.Send(new ListAppointmentsQuery());
            return OkResponse(appointments, "Appointments retrieved successfully.");
        }

        [HttpGet("{id:guid}", Name = nameof(GetAppointmentById))]
        public async Task<ActionResult<ApiResponse<AppointmentDTO?>>> GetAppointmentById(Guid id)
        {
            var appointment = await _mediator.Send(new GetAppointmentByIdQuery(id));
            if (appointment is null)
                return NotFound("Appointment not found.");

            return OkResponse<AppointmentDTO?>(appointment, "Appointment retrieved successfully.");
        }

        [HttpPost]
        public async Task<ActionResult<ApiResponse<string>>> Create(CreateAppointmentCommand command)
        {
            var appointmentId = await _mediator.Send(command);
            return CreatedResponse(nameof(GetAppointmentById), new { id = appointmentId }, appointmentId.ToString(), "Appointment created successfully.");
        }

        [HttpPatch("{id:guid}/schedule")]
        public async Task<ActionResult<ApiResponse<string>>> Schedule(Guid id)
        {
            var result = await _mediator.Send(new ScheduleAppointmentCommand(id));
            return OkResponse(result.ToString(), "Appointment schedule command accepted.");
        }

        [HttpPatch("{id:guid}/cancel")]
        public async Task<ActionResult<ApiResponse<string>>> Cancel(Guid id)
        {
            var result = await _mediator.Send(new CancelAppointmentCommand(id));
            return OkResponse(result.ToString(), "Appointment cancel command accepted.");
        }

        [HttpPatch("{id:guid}/finish")]
        public async Task<ActionResult<ApiResponse<string>>> Finish(Guid id)
        {
            var result = await _mediator.Send(new FinishAppointmentCommand(id));
            return OkResponse(result.ToString(), "Appointment finish command accepted.");
        }
    }
}
