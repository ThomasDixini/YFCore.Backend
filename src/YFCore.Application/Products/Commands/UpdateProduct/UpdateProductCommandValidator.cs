using FluentValidation;

namespace YFCore.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty();

            When(x => x.Name is not null, () =>
            {
                RuleFor(x => x.Name)
                    .NotEmpty()
                    .MaximumLength(200);
            });

            When(x => x.Description is not null, () =>
            {
                RuleFor(x => x.Description)
                    .NotEmpty()
                    .MaximumLength(500);
            });

            When(x => x.PriceAmount.HasValue, () =>
            {
                RuleFor(x => x.PriceAmount)
                    .GreaterThanOrEqualTo(0);
            });

            When(x => x.PriceCurrency is not null, () =>
            {
                RuleFor(x => x.PriceCurrency)
                    .NotEmpty()
                    .Length(3);
            });

            When(x => x.CategoryId.HasValue, () =>
            {
                RuleFor(x => x.CategoryId)
                    .NotEmpty();
            });
        }
    }
}
