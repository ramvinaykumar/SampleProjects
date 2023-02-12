namespace Core6.EFDbFirst.API.Entities
{
    public class BankAccountDto
    {
        public int ID { get; set; }
        public string TransactionNumber { get; set; }
        public string BankName { get; set; }
        public string BankCode { get; set; }
        public byte[] BankAccountNumber { get; set; }
        public string IFSCCode { get; set; }
        public string PaymentMode { get; set; }
        public decimal PartialSumAmount { get; set; }
        public bool IsActive { get; set; }
    }
}
