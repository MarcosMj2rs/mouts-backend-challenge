using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Validator for CreateSaleCommand.
    /// </summary>
    public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleCommandValidator()
        {
            RuleFor(x => x.SaleNumber)
                .NotEmpty()
                .MaximumLength(50);

            RuleFor(x => x.SaleDate)
                .NotEmpty();

            RuleFor(x => x.CustomerId)
                .NotEmpty();

            RuleFor(x => x.CustomerName)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.BranchId)
                .NotEmpty();

            RuleFor(x => x.BranchName)
                .NotEmpty()
                .MaximumLength(150);

            RuleFor(x => x.Items)
                .NotEmpty();

            RuleForEach(x => x.Items)
                .ChildRules(item =>
                {
                    item.RuleFor(i => i.ProductId).NotEmpty();
                    item.RuleFor(i => i.ProductDescription).NotEmpty();
                    item.RuleFor(i => i.Quantity).GreaterThan(0);
                    item.RuleFor(i => i.UnitPrice).GreaterThan(0);
                });
        }
    }
}
