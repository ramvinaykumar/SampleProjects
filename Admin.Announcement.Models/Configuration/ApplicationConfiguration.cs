namespace Admin.Announcement.Models.Configuration
{
    public class ApplicationConfiguration
    {
        public List<UserAccessInfo> AllowedUserInfo { get; set; }
        public bool EnableCache { get; set; }
        public int CacheTimeout { get; set; }
        public string CmsKeyEndPoint { get; set; }
        public string CmsCollectionName { get; set; }
    }

    public class UserAccessInfo
    {
        public string EmailId { get; set; }

        public bool HasCampaignAccess { get; set; }
    }

    public class ConfiguratorSettingConfig
    {
        public Dictionary<string, string> MFEUrls { get; set; }
        public Dictionary<string, string> APIUrls { get; set; }
        public Dictionary<string, string> ApplicationUrls { get; set; }
        public Dictionary<string, bool> Toggles { get; set; }
    }
}
