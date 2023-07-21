namespace EFCoreCodeFirstSample.Models.ContactDetails
{
    public class MemberContactDetails 
    {
        public string? AccountNumber { get; set; }
        
        public string? Salutation { get; set; }
       
        public string? ResidencePhone { get; set; }
       
        public string? Mobile { get; set; }
      
        public string? EmailAddress { get; set; }

        public DateTimeOffset? ContactInfoUpdatedOn { get; set; }

        public bool? IsSyncWithMyInfo { get; set; }
    }
}
