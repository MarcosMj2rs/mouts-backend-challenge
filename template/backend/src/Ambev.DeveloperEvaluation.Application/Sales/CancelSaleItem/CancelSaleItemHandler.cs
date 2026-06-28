using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem
{
    /// <summary>
    /// Handler for cancelling a sale item.
    /// </summary>
    public class CancelSaleItemHandler : IRequestHandler<CancelSaleItemCommand, CancelSaleItemResult>
    {
        private readonly ISaleRepository _saleRepository;

        public CancelSaleItemHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<CancelSaleItemResult> Handle(
            CancelSaleItemCommand command,
            CancellationToken cancellationToken)
        {
            var validator = new CancelSaleItemCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = await _saleRepository.GetByIdAsync(command.SaleId, cancellationToken);

            if (sale is null)
                throw new KeyNotFoundException($"Sale '{command.SaleId}' not found.");

            sale.CancelItem(command.ItemId);

            await _saleRepository.SaveChangesAsync(cancellationToken);

            return new CancelSaleItemResult
            {
                SaleId = sale.Id,
                ItemId = command.ItemId,
                IsCancelled = true,
                SaleTotalAmount = sale.TotalAmount
            };
        }
    }
}
