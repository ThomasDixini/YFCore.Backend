using FluentValidation;

namespace YFCore.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            RuleFor(x => x.Name)
                .NotEmpty()
                .MaximumLength(200);

            RuleFor(x => x.Description)
                .NotEmpty()
                .MaximumLength(500);

            RuleFor(x => x.PriceAmount)
                .GreaterThanOrEqualTo(0);

            RuleFor(x => x.PriceCurrency)
                .NotEmpty()
                .Length(3);

            RuleFor(x => x.CategoryId)
                .NotEmpty();
        }
    }
}
