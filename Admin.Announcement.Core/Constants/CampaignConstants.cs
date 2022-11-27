using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Announcement.Core.Constants
{
    public class CampaignConstants
    {
        public static Dictionary<string, string> Audiences = new Dictionary<string, string>() {
            {"CP","Channel Partner" },
            {"TSR","Internal" },
            {"RESELLER","Distributor/Reseller" },
        };

        public static ILookup<string, string> AudiencesLookup = Audiences.ToLookup(x => x.Key, x => x.Value);
    }
}
