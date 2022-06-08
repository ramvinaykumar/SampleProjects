using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearch.MVC.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class UserEN
    {
        /// <summary>
        /// 
        /// </summary>
        [Required]
        public int ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [Required]
        public string Address { get; set; }
    }
}
