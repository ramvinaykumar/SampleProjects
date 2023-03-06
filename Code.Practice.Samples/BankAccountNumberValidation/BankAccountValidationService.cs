using System;
using System.Text.RegularExpressions;

namespace Code.Practice.Samples.BankAccountNumberValidation
{
    public class BankAccountValidationService
    {
        /// <summary>
        /// Default constructor
        /// </summary>
        public BankAccountValidationService() { }

        /// <summary>
        /// Validate Payment Input
        /// </summary>
        /// <param name="requestDto">Object of ValidatePaymentInputRequestDto</param>
        /// <returns>Return ValidatePaymentInputResponseDto</returns>
        public ValidatePaymentInputResponseDto A55ValidatePaymentInputAsync(ValidatePaymentInputRequestDto requestDto)
        {
            ValidatePaymentInputResponseDto responseDto = new ValidatePaymentInputResponseDto();

            var result = IsValid(requestDto);
            responseDto.IsValidInput = result;

            return responseDto;
        }

        /// <summary>
        /// Check if the account number is valid or not
        /// </summary>
        /// <param name="inputRequestDto">Object of ValidatePaymentInputRequestDto</param>
        /// <returns>If valid returns true</returns>
        private bool IsValid(ValidatePaymentInputRequestDto inputRequestDto)
        {
            bool isValid = false;

            var bankName = string.Empty;
            var bankAccountNumber = string.Empty;

            if (!string.IsNullOrWhiteSpace(inputRequestDto.BankName) && !string.IsNullOrWhiteSpace(inputRequestDto.BankAccountNumber))
            {
                bankName = inputRequestDto.BankName.Trim().ToUpperInvariant();
                bankAccountNumber = inputRequestDto.BankAccountNumber.Trim();
                var res = AddHiphenIntoBankAccountByBankName(bankAccountNumber, bankName, 2);

                if (bankAccountNumber.Contains('-'))
                {
                    BankNameEnum bank = (BankNameEnum)Enum.Parse(typeof(BankNameEnum), bankName);
                    switch (bank)
                    {
                        case BankNameEnum.DBS:
                            {
                                isValid = IsBankAccountNumberPatternValid(bankAccountNumber, BankAccountValidationConstants.DbsAccountPattern1, BankAccountValidationConstants.DbsAccountPattern2);
                                break;
                            }
                        case BankNameEnum.OCBC:
                            {
                                isValid = IsBankAccountNumberPatternValid(bankAccountNumber, BankAccountValidationConstants.OcbcAccountPattern1, BankAccountValidationConstants.OcbcAccountPattern2);
                                break;
                            }
                        case BankNameEnum.POSB:
                            {
                                isValid = IsBankAccountNumberPatternValid(bankAccountNumber, BankAccountValidationConstants.PosbAccountPattern);
                                break;
                            }
                        case BankNameEnum.UOB:
                            {
                                isValid = IsBankAccountNumberPatternValid(bankAccountNumber, BankAccountValidationConstants.UobAccountPattern);
                                break;
                            }
                        default:
                            break;
                    }
                }
                else
                {

                }
            }

            return isValid;
        }

        /// <summary>
        /// To check whether the given account pattern is valid
        /// </summary>
        /// <param name="bankAccountNumber">Bank account number</param>
        /// <param name="pattern1">Bank account pattern 1</param>
        /// <param name="pattern2">Bank account pattern 2</param>
        /// <returns>If valid returns true</returns>
        private bool IsBankAccountNumberPatternValid(string bankAccountNumber, string pattern1, string pattern2 = "")
        {
            bool isAccountNumberPatternValid = false;
            //await Task.Run(() =>
            //{

            //});
            if (!string.IsNullOrWhiteSpace(pattern1) && !string.IsNullOrWhiteSpace(pattern2))
            {
                var accountPattern1 = new Regex(pattern1);
                var accountPattern2 = new Regex(pattern2);

                if (accountPattern1.IsMatch(bankAccountNumber) || accountPattern2.IsMatch(bankAccountNumber))
                    isAccountNumberPatternValid = true;
            }
            else
            {
                var accountPattern1 = new Regex(pattern1);

                if (accountPattern1.IsMatch(bankAccountNumber))
                    isAccountNumberPatternValid = true;
            }
            return isAccountNumberPatternValid;
        }

        /// <summary>
        /// Adding Special Character '-' to format the given number
        /// </summary>
        /// <param name="bankAccountNumber">string bankAccountNumber</param>
        /// <param name="bankName">string bankName</param>
        /// <param name="patternNumber">int patternNumber</param>
        /// <returns>Returns formatted account number</returns>
        private string AddHiphenIntoBankAccountByBankName(string bankAccountNumber, string bankName, int patternNumber)
        {
            string formattedBankAccountNumberPattern = string.Empty;
            var length = bankAccountNumber.Length;
            var valueToInsert = "-";

            BankNameEnum bank = (BankNameEnum)Enum.Parse(typeof(BankNameEnum), bankName);
            switch (bank)
            {
                case BankNameEnum.DBS:
                    {
                        if (patternNumber == 1 && length == 10)
                        {
                            formattedBankAccountNumberPattern = bankAccountNumber.Insert(3, valueToInsert);
                            formattedBankAccountNumberPattern = formattedBankAccountNumberPattern.Insert(5, valueToInsert);
                        }
                        else
                        {
                            formattedBankAccountNumberPattern = bankAccountNumber.Insert(3, valueToInsert);
                            formattedBankAccountNumberPattern = formattedBankAccountNumberPattern.Insert(10, valueToInsert);
                        }
                        break;
                    }
                case BankNameEnum.OCBC:
                    {
                        if (patternNumber == 1 && length == 10)
                        {
                            formattedBankAccountNumberPattern = bankAccountNumber.Insert(3, valueToInsert);
                            formattedBankAccountNumberPattern = formattedBankAccountNumberPattern.Insert(5, valueToInsert);
                        }
                        else if (patternNumber == 2 && length == 12)
                        {
                            formattedBankAccountNumberPattern = bankAccountNumber.Insert(3, valueToInsert);
                            formattedBankAccountNumberPattern = formattedBankAccountNumberPattern.Insert(10, valueToInsert);
                        }
                        break;
                    }
                case BankNameEnum.POSB:
                    {
                        if (patternNumber == 1 && length == 9)
                        {
                            formattedBankAccountNumberPattern = bankAccountNumber.Insert(3, valueToInsert);
                            formattedBankAccountNumberPattern = formattedBankAccountNumberPattern.Insert(9, valueToInsert);
                        }
                        break;
                    }
                case BankNameEnum.UOB:
                    {
                        if (patternNumber == 1 && length == 10)
                        {
                            formattedBankAccountNumberPattern = bankAccountNumber.Insert(3, valueToInsert);
                            formattedBankAccountNumberPattern = formattedBankAccountNumberPattern.Insert(7, valueToInsert);
                            formattedBankAccountNumberPattern = formattedBankAccountNumberPattern.Insert(11, valueToInsert);
                        }
                        break;
                    }
                default:
                    break;
            }

            return formattedBankAccountNumberPattern;
        }
    }
}
