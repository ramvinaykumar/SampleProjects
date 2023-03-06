using Newtonsoft.Json;
using System.ComponentModel;

namespace Code.Practice.Samples.BankAccountNumberValidation
{
    public class BankAccountValidationConstants
    {
        // Bank Account Patterns

        /// <summary>
        /// Valid bank account pattern 999-9-999999 for DBS
        /// </summary>
        public static string DbsAccountPattern1 = "^[0-9]{3}[-][0-9]{1}[-][0-9]{6}$";

        /// <summary>
        /// Valid bank account pattern 999-999999-9 for DBS
        /// </summary>
        public static string DbsAccountPattern2 = "^[0-9]{3}[-][0-9]{6}[-][0-9]{1}$";

        /// <summary>
        /// Valid bank account pattern 999-99999-9 for POSB
        /// </summary>
        public static string PosbAccountPattern = "^[0-9]{3}[-][0-9]{5}[-][0-9]{1}$";

        /// <summary>
        /// Valid bank account pattern 999-9-999999 for OCBC
        /// </summary>
        public static string OcbcAccountPattern1 = "^[0-9]{3}[-][0-9]{1}[-][0-9]{6}$";

        /// <summary>
        /// Valid bank account pattern 999-999999-999 for OCBC
        /// </summary>
        public static string OcbcAccountPattern2 = "^[0-9]{3}[-][0-9]{6}[-][0-9]{3}$";

        /// <summary>
        /// Valid bank account pattern 999-999-999-9 for UOB
        /// </summary>
        public static string UobAccountPattern = "^[0-9]{3}[-][0-9]{3}[-][0-9]{3}[-][0-9]{1}$";
    }

    public enum BankNameEnum
    {
        [Description("DBS")]
        DBS,
        [Description("POSB")]
        POSB,
        [Description("OCBC")]
        OCBC,
        [Description("UOB")]
        UOB,
        [Description("MBB")]
        MBB
    }

    public class ValidatePaymentInputRequestDto
    {
        [JsonProperty("bankName")]
        public string BankName { get; set; } = string.Empty;

        [JsonProperty("bankAccountNumber")]
        public string BankAccountNumber { get; set; } = string.Empty;
    }

    public class ValidatePaymentInputResponseDto
    {
        [JsonProperty("isValidInput")]
        public bool IsValidInput { get; set; }
    }
}
