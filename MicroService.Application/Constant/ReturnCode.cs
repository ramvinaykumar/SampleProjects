namespace MicroService.Application.Constant
{
    public static class ReturnCode
    {
        public const string Success = "00";

        public const string BusinessError = "01";

        public const string TechnicalError = "02";

        public const string Error = "03";
    }

    public static class LoggerConstants
    {
        public const string LogInfo = "Message:{Message}. Application:{Application}. Class:{Class}. Method:{Method}";

        public const string LogDebug = "Message:{Message}. Application:{Application}. Class:{Class}. Method:{Method}";

        public const string LogError = "Message:{Message}. Application:{Application}. Class:{Class}. Method:{Method}. Stacktrace :{Stacktrace}";

        public const string LogWarning = "Message:{Message}. Application:{Application}. Class:{Class}. Method:{Method}";
    }
}
