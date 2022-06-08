using System;
using System.Collections.Generic;

namespace Code.Practice.Samples.Accounting
{
    /// <summary>
    /// 
    /// </summary>
    public class Invoice
    {
        // A unique numerical identifier of an invoice (mandatory)
        public int Id { get; set; }
        // A short description of an invoice (optional).
        public string Description { get; set; }
        // A number of an invoice e.g. 134/10/2018 (mandatory).
        public string Number { get; set; }
        // An issuer of an invoice e.g. Metz-Anderson, 600  Hickman Street,Illinois (mandatory).
        public string Seller { get; set; }
        // A buyer of a service or a product e.g. John Smith, 4285  Deercove Drive, Dallas (mandatory).
        public string Buyer { get; set; }
        // A date when an invoice was issued (mandatory).
        public DateTime CreationDate { get; set; }
        // A date when an invoice was paid (optional).
        public DateTime? AcceptanceDate { get; set; }
        // A collection of invoice items for a given invoice (can be empty but is never null).
        public IList<InvoiceItem> InvoiceItems { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Invoice()
        {
            InvoiceItems = new List<InvoiceItem>();
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public class InvoiceItem
    {
        // A name of an item e.g. eggs.
        public string Name { get; set; }
        // A number of bought items e.g. 10.
        public uint Count { get; set; }
        // A price of an item e.g. 20.5.
        public decimal Price { get; set; }
    }
}
