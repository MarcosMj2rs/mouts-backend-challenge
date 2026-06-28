namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSale
{
    /// <summary>
    /// API response for CancelSale.
    /// </summary>
    public class CancelSaleResponse
    {
        public Guid Id { get; set; }

        public bool IsCancelled { get; set; }
    }
}
