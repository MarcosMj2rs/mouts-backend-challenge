using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    /// <summary>
    /// Handler for cancelling a sale.
    /// </summary>
    public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, CancelSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public CancelSaleHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<CancelSaleResult> Handle(CancelSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new CancelSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = await _saleRepository.GetByIdAsync(command.Id, cancellationToken);

            if (sale is null)
                throw new KeyNotFoundException($"Sale '{command.Id}' not found.");

            sale.Cancel();

            await _saleRepository.UpdateAsync(sale, cancellationToken);

            return _mapper.Map<CancelSaleResult>(sale);
        }
    }
}
