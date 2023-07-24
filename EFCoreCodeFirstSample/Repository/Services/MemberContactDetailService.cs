using EFCoreCodeFirstSample.Entity;
using EFCoreCodeFirstSample.Models;
using EFCoreCodeFirstSample.Models.ContactDetails;
using EFCoreCodeFirstSample.Repository.Interface;
using System.Text.RegularExpressions;

namespace EFCoreCodeFirstSample.Repository.Services
{
    public class MemberContactDetailService : IMemberContactDetailService
    {
        private readonly EFCoreCodeContext _dbContext;

        public MemberContactDetailService(EFCoreCodeContext dbContext)
        {
            _dbContext = dbContext;
        }

        public ResponseModel DeleteMemberContactDetail(string accountNumber)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                //MemberContactDetails _temp = GetMemberContactDetailsById(accountNumber);
                //if (_temp != null)
                //{
                //    _dbContext.Remove<MemberContactDetailsEntity>(_temp);
                //    _dbContext.SaveChanges();
                //    model.IsSuccess = true;
                //    model.Messsage = "MemberContact Deleted Successfully";
                //}
                //else
                //{
                //    model.IsSuccess = false;
                //    model.Messsage = "MemberContact Not Found";
                //}
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Messsage = "Error : " + ex.Message;
            }
            return model;
        }

        //public MemberContactDetailsEntity GetMemberContactDetailsById(string accountNumber)
        //{
        //    MemberContactDetailsEntity memberContact ;
        //    try
        //    {
        //       // memberContact = _dbContext.MemberContactDetails.Where(x => x.AccountNumber == accountNumber).FirstOrDefault();

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }
        //    return memberContact;
        //}

        public List<MemberContactDetails> GetMemberContactList()
        {
            List<MemberContactDetails> memberContactList = new List<MemberContactDetails>();
            //try
            //{
            //    memberContactList = _dbContext.MemberContactDetails.ToList();
            //}
            //catch (Exception)
            //{
            //    throw;
            //}
            return memberContactList;
        }

        public ResponseModel SaveMemberContactDetail(MemberContactDetails memberContact)
        {
            ResponseModel model = new ResponseModel();
            try
            {
                //MemberContactDetails _temp = GetMemberContactDetailsById(memberContact.AccountNumber);
                //if (_temp != null)
                //{
                //    _temp.AccountNumber = memberContact.AccountNumber;
                //    _temp.ResidencePhone = memberContact.ResidencePhone;
                //    _temp.ContactInfoUpdatedOn = DateTimeOffset.Now;
                //    _temp.Salutation = memberContact.Salutation;
                //    _temp.EmailAddress = memberContact.EmailAddress;
                //    _temp.IsSyncWithMyInfo = memberContact.IsSyncWithMyInfo;
                //    _temp.Mobile = memberContact.Mobile;

                //    _dbContext.Update<MemberContactDetailsEntity>(_temp);
                //    model.Messsage = "MemberContact Update Successfully";
                //}
                //else
                //{
                //    _dbContext.Add<MemberContactDetailsEntity>(memberContact);
                //    model.Messsage = "MemberContact Inserted Successfully";
                //}
                //_dbContext.SaveChanges();
                model.IsSuccess = true;
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Messsage = "Error : " + ex.Message;
            }
            return model;
        }

        /// <summary>
        /// Validate Account Number
        /// </summary>
        /// <param name="accountNumber"></param>
        /// <returns></returns>
        public ResponseModel ValidateAccountNumber(string accountNumber)
        {
            ResponseModel model = new ResponseModel();

            try
            {
                // Check the Length must be 9 char
                var inputLength = accountNumber.Length;
                if (inputLength == 9)
                {
                    model = IsValidAccountNumber(accountNumber);
                }
                else
                {
                    model.IsSuccess = false;
                    model.Messsage = "This account number " + accountNumber + " has " + inputLength + " in length. The required length should be 9.";
                }
            }
            catch (Exception ex)
            {
                model.IsSuccess = false;
                model.Messsage = "Error : " + ex.Message;
            }
            return model;
        }

        public ResponseModel ValidateEmailAddress(string emailAddress)
        {
            throw new NotImplementedException();
        }

        public ResponseModel ValidationMobileNumber(string mobileNumber, bool isSgNumber)
        {
            throw new NotImplementedException();
        }

        MemberContactDetails IMemberContactDetailService.GetMemberContactDetailsById(string accountNumber)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get First Character
        /// </summary>
        /// <param name="accountNumber">Account number to be checked for first letter</param>
        /// <returns>Account number first character should be letter</returns>
        private string GetFirstCharacter(string accountNumber)
        {
            string result = string.Empty;

            if (!string.IsNullOrEmpty(accountNumber))
                result = accountNumber.Substring(0, 1);
            return result;
        }

        /// <summary>
        /// Get Last Character
        /// </summary>
        /// <param name="accountNumber">Account number to be checked for last letter</param>
        /// <returns>Account number last character should be letter</returns>
        private string GetLastCharacter(string accountNumber)
        {
            string result = string.Empty;

            if (!string.IsNullOrEmpty(accountNumber))
                result = accountNumber.Substring(accountNumber.Length - 1, 1);
            return result;
        }

        /// <summary>
        /// Is Digits Only
        /// </summary>
        /// <param name="accountNumber">Account number to be checked for number only</param>
        /// <returns>Return true if condition satisfy</returns>
        private bool IsDigitsOnly(string accountNumber)
        {
            bool isOnlyNumber = false;

            if (!string.IsNullOrEmpty(accountNumber))
            {
                var inputData = string.Empty;
                inputData = accountNumber.Remove(0, 1);
                inputData = inputData.Remove(inputData.Length - 1, 1);
                isOnlyNumber = Regex.IsMatch(inputData, @"^[0-9]+$");
            }
            return isOnlyNumber;
        }

        /// <summary>
        /// Is Only Alpha Numeric
        /// </summary>
        /// <param name="accountNumber">Account Number to be validated as only alpha numeric</param>
        /// <returns>Return true if condition satisfy</returns>
        private bool IsOnlyAlphaNumeric(string accountNumber)
        {
            bool isOnlyAlphaNumeric = false;

            if (!string.IsNullOrEmpty(accountNumber))
            {
                isOnlyAlphaNumeric = Regex.IsMatch(accountNumber, @"^[a-zA-Z0-9]+$");
            }
            return isOnlyAlphaNumeric;
        }

        /// <summary>
        /// Is Only Letter
        /// </summary>
        /// <param name="input">input to be checked for letter only</param>
        /// <returns>Return true if condition satisfy</returns>
        private bool IsOnlyLetter(string input)
        {
            bool isValid = false;

            if (!string.IsNullOrEmpty(input) && input.Length > 0)
            {
                isValid = Regex.IsMatch(input, @"^[a-zA-Z]+$");
            }
            return isValid;
        }

        /// <summary>
        /// Is Valid Account Number
        /// </summary>
        /// <param name="accountNumber">Account Number which need to validated should be in string format</param>
        /// <returns>Returns valid account number</returns>
        private ResponseModel IsValidAccountNumber(string accountNumber)
        {
            ResponseModel responseModel = new ResponseModel();
            bool isValidInput = false;
            string strMessage = string.Empty;

            var isAlphaNumeric = IsOnlyAlphaNumeric(accountNumber);

            if (isAlphaNumeric)
            {
                var firstLetter = GetFirstCharacter(accountNumber);
                var lastLetter = GetLastCharacter(accountNumber);

                var isFirstDigitLetter = IsOnlyLetter(firstLetter);
                var isLastDigitLetter = IsOnlyLetter(lastLetter);

                if (firstLetter == null || lastLetter == null)
                {
                    strMessage = "This '" + accountNumber + "' is a not valid account number.";
                }
                var firstLetterUpper = firstLetter?.ToUpperInvariant();
                var isNumber = IsDigitsOnly(accountNumber);

                if (isFirstDigitLetter && isLastDigitLetter)
                {
                    if (firstLetterUpper == "S" || firstLetterUpper == "T")
                    {
                        if (isNumber)
                        {
                            isValidInput = true;
                            strMessage = "This '" + accountNumber + "' is a valid account number.";
                        }
                        else
                        {
                            strMessage = "This '" + accountNumber + "' is a not valid account number.";
                        }
                    }
                    else
                    {
                        strMessage = "Account number first character should be either T or S, but it start with " + firstLetterUpper + ", please enter correct account number.";
                    }
                }
                else if (!isFirstDigitLetter && isLastDigitLetter)
                {
                    strMessage = "This '" + accountNumber + "' is a not valid account number, due to first digit - '" + firstLetter + "' should be letter.";
                }
                else if (isFirstDigitLetter && !isLastDigitLetter)
                {
                    strMessage = "This '" + accountNumber + "' is a not valid account number, due to last digit - '" + lastLetter + "' should be letter.";
                }
                else if (!isFirstDigitLetter)
                {
                    strMessage = "This '" + accountNumber + "' is a not valid account number, due to first digit - '"+ firstLetter + "' should be letter.";
                }
                else if (!isLastDigitLetter)
                {
                    strMessage = "This '" + accountNumber + "' is a not valid account number, due to last digit - '"+ lastLetter + "' should be letter.";
                }
            }
            else
            {
                strMessage = "This '" + accountNumber + "' is a not valid account number.";
            }

            responseModel.IsSuccess = isValidInput;
            responseModel.Messsage = strMessage;

            return responseModel;
        }
    }
}
