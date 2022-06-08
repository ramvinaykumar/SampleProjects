using System;
using System.ComponentModel.DataAnnotations;

namespace Application.DTO
{
    /// <summary>
    /// 
    /// </summary>
    public class PlaceInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Place { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string About { get; set; }

        [Required]
        public string City { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string State { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Country { get; set; }
    }
}
