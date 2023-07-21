using EFCoreCodeFirstSample.Entity;
using EFCoreCodeFirstSample.Models;
using EFCoreCodeFirstSample.Models.ContactDetails;

namespace EFCoreCodeFirstSample.Repository.Interface
{
    public interface IMemberContactDetailService
    {
        /// <summary>
        /// Get member contact list
        /// </summary>
        /// <returns></returns>
        List<MemberContactDetails> GetMemberContactList();

        /// <summary>
        /// Get MemberContact details by accountNumber
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        MemberContactDetails GetMemberContactDetailsById(string accountNumber);

        /// <summary>
        ///  Add edit MemberContact
        /// </summary>
        /// <param name="memberContact"></param>
        /// <returns></returns>
        ResponseModel SaveMemberContactDetail(MemberContactDetails memberContact);


        /// <summary>
        /// Delete MemberContact
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        ResponseModel DeleteMemberContactDetail(string accountNumber);

        /// <summary>
        /// Validate Email Address
        /// </summary>
        /// <param name="emailAddress">string emailAddress</param>
        /// <returns></returns>
        ResponseModel ValidateEmailAddress(string emailAddress);

        /// <summary>
        /// Validate Mobile Number
        /// </summary>
        /// <param name="mobileNumber">string mobileNumber</param>
        /// <param name="isSgNumber">bool isSgNumber</param>
        /// <returns></returns>
        ResponseModel ValidationMobileNumber(string mobileNumber, bool isSgNumber);

        /// <summary>
        /// Validate Account Number
        /// </summary>
        /// <param name="accountNumber">string accountNumber</param>
        /// <returns></returns>
        ResponseModel ValidateAccountNumber(string accountNumber);
    }
}
