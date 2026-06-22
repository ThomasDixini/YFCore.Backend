using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using YFCore.Application.ProcedureType.Contracts;

namespace YFCore.Application.ProcedureType.Queries.GetProcedureTypeById
{
    public class GetProcedureTypeByIdHandler : IRequestHandler<GetProcedureTypeByIdQuery, ProcedureTypeDTO?>
    {
        private readonly IProcedureTypeRead _procedureTypeRead;
        public GetProcedureTypeByIdHandler(IProcedureTypeRead procedureTypeRead)
        {
            _procedureTypeRead = procedureTypeRead;
        }
        public async Task<ProcedureTypeDTO?> Handle(GetProcedureTypeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _procedureTypeRead.GetByIdAsync(request.Id);
        }
    }
}
