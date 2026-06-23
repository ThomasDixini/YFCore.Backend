using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using YFCore.Application.ProcedureTypes.Contracts;
using YFCore.Application.ProcedureTypes.Queries.GetProcedureTypeById;

namespace YFCore.Application.ProcedureTypes.Queries.GetProcedureTypes
{
    public class GetProcedureTypesHandler : IRequestHandler<GetProcedureTypesQuery, IEnumerable<ProcedureTypeDTO>>
    {
        private readonly IProcedureTypeRead _procedureTypeRead;
        public GetProcedureTypesHandler(IProcedureTypeRead procedureTypeRead)
        {
            _procedureTypeRead = procedureTypeRead;
        }
        public async Task<IEnumerable<ProcedureTypeDTO>> Handle(GetProcedureTypesQuery request, CancellationToken cancellationToken)
        {
            return await _procedureTypeRead.GetProcedureTypesAsync();
        }
    }
}