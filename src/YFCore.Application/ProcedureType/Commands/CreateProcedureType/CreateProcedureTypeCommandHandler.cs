using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using YFCore.Application.Contracts;
using YFCore.Application.ProcedureTypes.Commands.CreateProcedureType;
using YFCore.Domain.ProcedureTypes.Entity;
using YFCore.Domain.ProcedureTypes.Repository;

namespace YFCore.Application.ProcedureTypes.Commands.CreateProcedureType
{
    public class CreateProcedureTypeCommandHandler : IRequestHandler<CreateProcedureTypeCommand, Guid>
    {
        private readonly IProcedureTypeRepository _procedureTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        public CreateProcedureTypeCommandHandler(IProcedureTypeRepository procedureTypeRepository, IUnitOfWork unitOfWork)
        {
            _procedureTypeRepository = procedureTypeRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Guid> Handle(CreateProcedureTypeCommand request, CancellationToken cancellationToken)
        {
            var procedureType = new ProcedureType(request.Name, request.Description);
            _procedureTypeRepository.Add(procedureType);
            await _unitOfWork.CommitAsync();
            return procedureType.Id;
        }
    }
}