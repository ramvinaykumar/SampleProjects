namespace RandomActivity.API.Models
{
    public static class A55ImmediateNeedsWithdrawalConstants
    {
        public const string SUCCESS = "00";
        public const string INPUT_ERROR = "01";
        public const string ROUTINE_ERROR = "02";
        public const string UNKNOWN_ERROR = "99";
        public const string ErrorCodeNotFound = "Unable to find error code : {0}";
        public const string CodeValueNotFound = "{0} is not found in Database";

        // Bank Account Patterns
        public const string DbsAccountPattern1 = "^[0-9]{3}[-][0-9]{1}[-][0-9]{6}$";

        public const string DbsAccountPattern2 = "^[0-9]{3}[-][0-9]{6}[-][0-9]{1}$";

        public const string PosbAccountPattern = "^[0-9]{3}[-][0-9]{5}[-][0-9]{1}$";

        public const string OcbcAccountPattern1 = "^[0-9]{3}[-][0-9]{1}[-][0-9]{6}$";

        public const string OcbcAccountPattern2 = "^[0-9]{3}[-][0-9]{6}[-][0-9]{3}$";

        public const string UobAccountPattern = "^[0-9]{3}[-][0-9]{3}[-][0-9]{3}[-][0-9]{1}$";
    }
}
