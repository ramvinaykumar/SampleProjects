using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Announcement.Core.Constants
{
    public static class ApplicationConstants
    {
        public const string ApiDescription = "Campaigns";
        public const string SwaggerEndpoint = "/swagger/v1/swagger.json";
        public const string AppName = "Solution-campaign-service";
        public const string ResourceNotFound = "Resource not found";
        public const string MessageRequired = "Message is required";
        public const string LanguageRequired = "Language is required";
        public const string RegionRequired = "Region is required";
        public const string AudienceRequired = "Audience is required";
        public const string DateTimeRangeInvalid = "StartDateTime should not be greater than EndDateTime";
        public const string HealthCheckReadyEndpoint = "/healthCheck/ready";
        public const string HealthCheckLiveEndpoint = "/healthCheck/live";
        public const string JsonResponseType = "application/json";
    }
}
