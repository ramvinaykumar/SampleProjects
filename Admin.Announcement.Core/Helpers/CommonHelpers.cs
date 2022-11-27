using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Announcement.Core.Helpers
{
    public static class Common
    {
        public static DateTime GetCurrentDateTime()
        {
            var dtNow = DateTime.Now;
            return new DateTime(dtNow.Year, dtNow.Month, dtNow.Day);
        }
    }
}
