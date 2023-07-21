using System;
using System.Collections.Generic;
using System.Text;

namespace Code.Practice.Samples.NotificationEvent
{
    public class BaseIntegrationEvent
    {
        public Guid ID { get; set; }

        public string OperationID { get; set; }

        public string FromMS { get; set; }

        public string ToMS { get; set; }

        public string? CICSID { get; set; }

        public string? Port { get; set; }

        public string? Type { get; set; }
    }
}
