namespace EFCoreCodeFirstSample.Constants
{
    public static class ValidationConstants
    {
        public static string AccountNumber_EmailAddress_MobileNumber_Blank = "Please provide value for any one of the field(s).";
        public static string Invalid_Email_Address = "Email address is not valid.";
        public static string Invalid_Mobile_Number = "Mobile number is not valid.";
        public static string Invalid_Account_Number = "This '{0}' is a not valid AccountNumber.";
        public static string Invalid_AccountNumber_EmailAddress = "This '{0}' is a not valid AccountNumber. Email address is not valid.";
        public static string Invalid_AccountNumber_MobileNumber= "This '{0}' is a not valid AccountNumber. Mobile number is not valid.";
        public static string Invalid_MobileNumber_EmailAddress = "Email address is not valid. Mobile number is not valid.";
        public static string Invalid_MobileNumber_EmailAddress_AccountNumber = "This '{0}' is a not valid AccountNumber. Email address is not valid. Mobile number is not valid.";
        public static string Invalid_AccountNumber_FirstChar = "This '{0}' is a not valid AccountNumber, as the first char - '{1}' should be a letter.";
        public static string Invalid_AccountNumber_LastChar = "This '{0}' is a not valid AccountNumber, as the last char - '{1}' should be a letter.";
        public static string Invalid_AccountNumber_CharShould_StartWith = "AccountNumber first char should be either T or S or M, but it start with '{0}', please enter valid AccountNumber.";
        public static string AccountNumber_Valid_Length = "This account number '{0}' has {1} in length. Length must be 9 char.";
        public static string Invalid_AccountNumber = "This '{0}' is a not valid AccountNumber.";
        public static string Valid_Inputs = "Inputs are valid.";
        public static string Input_Should_Be_Numbers_Olny = "3rd to 8th char should be integer.";
    }
}
