namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem
{
    /// <summary>
    /// Result of a cancelled sale item.
    /// </summary>
    public class CancelSaleItemResult
    {
        public Guid SaleId { get; set; }

        public Guid ItemId { get; set; }

        public bool IsCancelled { get; set; }

        public decimal SaleTotalAmount { get; set; }
    }
}
