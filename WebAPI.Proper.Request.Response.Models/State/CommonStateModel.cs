using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Proper.Request.Response.Models.State
{
    public class CommonStateModel
    {
        public int StateID { get; set; }

        public int CountryID { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
