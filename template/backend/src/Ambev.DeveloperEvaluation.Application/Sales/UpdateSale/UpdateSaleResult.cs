namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleResult
    {
        public Guid Id { get; set; }

        public bool IsCancelled { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
