using Ambev.DeveloperEvaluation.Domain.Common;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    /// <summary>
    /// Represents a sale aggregate root.
    /// Responsible for controlling its items and maintaining business consistency.
    /// </summary>
    public class Sale : BaseEntity
    {
        private readonly List<SaleItem> _items = [];

        public string SaleNumber { get; private set; } = string.Empty;

        public DateTime SaleDate { get; private set; }

        public Guid CustomerId { get; private set; }

        public string CustomerName { get; private set; } = string.Empty;

        public Guid BranchId { get; private set; }

        public string BranchName { get; private set; } = string.Empty;

        public decimal TotalAmount { get; private set; }

        public bool IsCancelled { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime? UpdatedAt { get; private set; }

        public IReadOnlyCollection<SaleItem> Items => _items.AsReadOnly();

        // Required by EF Core
        private Sale()
        {
            CreatedAt = DateTime.UtcNow;
        }

        public Sale(
            string saleNumber,
            DateTime saleDate,
            Guid customerId,
            string customerName,
            Guid branchId,
            string branchName,
            IEnumerable<SaleItem> items)
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;

            SetSaleNumber(saleNumber);
            SetSaleDate(saleDate);
            SetCustomer(customerId, customerName);
            SetBranch(branchId, branchName);

            if (items is null || !items.Any())
                throw new DomainException("A sale must contain at least one item.");

            foreach (var item in items)
                AddItem(item);

            RecalculateTotal();
        }

        public void Update(
            string saleNumber,
            DateTime saleDate,
            Guid customerId,
            string customerName,
            Guid branchId,
            string branchName,
            IEnumerable<SaleItem> items)
        {
            EnsureNotCancelled();

            if (items is null || !items.Any())
                throw new DomainException("A sale must contain at least one item.");

            SetSaleNumber(saleNumber);
            SetSaleDate(saleDate);
            SetCustomer(customerId, customerName);
            SetBranch(branchId, branchName);

            _items.Clear();

            foreach (var item in items)
                AddItem(item);

            RecalculateTotal();

            UpdatedAt = DateTime.UtcNow;
        }

        public void Cancel()
        {
            EnsureNotCancelled();

            foreach (var item in _items.Where(i => !i.IsCancelled))
                item.Cancel();

            IsCancelled = true;

            RecalculateTotal();

            UpdatedAt = DateTime.UtcNow;
        }

        public void CancelItem(Guid itemId)
        {
            EnsureNotCancelled();

            var item = _items.FirstOrDefault(i => i.Id == itemId);

            if (item is null)
                throw new DomainException("Sale item not found.");

            item.Cancel();

            RecalculateTotal();

            UpdatedAt = DateTime.UtcNow;
        }

        private void AddItem(SaleItem item)
        {
            ArgumentNullException.ThrowIfNull(item);

            _items.Add(item);
        }

        private void RecalculateTotal()
        {
            TotalAmount = _items
                .Where(i => !i.IsCancelled)
                .Sum(i => i.TotalAmount);
        }

        private void SetSaleNumber(string saleNumber)
        {
            if (string.IsNullOrWhiteSpace(saleNumber))
                throw new DomainException("Sale number is required.");

            SaleNumber = saleNumber.Trim();
        }

        private void SetSaleDate(DateTime saleDate)
        {
            if (saleDate == default)
                throw new DomainException("Sale date is required.");

            SaleDate = saleDate;
        }

        private void SetCustomer(Guid customerId, string customerName)
        {
            if (customerId == Guid.Empty)
                throw new DomainException("Customer id is required.");

            if (string.IsNullOrWhiteSpace(customerName))
                throw new DomainException("Customer name is required.");

            CustomerId = customerId;
            CustomerName = customerName.Trim();
        }

        private void SetBranch(Guid branchId, string branchName)
        {
            if (branchId == Guid.Empty)
                throw new DomainException("Branch id is required.");

            if (string.IsNullOrWhiteSpace(branchName))
                throw new DomainException("Branch name is required.");

            BranchId = branchId;
            BranchName = branchName.Trim();
        }

        private void EnsureNotCancelled()
        {
            if (IsCancelled)
                throw new DomainException("Cancelled sales cannot be changed.");
        }
    }
}
