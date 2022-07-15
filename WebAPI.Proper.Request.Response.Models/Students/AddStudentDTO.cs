using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebAPI.Proper.Request.Response.Models.Enums;

namespace WebAPI.Proper.Request.Response.Models.Students
{
    public class AddStudentDTO
    {
        // <summary>
        /// Student First Name
        /// </summary>
        [Required(ErrorMessage = "First name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First name should be minimum 2 character and not more than 50 character!")]
        public string FirstName { get; set; }

        /// <summary>
        /// Student Last Name
        /// </summary>
        [Required(ErrorMessage = "Last name is required.")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Last name should be minimum 2 character and not more than 50 character!")]
        public string LastName { get; set; }

        /// <summary>
        /// Student Email
        /// </summary>
        [Required(ErrorMessage = "Email is required.")]
        [RegularExpression("^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$", ErrorMessage = "E-mail is not valid")]
        [StringLength(128, MinimumLength = 8, ErrorMessage = "Email is not valid.")]
        public string Email { get; set; }

        /// <summary>
        /// Student Mobile
        /// </summary>
        [Required(ErrorMessage = "Mobile is required.")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "Invalid Mobile Number.")]
        public string Mobile { get; set; }

        /// <summary>
        /// Student gender
        /// </summary>
        [Required(ErrorMessage = "Gender is required, could be Male(0) or Female(1)!")]
        public Gender Gender { get; set; }

        /// <summary>
        /// Student Age
        /// </summary>
        [Required(ErrorMessage = "Age is required!")]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Age is required, could be between 10 to 70!")]
        [Range(10, 70)]
        public int Age { get; set; }

        /// <summary>
        /// Student Address
        /// </summary>
        [Required(ErrorMessage = "Address is required!")]
        [StringLength(200, ErrorMessage = "Address should not be more than 180 character!")]
        public string Address { get; set; }

        /// <summary>
        /// Student ZipCode
        /// </summary>
        [Required(ErrorMessage = "ZipCode is required!")]
        [StringLength(6, MinimumLength = 6, ErrorMessage = "Enter a valid zipcode like 650076")]
        [RegularExpression(@"^[1-9]\d*$", ErrorMessage = "Enter a valid zipcode like 650076")]
        public string ZipCode { get; set; }

        /// <summary>
        /// To check whether Student IsActive or not
        /// </summary>
        public bool IsActive { get; set; } = true;
    }
}
