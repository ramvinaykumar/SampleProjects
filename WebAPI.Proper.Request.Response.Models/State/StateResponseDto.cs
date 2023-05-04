using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.Proper.Request.Response.Models.State
{
    public class StateResponseDto : CommonStateModel
    {
        public string CountryName { get; set; } = string.Empty;

        public string IsDeleted { get; set; } = string.Empty;
    }
}
