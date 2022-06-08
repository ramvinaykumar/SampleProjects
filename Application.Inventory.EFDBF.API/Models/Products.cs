using System;

namespace Application.Inventory.EFDBF.API.Models
{
    /// <summary>
    /// Model class for product
    /// </summary>
    public partial class Products
    {
        /// <summary>
        /// Product Id
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// Product naem
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Product category
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Product color
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Product unit price
        /// </summary>
        public decimal UnitPrice { get; set; }

        /// <summary>
        /// Product available quantity
        /// </summary>
        public int AvailableQuantity { get; set; }

        /// <summary>
        /// Product creation date
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Check whether product is active or not
        /// </summary>
        public bool? IsActive { get; set; }
    }
}
