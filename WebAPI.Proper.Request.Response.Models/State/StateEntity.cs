using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Proper.Request.Response.Models.State
{
    // [Index(nameof(CountryID))]
    public class StateEntity
    {
        [Key]
        public int StateID { get; set; }

        [Required]
        [ForeignKey("CountryID")]
        public int CountryID { get; set; }

        public string Name { get; set; } = string.Empty;

        public bool IsDeleted { get; set; }
    }
}
