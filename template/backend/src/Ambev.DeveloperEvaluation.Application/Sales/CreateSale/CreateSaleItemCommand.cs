using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    /// <summary>
    /// Represents an item to be included in a sale.
    /// </summary>
    public class CreateSaleItemCommand
    {
        public Guid ProductId { get; set; }

        public string ProductDescription { get; set; } = string.Empty;

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }
    }
}
