using System;

namespace Code.Practice.Samples.NotificationEvent
{
    public class CreateNotificationEvent : BaseIntegrationEvent
    {
        public string NotificationType { get; set; }

        public string TemplateCode { get; set; }

        public string SubjectReplacementKey { get; set; }

        public string SubjectReplacementValue { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string BodyType { get; set; }

        public string JsonBody { get; set; }

        public string CPFAccountNumber { get; set; }

        //public TransactionNumberRequestDto TransactionNumberRequest { get; set; }

        public CreateNotificationEvent()
        {
        }

        public CreateNotificationEvent(string NotificationType, string TemplateCode, string SubjectReplacementKey, string SubjectReplacementValue, string Email, string PhoneNumber, string BodyType, string JsonBody, string CPFAccountNumber, string FromMS, string ToMS)
        {
            base.ID = Guid.NewGuid();
            this.NotificationType = NotificationType;
            this.TemplateCode = TemplateCode;
            this.SubjectReplacementKey = SubjectReplacementKey;
            this.SubjectReplacementValue = SubjectReplacementValue;
            this.Email = Email;
            this.PhoneNumber = PhoneNumber;
            this.BodyType = BodyType;
            this.JsonBody = JsonBody;
            this.CPFAccountNumber = CPFAccountNumber;
            //this.TransactionNumberRequest = TransactionNumberRequest;
            base.FromMS = FromMS;
            base.ToMS = ToMS;
        }
    }
}
