using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardPayment.API.Models
{
    public class CardDetail
    {
        [Key]
        public int CardDetailId { get; set; }
        [Column(TypeName = "nvarchar(100)")]
        public string CardOwnerName { get; set; }
        [Column(TypeName = "nvarchar(16)")]
        public string CardNumber { get; set; }
        [Column(TypeName = "nvarchar(5)")]
        public string ExpiryDate { get; set; }
        [Column(TypeName = "nvarchar(3)")]
        public string SecurityNumber { get; set; }
    }
}
