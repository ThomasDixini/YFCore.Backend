using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using FluentValidation;

namespace YFCore.Application.ProcedureTypes.Commands.UpdateProcedureTypeCommand
{
    public class UpdateProcedureTypeCommandValidator : AbstractValidator<UpdateProcedureTypeCommand>
    {
        public UpdateProcedureTypeCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();
            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(100);
            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(200);
        }
    }
}