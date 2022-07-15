using System;
using WebAPI.Proper.Request.Response.Models.Enums;

namespace WebAPI.Proper.Request.Response.Models.Students
{
    public class Students
    {
        public int ID { get; set; }

        // <summary>
        /// Student First Name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Student Last Name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Student gender
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Student Age
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// Student gender
        /// </summary>
        public Gender Gender { get; set; }

        /// <summary>
        /// Student Age
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Student Middle Name
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Student Age
        /// </summary>
        public string ZipCode { get; set; }

        /// <summary>
        /// To check whether Student is deleted or not
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// To check whether Student IsActive or not
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Member creation date
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Member updation date
        /// </summary>
        public DateTime? UpdatedDate { get; set; }
    }
}
