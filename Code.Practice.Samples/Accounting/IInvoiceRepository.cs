using System;
using System.Collections.Generic;

namespace Code.Practice.Samples.Accounting
{
    /// <summary>
    /// 
    /// </summary>
    public interface IInvoiceRepository
    {
        decimal? GetTotal(int invoiceId);

        decimal GetTotalOfUnpaid();

        IReadOnlyDictionary<string, long> GetItemsReport(DateTime? from, DateTime? to);
    }
}
