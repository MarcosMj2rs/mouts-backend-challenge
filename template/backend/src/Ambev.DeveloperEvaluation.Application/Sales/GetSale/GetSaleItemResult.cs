namespace Ambev.DeveloperEvaluation.Application.Sales.GetSale
{
    /// <summary>
    /// Represents an item of a sale.
    /// </summary>
    public class GetSaleItemResult
    {
        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string ProductDescription { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal DiscountAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public bool IsCancelled { get; set; }
    }
}
