using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebAPI.Proper.Request.Response.Models.Enums;

namespace WebAPI.Proper.Request.Response.Models.Campaign
{
    public class UpdateCampaignDTO
    {
        /// <summary>
        /// Campaign ID
        /// </summary>
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Please Enter Valid Campaign ID")]
        public int ID { get; set; }

        // <summary>
        /// Campaign Name
        /// </summary>
        [Required(ErrorMessage = "Campaign name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Campaign name should be minimum 2 character and not more than 50 character!")]
        public string Name { get; set; }

        /// <summary>
        /// Campaign Audience
        /// </summary>
        [Required(ErrorMessage = "Audience is required.")]
        [ValidEnumValue]
        public Audience Audience { get; set; }

        /// <summary>
        /// Campaign Region
        /// </summary>
        [Required(ErrorMessage = "Region is required.")]
        [ValidEnumValue]
        public Region Region { get; set; }

        /// <summary>
        /// Campaign StartDate
        /// </summary>
        [Required(ErrorMessage = "Campaign StartDate is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Campaign EndDate
        /// </summary>
        [Required(ErrorMessage = "Campaign EndDate is required.")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Campaign Language
        /// </summary>
        [Required(ErrorMessage = "Language is required.")]
        [ValidEnumValue]
        public Language Language { get; set; }

        /// <summary>
        /// Campaign Message
        /// </summary>
        [Required(ErrorMessage = "Campaign Message is required.")]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Campaign Message should be minimum 2 character and not more than 150 character!")]
        public string Message { get; set; }

        /// <summary>
        /// To check whether Campaign is Active or not
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Campaign Updated Date
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime? UpdatedDate { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Campaign Updated By
        /// </summary>
        public int UpdatedBy { get; set; } = 1;

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // if either date is null, that date's required attribute will invalidate
            if (StartDate != null && EndDate != null && StartDate >= EndDate)
                yield return new ValidationResult($"End Date Should Be Greater Than {StartDate}.", new[] { nameof(EndDate) });
        }
    }
}
