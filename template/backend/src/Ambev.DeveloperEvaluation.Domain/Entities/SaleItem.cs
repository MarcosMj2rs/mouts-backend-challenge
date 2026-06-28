using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a product item in a sale.
    /// Responsible for applying quantity-based discount rules.
    /// </summary>
    public class SaleItem : BaseEntity
    {
        private const int MinimumQuantity = 1;
        private const int MaximumQuantity = 20;

        public Guid SaleId { get; private set; }

        public Guid ProductId { get; private set; }

        public string ProductDescription { get; private set; } = string.Empty;

        public int Quantity { get; private set; }

        public decimal UnitPrice { get; private set; }

        public decimal Subtotal { get; private set; }

        public decimal DiscountAmount { get; private set; }

        public decimal TotalAmount { get; private set; }

        public bool IsCancelled { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime? UpdatedAt { get; private set; }

        public SaleItem()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public SaleItem(Guid productId, string productDescription, int quantity, decimal unitPrice)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;

            SetProduct(productId, productDescription);
            SetQuantity(quantity);
            SetUnitPrice(unitPrice);
            RecalculateTotals();
        }

        public void Update(Guid productId, string productDescription, int quantity, decimal unitPrice)
        {
            EnsureNotCancelled();

            SetProduct(productId, productDescription);
            SetQuantity(quantity);
            SetUnitPrice(unitPrice);
            RecalculateTotals();

            UpdatedAt = DateTime.UtcNow;
        }

        public void Cancel()
        {
            EnsureNotCancelled();

            IsCancelled = true;
            UpdatedAt = DateTime.UtcNow;
        }

        private void SetProduct(Guid productId, string productDescription)
        {
            if (productId == Guid.Empty)
                throw new DomainException("Product id is required.");

            if (string.IsNullOrWhiteSpace(productDescription))
                throw new DomainException("Product description is required.");

            ProductId = productId;
            ProductDescription = productDescription.Trim();
        }

        private void SetQuantity(int quantity)
        {
            if (quantity < MinimumQuantity)
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

        private void RecalculateTotals()
        {
            Subtotal = Quantity * UnitPrice;
            DiscountAmount = Subtotal * GetDiscountRate();
            TotalAmount = Subtotal - DiscountAmount;
        }

        private decimal GetDiscountRate()
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

        public void SetSaleId(Guid saleId)
        {
            if (saleId == Guid.Empty)
                throw new DomainException("Sale id is required.");

            SaleId = saleId;
        }
    }
}
