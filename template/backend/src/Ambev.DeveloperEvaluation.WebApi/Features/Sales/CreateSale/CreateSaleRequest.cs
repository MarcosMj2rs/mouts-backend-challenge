namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    /// <summary>
    /// Represents a request to create a new sale.
    /// </summary>
    public class CreateSaleRequest
    {
        /// <summary>
        /// Sale number.
        /// </summary>
        public string SaleNumber { get; set; } = string.Empty;

        /// <summary>
        /// Date when the sale was made.
        /// </summary>
        public DateTime SaleDate { get; set; }

        /// <summary>
        /// Customer identifier.
        /// </summary>
        public Guid CustomerId { get; set; }

        /// <summary>
        /// Customer name (External Identity).
        /// </summary>
        public string CustomerName { get; set; } = string.Empty;

        /// <summary>
        /// Branch identifier.
        /// </summary>
        public Guid BranchId { get; set; }

        /// <summary>
        /// Branch name (External Identity).
        /// </summary>
        public string BranchName { get; set; } = string.Empty;

        /// <summary>
        /// Sale items.
        /// </summary>
        public List<CreateSaleItemRequest> Items { get; set; } = [];
    }
}
