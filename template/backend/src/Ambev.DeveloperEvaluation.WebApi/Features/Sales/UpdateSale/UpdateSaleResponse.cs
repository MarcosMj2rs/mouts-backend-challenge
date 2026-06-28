namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    public class UpdateSaleResponse
    {
        public Guid Id { get; set; }

        public bool IsCancelled { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
