using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using YFCore.Application.ProcedureType.Contracts;
using YFCore.Application.ProcedureType.Queries.GetProcedureTypeById;

namespace YFCore.Application.ProcedureType.Queries.GetProcedureTypes
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