using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using YFCore.Application.ProcedureTypes.Commands.CreateProcedureType;
using YFCore.Application.ProcedureTypes.Commands.UpdateProcedureTypeCommand;
using YFCore.Application.ProcedureTypes.Queries.GetProcedureTypeById;
using YFCore.Application.ProcedureTypes.Queries.GetProcedureTypes;

namespace YFCore.Api.Controllers.ProcedureTypes
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProcedureTypesController : BaseController
    {
        private readonly IMediator _mediator;
        public ProcedureTypesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<ProcedureTypeDTO>>>> GetProcedureTypes()
        {
            var procedureTypes = await _mediator.Send(new GetProcedureTypesQuery());
            return OkResponse(procedureTypes, "Procedures Type retrieved successfully");
        }
        [HttpGet("{id:guid}", Name = nameof(GetProcedureTypeById))]
        public async Task<ActionResult<ApiResponse<ProcedureTypeDTO>>> GetProcedureTypeById(Guid id)
        {
            var procedureType = await _mediator.Send(new GetProcedureTypeByIdQuery(id));
            return OkResponse(procedureType, "Procedure type retrieved successfully.");
        }
        [HttpPost]
        public async Task<ActionResult<ApiResponse<string>>> Create(CreateProcedureTypeCommand command)
        {
            var procedureTypeId = await _mediator.Send(command);
            return CreatedResponse(nameof(GetProcedureTypeById), new { id = procedureTypeId }, procedureTypeId.ToString(), "Procedure Type created successufully.");
        }
        [HttpPatch]
        public async Task<ActionResult<ApiResponse<object>>> Update(UpdateProcedureTypeCommand command)
        {
            var procedureTypeUpdated = await _mediator.Send(command);
            return OkResponse(procedureTypeUpdated, "Procedure type updated successfully.");
        }
    }
}
