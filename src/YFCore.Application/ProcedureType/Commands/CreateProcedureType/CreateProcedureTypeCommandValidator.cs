using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentValidation;

using YFCore.Domain.ProcedureTypes.Entity;

namespace YFCore.Application.ProcedureTypes.Commands.CreateProcedureType
{
    public class CreateProcedureTypeCommandValidator : AbstractValidator<CreateProcedureTypeCommand>
    {
        public CreateProcedureTypeCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(80);
            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(200);
        }
    }
}