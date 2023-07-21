using System.ComponentModel.DataAnnotations;

namespace WebAPI.Proper.Request.Response.Models.Country
{
    public class CountryEntity
    {
        [Key]
        public int CountryID { get; set; }
        
        public string Name { get; set; } = string.Empty;

        public string ISO3 { get; set; } = string.Empty;
       
        public string ISO2 { get; set; } = string.Empty;

        public string Code { get; set; } = string.Empty;

        public string PhoneCode { get; set; } = string.Empty;

        public string Capital { get; set; } = string.Empty;

        public string Currency { get; set; } = string.Empty;

        public string CurrencyName { get; set; } = string.Empty;

        public string CurrencySymbol { get; set; } = string.Empty;

        public string Region { get; set; } = string.Empty;

        public string Subregion { get; set; } = string.Empty;

        public bool IsDeleted { get; set; } = false;
    }
}
