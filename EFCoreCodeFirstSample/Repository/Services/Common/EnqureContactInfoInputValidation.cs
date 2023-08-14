using EFCoreCodeFirstSample.Constants;
using EFCoreCodeFirstSample.Models.ContactDetails;
using System.Text.RegularExpressions;

namespace EFCoreCodeFirstSample.Repository.Services.Common
{
    /// <summary>
    /// Enqure Contact Info Input Validation Helper Class
    /// </summary>
    public static class EnqureContactInfoInputValidation
    {
        #region Variables

        public const string ValidAlphaNumericRegex = @"^[a-zA-Z0-9]+$";
        public const string ValidLettersRegex = @"^[a-zA-Z]+$";
        public const string ValidNumbersRegex = @"^[0-9]+$";
        public const string ValidOverseaNumberRegex = @"^[0-9 -]{0,20}$";
        public const string ValidEmailRegex = @"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$";

        #endregion

        #region Public methods

        /// <summary>
        /// Validate CpfAccountNumber And ReturnMessage
        /// </summary>
        /// <param name="cpfAccountNumber">string cpfAccountNumber</param>
        /// <returns></returns>
        public static string ValidateCpfAccountNumberAndReturnMessage(string cpfAccountNumber)
        {
            string returnMessage = string.Empty;

            var length = cpfAccountNumber.Length;
            if (length == 9)
            {
                var isAlphaNumeric = IsOnlyAlphaNumericData(cpfAccountNumber);

                if (isAlphaNumeric)
                {
                    var firstLetter = cpfAccountNumber.Substring(0, 1);
                    var lastLetter = cpfAccountNumber.Substring(length - 1, 1);

                    var isFirstDigitLetter = IsOnlyCharacter(firstLetter);
                    var isLastDigitLetter = IsOnlyCharacter(lastLetter);

                    if (!isFirstDigitLetter)
                    {
                        return returnMessage = string.Format(ValidationConstants.Invalid_AccountNumber_FirstChar, cpfAccountNumber, firstLetter);
                    }
                    if (!isLastDigitLetter)
                    {
                        return returnMessage = string.Format(ValidationConstants.Invalid_AccountNumber_LastChar, cpfAccountNumber, lastLetter);
                    }

                    // Check whether first letter start with S or T
                    var firstLetterUpper = firstLetter?.ToUpperInvariant();
                    if (firstLetterUpper == "S" || firstLetterUpper == "T" || firstLetterUpper == "M")
                    {
                        // Remaining 6 digit should be only numbers
                        var isRemainingDigitNumber = IsNumbersOnly(cpfAccountNumber);
                        if (isRemainingDigitNumber)
                        {
                            return returnMessage = string.Empty;
                        }
                        else
                        {
                            return returnMessage = ValidationConstants.Input_Should_Be_Numbers_Olny;
                        }
                    }
                    else
                    {
                        return returnMessage = string.Format(ValidationConstants.Invalid_AccountNumber_CharShould_StartWith, cpfAccountNumber);
                    }
                }
                else
                    return returnMessage = string.Format(ValidationConstants.Invalid_AccountNumber, cpfAccountNumber);
            }
            else
                return returnMessage = string.Format(ValidationConstants.AccountNumber_Valid_Length, cpfAccountNumber, length);
        }

        /// <summary>
        /// Return Message When All Inputs Are Invalid
        /// </summary>
        /// <param name="requestDto">EnquireListOfContactInfoRequestDto requestDto</param>
        /// <returns></returns>
        public static string ReturnMessageWhenAllInputsAreInvalid(EnquireListOfContactInfoRequestDto requestDto)
        {
            string strMessage = string.Empty;
            bool isInputDataInvalid = false;

            if (!string.IsNullOrEmpty(requestDto.CpfAccountNumber))
            {
                var cpfAccount = ValidateCpfAccountNumberAndReturnMessage(requestDto.CpfAccountNumber);
                if (!string.IsNullOrEmpty(cpfAccount))
                {
                    strMessage = cpfAccount;
                    isInputDataInvalid = true;
                }
            }

            if (!string.IsNullOrEmpty(requestDto.EmailAddress))
            {
                var isEmailAddrss = IsValidateEmailAddress(requestDto.EmailAddress);
                if (!isEmailAddrss)
                {
                    strMessage = strMessage + " " + ValidationConstants.Invalid_Email_Address;
                    isInputDataInvalid = true;
                }
            }

            if (!string.IsNullOrEmpty(requestDto.MobileNumber))
            {
                var isValidMobileNumber = IsValidateMobileNumber(requestDto.MobileNumber);
                if (!isValidMobileNumber)
                {
                    strMessage = strMessage + " " + ValidationConstants.Invalid_Mobile_Number;
                    isInputDataInvalid = true;
                }
            }
            if (isInputDataInvalid)
                return strMessage.Trim();

            return ValidationConstants.Valid_Inputs;
        }

        /// <summary>
        /// Return Message When CpfAccountNumber and Email Invalid
        /// </summary>
        /// <param name="requestDto">EnquireListOfContactInfoRequestDto requestDto</param>
        /// <returns></returns>
        public static string ReturnMessageWhenCpfAccountNumberEmailInvalid(EnquireListOfContactInfoRequestDto requestDto)
        {
            string strMessage = string.Empty;
            bool isInputDataInvalid = false;

            if (!string.IsNullOrEmpty(requestDto.CpfAccountNumber))
            {
                var cpfAccount = ValidateCpfAccountNumberAndReturnMessage(requestDto.CpfAccountNumber);
                if (!string.IsNullOrEmpty(cpfAccount))
                {
                    strMessage = cpfAccount;
                    isInputDataInvalid = true;
                }
            }

            if (!string.IsNullOrEmpty(requestDto.EmailAddress))
            {
                var isEmailAddrss = IsValidateEmailAddress(requestDto.EmailAddress);
                if (!isEmailAddrss)
                {
                    strMessage = strMessage + " " + ValidationConstants.Invalid_Email_Address;
                    isInputDataInvalid = true;
                }
            }

            if (isInputDataInvalid)
                return strMessage.Trim();

            return ValidationConstants.Valid_Inputs;
        }

        /// <summary>
        /// Return Message When CpfAccountNumber and Mobile Invalid
        /// </summary>
        /// <param name="requestDto">EnquireListOfContactInfoRequestDto requestDto</param>
        /// <returns></returns>
        public static string ReturnMessageWhenCpfAccountNumberMobileInvalid(EnquireListOfContactInfoRequestDto requestDto)
        {
            string strMessage = string.Empty;
            bool isInputDataInvalid = false;

            if (!string.IsNullOrEmpty(requestDto.CpfAccountNumber))
            {
                var cpfAccount = ValidateCpfAccountNumberAndReturnMessage(requestDto.CpfAccountNumber);
                if (!string.IsNullOrEmpty(cpfAccount))
                {
                    strMessage = cpfAccount;
                    isInputDataInvalid = true;
                }
            }

            if (!string.IsNullOrEmpty(requestDto.MobileNumber))
            {
                var isValidMobileNumber = IsValidateMobileNumber(requestDto.MobileNumber);
                if (!isValidMobileNumber)
                {
                    strMessage = strMessage + " " + ValidationConstants.Invalid_Mobile_Number;
                    isInputDataInvalid = true;
                }
            }
            if (isInputDataInvalid)
                return strMessage.Trim();

            return ValidationConstants.Valid_Inputs;
        }

        /// <summary>
        /// Return Message When Email and Mobile Are Invalid
        /// </summary>
        /// <param name="requestDto">EnquireListOfContactInfoRequestDto requestDto</param>
        /// <returns></returns>
        public static string ReturnMessageWhenEmailMobileInvalid(EnquireListOfContactInfoRequestDto requestDto)
        {
            string strMessage = string.Empty;
            bool isInputDataInvalid = false;

            if (!string.IsNullOrEmpty(requestDto.EmailAddress))
            {
                var isEmailAddrss = IsValidateEmailAddress(requestDto.EmailAddress);
                if (!isEmailAddrss)
                {
                    strMessage = strMessage + " " + ValidationConstants.Invalid_Email_Address;
                    isInputDataInvalid = true;
                }
            }

            if (!string.IsNullOrEmpty(requestDto.MobileNumber))
            {
                var isValidMobileNumber = IsValidateMobileNumber(requestDto.MobileNumber);
                if (!isValidMobileNumber)
                {
                    strMessage = strMessage + " " + ValidationConstants.Invalid_Mobile_Number;
                    isInputDataInvalid = true;
                }
            }
            if (isInputDataInvalid)
                return strMessage.Trim();

            return ValidationConstants.Valid_Inputs;
        }

        /// <summary>
        /// Is Validate Email Address
        /// </summary>
        /// <param name="emailAddress">string emailAddress</param>
        /// <returns></returns>
        public static bool IsValidateEmailAddress(string emailAddress)
        {
            bool isValidEmail = false;
            if (!string.IsNullOrEmpty(emailAddress))
            {
                isValidEmail = Regex.IsMatch(emailAddress, ValidEmailRegex, RegexOptions.IgnoreCase);
            }
            return isValidEmail;
        }

        /// <summary>
        /// Is Validate Mobile Number
        /// </summary>
        /// <param name="mobileNumber">string mobileNumber</param>
        /// <returns></returns>
        public static bool IsValidateMobileNumber(string mobileNumber)
        {
            bool isValidNumber = false;
            if (!string.IsNullOrEmpty(mobileNumber))
            {
                isValidNumber = Regex.IsMatch(mobileNumber, ValidOverseaNumberRegex);
            }
            return isValidNumber;
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Validate for only alpha numeric
        /// </summary>
        /// <param name="accountNumber">string accountNumber</param>
        /// <returns></returns>
        private static bool IsOnlyAlphaNumericData(string accountNumber)
        {
            bool isOnlyAlphaNumeric = false;

            if (!string.IsNullOrEmpty(accountNumber))
            {
                isOnlyAlphaNumeric = Regex.IsMatch(accountNumber, ValidAlphaNumericRegex);
            }
            return isOnlyAlphaNumeric;
        }

        /// <summary>
        /// Validate for only characters
        /// </summary>
        /// <param name="input">string input</param>
        /// <returns></returns>
        private static bool IsOnlyCharacter(string input)
        {
            bool isValid = false;

            if (!string.IsNullOrEmpty(input) && input.Length > 0)
            {
                isValid = Regex.IsMatch(input, ValidLettersRegex);
            }
            return isValid;
        }

        /// <summary>
        /// Validate for only numbers
        /// </summary>
        /// <param name="accountNumber">string accountNumber</param>
        /// <returns></returns>
        private static bool IsNumbersOnly(string accountNumber)
        {
            bool isOnlyNumber = false;

            if (!string.IsNullOrEmpty(accountNumber))
            {
                var inputData = string.Empty;
                inputData = accountNumber.Remove(0, 2);
                inputData = inputData.Remove(inputData.Length - 1, 1);
                isOnlyNumber = Regex.IsMatch(inputData, ValidNumbersRegex);
            }
            return isOnlyNumber;
        }

        #endregion
    }
}
