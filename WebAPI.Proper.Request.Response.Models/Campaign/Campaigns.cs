using System;

namespace WebAPI.Proper.Request.Response.Models.Campaign
{
    public class Campaigns
    {
        /// <summary>
        /// Campaign ID
        /// </summary>
        public int ID { get; set; }

        // <summary>
        /// Campaign Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Campaign Audience
        /// </summary>
        public string Audience { get; set; }

        /// <summary>
        /// Campaign Region
        /// </summary>
        public string Region { get; set; }

        /// <summary>
        /// Campaign StartDate
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Campaign EndDate
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Campaign Language
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Campaign Message
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// To check whether Campaign is Active or not
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// To check whether Campaign is Deleted or not
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Campaign Created Date
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Campaign Updated Date
        /// </summary>
        public DateTime? UpdatedDate { get; set; }

        /// <summary>
        /// Campaign Created By
        /// </summary>
        public int CreatedBy { get; set; } = 1;

        /// <summary>
        /// Campaign Updated By
        /// </summary>
        public int UpdatedBy { get; set; } = 1;
    }
}
