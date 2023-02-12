namespace Core6.EFDbFirst.API.Entities
{
    public class BankAccount
    {
        public int ID { get; set; }
        public string TransactionNumber { get; set; } 
        public string BankName { get; set; }
        public string BankCode { get; set; }
        public string BankAccountNumber { get; set; }
        public string IFSCCode { get; set; }
        public string PaymentMode { get; set; }
        public decimal PartialSumAmount { get; set; }
        public bool IsActive { get; set; }
    }
}
