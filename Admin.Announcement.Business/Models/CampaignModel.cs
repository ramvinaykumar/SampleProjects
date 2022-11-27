namespace Admin.Announcement.Business.Models
{
    public class CampaignModel : Audit
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Regions { get; set; }
        public string Audiences { get; set; }
        public List<LocalizedMessageModel> LocalizedMessages { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public bool IsActive { get; set; }
        public string MessageType { get; set; }
        #nullable enable
        public string? Country { get; set; }
    }
}
