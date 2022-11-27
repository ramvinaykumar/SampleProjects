using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Announcement.Business.Models
{
    public class LocalizedMessageModel
    {
        public string LanguageCode { get; set; }
        public string Message { get; set; }
    }
}
