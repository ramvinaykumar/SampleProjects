using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Announcement.Models.CmsKeys
{
    public class CMSResponse
    {
        public QueryResponse queryResponse { get; set; }

        public class QueryResponse
        {
            public double TimeTaken { get; set; }
            public double TotalCount { get; set; }
            public bool Success { get; set; }
            public object ErrorMessage { get; set; }
            public List<Item> Items { get; set; }
            public string ContentTypeName { get; set; }
            public string TemplateName { get; set; }
        }

        public class Item
        {
            public string Key { get; set; }
            public Value Value { get; set; }
        }

        public class Value
        {
            public string MicroContentValue { get; set; }
        }
    }
}
