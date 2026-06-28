namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale
{
    /// <summary>
    /// Result of a cancelled sale.
    /// </summary>
    public class CancelSaleResult
    {
        public Guid Id { get; set; }

        public bool IsCancelled { get; set; }
    }
}
