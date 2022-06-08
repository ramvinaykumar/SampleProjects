using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace Code.Practice.Samples.Accounting
{
    /// <summary>
    /// 
    /// </summary>
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly IQueryable<Invoice> _invoices;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="invoices"></param>
        public InvoiceRepository(IQueryable<Invoice> invoices)
        {
            Contract.Requires<ArgumentNullException>(invoices != null, "input parameter cannot be null.");
            //// Guard.ThrowIfArgumentNull(provider, "provider");

            _invoices = invoices;
        }

        /// <summary>
        /// Should return a total value of an invoice with a given id. If an invoice does not exist null should be returned.
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <returns></returns>
        public decimal? GetTotal(int invoiceId)
        {
            decimal? total;
            if (_invoices.Any(x => x.Id == invoiceId))
                total = _invoices.Count(x => x.Id == invoiceId);
            else
                return null;

            return total;
        }

        /// <summary>
        /// Should return a total value of all unpaid invoices.
        /// </summary>
        /// <returns></returns>
        public decimal GetTotalOfUnpaid()
        {
            return _invoices.Where(x => x.AcceptanceDate == null)
                            .Sum(s => s.InvoiceItems
                            .Sum(y => y.Price));
        }

        /// <summary>
        /// Should return a dictionary where the name of an invoice item is a key and the number of bought items is a value.
        /// The number of bought items should be summed within a given period of time (from, to). Both the from date and the end date can be null.
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public IReadOnlyDictionary<string, long> GetItemsReport(DateTime? from, DateTime? to)
        {
            return _invoices.Where(x => x.AcceptanceDate >= (from == null ? x.AcceptanceDate : from) && x.AcceptanceDate <= (to == null ? x.AcceptanceDate : to))
                            .ToDictionary(d => d.Description, d => d.InvoiceItems
                            .Sum(s => s.Count));
            // return _invoices.Select(s => new { s.Description, s.InvoiceItems.Count }).Sum(s => s.InvoiceItems.Sum(y => y.Count)).ToDictionary(d => d.Description, d => Convert.ToInt64(d.Count));
            // x=> x.AcceptanceDate >= from && x.AcceptanceDate <= to).Sum(s => s.InvoiceItems.Sum(y => y.Count));
        }
    }
}
