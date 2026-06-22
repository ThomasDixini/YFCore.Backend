using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using Microsoft.AspNetCore.Mvc;

using YFCore.Application.ProcedureType.Queries.GetProcedureTypeById;

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
        [HttpGet("{Id}")]
        public async Task<ActionResult<ApiResponse<ProcedureTypeDTO>>> GetProcedureType(Guid Id)
        {
            var procedureType = await _mediator.Send(new GetProcedureTypeByIdQuery(Id));
            return OkResponse(procedureType, "Procedure type retrieved successfully.");
        }
    }
}
