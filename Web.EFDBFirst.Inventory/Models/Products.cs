using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace Web.EFDBFirst.Inventory.Models
{
    public partial class Products
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public string Color { get; set; }
        public decimal UnitPrice { get; set; }
        public int AvailableQuantity { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool? IsActive { get; set; }
    }

    public class Demo
    {

        private void Add()
        {
           var result = Add(new Products());
        }

        private Products Add(Products products)
        {
            products.Category = "dfadf";
            return products;
        }

    }
}
