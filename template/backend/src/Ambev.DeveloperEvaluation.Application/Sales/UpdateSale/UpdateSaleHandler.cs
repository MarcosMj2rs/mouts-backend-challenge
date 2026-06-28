using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;

        public UpdateSaleHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new UpdateSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);

            if (sale is null)
                throw new KeyNotFoundException($"Sale '{command.Id}' not found.");

            var items = command.Items
                .Select(item => new SaleItem(
                    item.ProductId,
                    item.ProductDescription,
                    item.Quantity,
                    item.UnitPrice))
                .ToList();

            sale.Update(
                command.SaleNumber,
                command.SaleDate,
                command.CustomerId,
                command.CustomerName,
                command.BranchId,
                command.BranchName,
                items);

            await _saleRepository.UpdateAsync(sale, cancellationToken);

            return new UpdateSaleResult
            {
                Id = sale.Id,
                IsCancelled = sale.IsCancelled,
                TotalAmount = sale.TotalAmount
            };
        }
    }
}
