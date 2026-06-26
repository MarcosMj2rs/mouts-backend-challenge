using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents an item of a sale, containing product identity, quantity, price, discount and totals.
    /// </summary>
    public class SaleItem : BaseEntity
    {
        private const int MaximumQuantity = 20;

        public Guid SaleId { get; set; }

        public Guid ProductId { get; set; }

        public string ProductDescription { get; set; } = string.Empty;

        public int Quantity { get; private set; }

        public decimal UnitPrice { get; private set; }

        public decimal Discount { get; private set; }

        public decimal TotalAmount { get; private set; }

        public bool IsCancelled { get; private set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public SaleItem()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public SaleItem(Guid productId, string productDescription, int quantity, decimal unitPrice)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            ProductDescription = productDescription;
            CreatedAt = DateTime.UtcNow;

            SetQuantity(quantity);
            SetUnitPrice(unitPrice);
            CalculateTotals();
        }

        public void Update(Guid productId, string productDescription, int quantity, decimal unitPrice)
        {
            EnsureNotCancelled();

            ProductId = productId;
            ProductDescription = productDescription;

            SetQuantity(quantity);
            SetUnitPrice(unitPrice);
            CalculateTotals();

            UpdatedAt = DateTime.UtcNow;
        }

        public void Cancel()
        {
            EnsureNotCancelled();

            IsCancelled = true;
            UpdatedAt = DateTime.UtcNow;
        }

        private void SetQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new DomainException("Item quantity must be greater than zero.");

            if (quantity > MaximumQuantity)
                throw new DomainException("It is not possible to sell more than 20 identical items.");

            Quantity = quantity;
        }

        private void SetUnitPrice(decimal unitPrice)
        {
            if (unitPrice <= 0)
                throw new DomainException("Unit price must be greater than zero.");

            UnitPrice = unitPrice;
        }

        private void CalculateTotals()
        {
            var grossAmount = Quantity * UnitPrice;
            Discount = grossAmount * GetDiscountPercentage();
            TotalAmount = grossAmount - Discount;
        }

        private decimal GetDiscountPercentage()
        {
            if (Quantity >= 10)
                return 0.20m;

            if (Quantity >= 4)
                return 0.10m;

            return 0m;
        }

        private void EnsureNotCancelled()
        {
            if (IsCancelled)
                throw new DomainException("Cancelled items cannot be changed.");
        }
    }
}
