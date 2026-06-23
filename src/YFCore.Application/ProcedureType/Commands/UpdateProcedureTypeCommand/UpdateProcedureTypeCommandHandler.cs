using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using MediatR;

using YFCore.Application.Contracts;
using YFCore.Domain.ProcedureTypes.Repository;
using YFCore.Domain.Shared.Exceptions;

namespace YFCore.Application.ProcedureTypes.Commands.UpdateProcedureTypeCommand
{
    public class UpdateProcedureTypeCommandHandler : IRequestHandler<UpdateProcedureTypeCommand, object>
    {
        private readonly IProcedureTypeRepository _procedureTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UpdateProcedureTypeCommandHandler(IProcedureTypeRepository procedureTypeRepository, IUnitOfWork unitOfWork)
        {
            _procedureTypeRepository = procedureTypeRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<object> Handle(UpdateProcedureTypeCommand request, CancellationToken cancellationToken)
        {
            var procedureType = await _procedureTypeRepository.GetByIdAsync(request.Id);
            if (procedureType is null) throw new DomainException("Procedure type not found");

            if (request.Name is not null)
                procedureType.ChangeName(request.Name);
            if (request.Description is not null)
                procedureType.ChangeDescription(request.Description);
            await _unitOfWork.CommitAsync();
            return new
            {
                Id = procedureType.Id,
                Name = procedureType.Name,
                Description = procedureType.Description,
            };
        }
    }
}