namespace EFCoreCodeFirstSample.Models.ContactDetails
{
    /// <summary>
    /// Member contact detail class
    /// </summary>
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

    /// <summary>
    /// Enquire list of contact info request class
    /// </summary>
    public class EnquireListOfContactInfoRequestDto
    {
        public string? CpfAccountNumber { get; set; }

        public string? MobileNumber { get; set; }

        public string? EmailAddress { get; set; }
    }
}
