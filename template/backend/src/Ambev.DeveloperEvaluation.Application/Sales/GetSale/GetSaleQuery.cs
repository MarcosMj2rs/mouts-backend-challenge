using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    /// <summary>
    /// Query for retrieving a sale by its identifier.
    /// </summary>
    public class GetSaleQuery : IRequest<GetSaleResult>
    {
        public Guid Id { get; set; }
    }
}
