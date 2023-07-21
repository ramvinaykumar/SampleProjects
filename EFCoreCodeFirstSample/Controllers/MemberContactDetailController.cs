using EFCoreCodeFirstSample.Entity;
using EFCoreCodeFirstSample.Models.ContactDetails;
using EFCoreCodeFirstSample.Repository.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EFCoreCodeFirstSample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberContactDetailController : ControllerBase
    {
        private readonly IMemberContactDetailService _memberContactDetailService;

        public MemberContactDetailController(IMemberContactDetailService contactDetailService)
        {
            _memberContactDetailService = contactDetailService;
        }

        /// <summary>
        /// Get member contact list
        /// </summary>
        /// <returns></returns>
        [HttpGet("MemberContactDetailList")]
        public IActionResult GetList()
        {
            try
            {
                var contactDetails = _memberContactDetailService.GetMemberContactList();
                if (contactDetails == null) return NotFound();
                return Ok(contactDetails);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Get MemberContact details by accountNumber
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        [HttpGet("MemberContactDetailById")]
        public IActionResult GetById(string accountNumber)
        {
            try
            {
                var contactDetails = _memberContactDetailService.GetMemberContactDetailsById(accountNumber);
                if (contactDetails == null) return NotFound();
                return Ok(contactDetails);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        ///  Add edit MemberContact
        /// </summary>
        /// <param name="memberContact"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Save(MemberContactDetailsEntity memberContact)
        {
            try
            {
                //var model = _memberContactDetailService.SaveMemberContactDetail(memberContact);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Delete MemberContact
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult Delete(string accountNumber)
        {
            try
            {
                var model = _memberContactDetailService.DeleteMemberContactDetail(accountNumber);
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Validate Email Address
        /// </summary>
        /// <param name="emailAddress">string emailAddress</param>
        /// <returns></returns>
        [HttpPost("ValidateEmail")]
        public IActionResult EmailValidation(string emailAddress)
        {
            try
            {
                var model = _memberContactDetailService.ValidateEmailAddress(emailAddress);
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Validate Mobile Number
        /// </summary>
        /// <param name="mobileNumber">string mobileNumber</param>
        /// <param name="isSgNumber">bool isSgNumber</param>
        /// <returns></returns>
        [HttpPost("ValidateMobile")]
        public IActionResult MobileValidation(string mobileNumber, bool isSgNumber)
        {
            try
            {
                var model = _memberContactDetailService.ValidationMobileNumber(mobileNumber, isSgNumber);
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Validate Account Number
        /// </summary>
        /// <param name="accountNumber">string accountNumber</param>
        /// <returns></returns>
        [HttpPost("ValidateAccountNumber")]
        public IActionResult AccountNumberValidation(string accountNumber)
        {
            try
            {
                var model = _memberContactDetailService.ValidateAccountNumber(accountNumber);
                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
