using Ambev.DeveloperEvaluation.Application.Events;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IEventPublisher _eventPublisher;

        public UpdateSaleHandler(ISaleRepository saleRepository, IEventPublisher eventPublisher)
        {
            _saleRepository = saleRepository;
            _eventPublisher = eventPublisher;
        }

        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new UpdateSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = await _saleRepository.GetByIdAsNoTrackingAsync(command.Id, cancellationToken);

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

            await _saleRepository.ReplaceAsync(sale, cancellationToken);

            await _eventPublisher.PublishAsync(EventNames.SALE_UPDATED, sale, cancellationToken);

            return new UpdateSaleResult
            {
                Id = sale.Id,
                IsCancelled = sale.IsCancelled,
                TotalAmount = sale.TotalAmount
            };
        }
    }
}
