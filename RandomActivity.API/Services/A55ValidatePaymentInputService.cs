using RandomActivity.API.Models;
using System.Text.RegularExpressions;

namespace RandomActivity.API.Services
{
    public class A55ValidatePaymentInputService : IA55ValidatePaymentInputService
    {
        private readonly ILogger _logger;
        private string serviceName = "A55ValidatePaymentInputService";

        public A55ValidatePaymentInputService(ILogger<A55ValidatePaymentInputService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Validate Payment Input
        /// </summary>
        /// <param name="requestDto">Object of ValidatePaymentInputRequestDto</param>
        /// <returns></returns>
        public async Task<ValidatePaymentInputResponseDto> A55ValidatePaymentInputAsync(ValidatePaymentInputRequestDto requestDto)
        {
            _logger.LogInformation("---Start in A55ValidatePaymentInputAsync---");

            ValidatePaymentInputResponseDto responseDto = new ValidatePaymentInputResponseDto();

            var result = await IsValid(requestDto);
            responseDto.IsValidInput = result;
            responseDto.Guid = Guid.NewGuid().ToString();

            _logger.LogInformation("---End of A55ValidatePaymentInputAsync---");

            return responseDto;
        }

        /// <summary>
        /// Check if the account number is valid or not
        /// </summary>
        /// <param name="inputRequestDto">Object of ValidatePaymentInputRequestDto</param>
        /// <returns>If valid returns true</returns>
        private async Task<bool> IsValid(ValidatePaymentInputRequestDto inputRequestDto)
        {
            bool isValid = false;

            if (!string.IsNullOrWhiteSpace(inputRequestDto.BankName) && !string.IsNullOrWhiteSpace(inputRequestDto.BankAccountNumber))
            {
                BankNameEnum bank = (BankNameEnum)Enum.Parse(typeof(BankNameEnum), inputRequestDto.BankName);
                switch (bank)
                {
                    case BankNameEnum.DBS:
                        {
                            isValid = await IsAccountNumberPatternValid(inputRequestDto.BankAccountNumber, A55ImmediateNeedsWithdrawalConstants.DbsAccountPattern1, A55ImmediateNeedsWithdrawalConstants.DbsAccountPattern2);
                            break;
                        }
                    case BankNameEnum.OCBC:
                        {
                            isValid = await IsAccountNumberPatternValid(inputRequestDto.BankAccountNumber, A55ImmediateNeedsWithdrawalConstants.OcbcAccountPattern1, A55ImmediateNeedsWithdrawalConstants.OcbcAccountPattern2);
                            break;
                        }
                    case BankNameEnum.POSB:
                        {
                            isValid = await IsAccountNumberPatternValid(inputRequestDto.BankAccountNumber, A55ImmediateNeedsWithdrawalConstants.PosbAccountPattern);
                            break;
                        }
                    case BankNameEnum.UOB:
                        {
                            isValid = await IsAccountNumberPatternValid(inputRequestDto.BankAccountNumber, A55ImmediateNeedsWithdrawalConstants.UobAccountPattern);
                            break;
                        }
                    default:
                        break;
                }
            }

            return isValid;
        }

        /// <summary>
        /// To check whether the given account pattern is valid
        /// </summary>
        /// <param name="accountNumber">Bank account number</param>
        /// <param name="pattern1">Bank account pattern 1</param>
        /// <param name="pattern2">Bank account pattern 2</param>
        /// <returns>If valid returns true</returns>
        private async Task<bool> IsAccountNumberPatternValid(string accountNumber, string pattern1, string pattern2 = "")
        {
            bool isAccountNumberPatternValid = false;
            await Task.Run(() =>
            {
                if (!string.IsNullOrWhiteSpace(pattern1) && !string.IsNullOrWhiteSpace(pattern2))
                {
                    var accountPattern1 = new Regex(pattern1);
                    var accountPattern2 = new Regex(pattern2);

                    if (accountPattern1.IsMatch(accountNumber) || accountPattern2.IsMatch(accountNumber))
                        isAccountNumberPatternValid = true;
                }
                else
                {
                    var accountPattern1 = new Regex(pattern1);

                    if (accountPattern1.IsMatch(accountNumber))
                        isAccountNumberPatternValid = true;
                }
            });
            return isAccountNumberPatternValid;
        }

        /// <summary>
        /// Check whether the bank exists or not
        /// </summary>
        /// <param name="bank">Bank name</param>
        /// <returns></returns>
        private async Task<List<ExceptionsDto>> ValidateBankAsync(string bank)
        {
            _logger.LogInformation("---Start in ValidateBankAsync---");
            List<ExceptionsDto> exceptionsDtoList = new List<ExceptionsDto>();
            await Task.Run(() =>
            {
                BankNameEnum _bank;

                if (!Enum.TryParse<BankNameEnum>(bank, out _bank))
                {
                    exceptionsDtoList.Add(new ExceptionsDto(
                            A55ImmediateNeedsWithdrawalConstants.INPUT_ERROR,
                            string.Format(A55ImmediateNeedsWithdrawalConstants.CodeValueNotFound, bank),
                            serviceName
                            ));
                }
            });

            _logger.LogInformation("---End in ValidateBankAsync---");
            return exceptionsDtoList;
        }
    }
}
