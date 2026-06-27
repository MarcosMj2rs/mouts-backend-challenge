namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    /// <summary>
    /// Represents a sale item request.
    /// </summary>
    public class CreateSaleItemRequest
    {
        /// <summary>
        /// Product identifier.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// Product description.
        /// </summary>
        public string ProductDescription { get; set; } = string.Empty;

        /// <summary>
        /// Quantity sold.
        /// </summary>
        public int Quantity { get; set; }

        /// <summary>
        /// Unit price.
        /// </summary>
        public decimal UnitPrice { get; set; }
    }
}
