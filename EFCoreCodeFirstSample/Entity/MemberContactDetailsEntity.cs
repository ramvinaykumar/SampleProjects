using System.ComponentModel.DataAnnotations;

namespace EFCoreCodeFirstSample.Entity
{
    public class MemberContactDetailsEntity : BaseEntity
    {
        [MaxLength(200)]
        public string AccountNumber { get; set; }
        [MaxLength(5)]
        public string? Salutation { get; set; }
        [MaxLength(20)]
        public string? ResidencePhone { get; set; }
        [MaxLength(20)]
        public string? Mobile { get; set; }
        [MaxLength(512)]
        public string? EmailAddress { get; set; }
        public DateTimeOffset? ContactInfoUpdatedOn { get; set; }
        public bool? IsSyncWithMyInfo { get; set; }

        private MemberContactDetailsEntity() { }

        public MemberContactDetailsEntity(string accountNumber, string salution, string residencePhone, string mobile
                                        , string emailAddress, DateTimeOffset contactInfoUpdatedOn, string userType, string userID
                                        , bool isSyncWithMyInfo = false) : base(userID, userType)
        {
            AccountNumber = accountNumber;
            Salutation = salution;
            ResidencePhone = residencePhone;
            Mobile = mobile;
            EmailAddress = emailAddress;
            IsSyncWithMyInfo = isSyncWithMyInfo;
            ContactInfoUpdatedOn = contactInfoUpdatedOn;
        }
    }
}
