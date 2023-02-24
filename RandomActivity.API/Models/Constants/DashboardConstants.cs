namespace RandomActivity.API.Models.Constants
{
    public class DashboardConstants
    {
        public const string MEDIA_TYPE = "application/json";
        public const string MAINFRAME_DATE_FORMAT = "yyyyMMdd";
        public const string MAINFRAME_DATE_FORMAT_DASH = "yyyy-MM-dd";
        public const string MAINFRAME_DATE_FORMAT_DASH_yyyyMM = "yyyy-MM";
        public const string MAINFRAME_DATE_FORMAT_ALTERNATE = "dd MMM yyyy";
        public const string MICROSERVICE_DATE_FORMAT = "yyyy-MM-dd HH:mm:ss";
        public const string YEAR_FORMAT = "yyyy";

        // MF Integration
        public const string MF_APIKEY = "apikey";
        public const string MF_RetirementTopUpAXPAPIKey = "axp-apikey";

        // Service Level Error Code
        public const string MF_GET_RSTU_HISTORY_SUCCESS = "00";

        // Service Name
        public const string MF_INTEGRATION = "MFIntegration";

        public const string MS_NAME = "Retirement";

        // API Validation Level Code
        public const string CPF_NUMBER_NULL_VALIDATION = "01";
        public const string DATE_INPUT_VALIDATION = "02";
        public const string NO_DATA_FOUND_VALIDATION = "03";

        // API Alert Message Code
        public const string DEFAULT_ERROR_MESSAGE = "01";
        public const string NO_DATA_FOUND_MESSAGE = "02";
        public const string RETIREMENT_PLANNING_REDIRECT_MESSAGE = "03";

        //External System Names
        public const string MTP_MRT_EXTERNAL_SYSTEM = "MTP MRT";


        //Some URLs to be used in AEM side
        public const string MONTHLY_STATEMENT_URL = "/retirement/monthly-payouts-history";
        public const string YEARLY_STATEMENT_URL = "/retirement/yearly-payouts-history";

        public const string VARIANT12_APPLY_NOW_URL = "/eSvc/Web/Schemes/MonthlyPayoutsFromMyRetirementAccount/BeginRequest";
        public const string VARIANT15_APPLY_NOW_URL = "/member/tools-and-services/forms-e-applications/plan-my-monthly-payouts";
        public const string VARIANT18_APPLY_NOW_URL = "/member/tools-and-services/forms-e-applications/plan-my-monthly-payouts";

        public const string CTA_TO_RSTU_URL = "/eSvc/Web/Miscellaneous/Cashier/ECashierHomepage";

        public const string WITHDRAWAL_DETAILS_BOOST_MONTHLY_PAYOUT_URL = "/eSvc/Web/Schemes/TransferAmountSpecialAccountToRetirementAccount/Index";

        public const string WITHDRAWAL_DETAILS_WITHDRAWAL_BUTTON_URL = "/eSvc/Web/Schemes/DecideOnMyCPFOptionsAbove55/CoverPage";

        public const string WITHDRAWAL_DETAILS_MY_PLAN_PAYOUT_URL = "/member/tools-and-services/forms-e-applications/plan-my-monthly-payouts";

        #region GetWithdrawalHistory MS API -> PmtDtlLine1 Value(s)

        #region List of Payment Mode returned by GetRAPmtDetail and GetWDLPmtDtl MF APIs

        public const string INTERBANK_GIRO_CODE = "I";
        public const string CHEQUE_CODE = "C";
        public const string PAYNOW_CODE = "P";
        public const string TELEGRAPHIC_TRANSFER_CODE = "T";
        public const string DEMAND_DRAFT_CODE = "D";

        public static readonly List<string> PAYMENT_MODE_CODE_LIST = new List<string>()
            {
                INTERBANK_GIRO_CODE,CHEQUE_CODE,PAYNOW_CODE,TELEGRAPHIC_TRANSFER_CODE,DEMAND_DRAFT_CODE
            };

        #endregion

        #region Non-3rd Party Payment

        public const string PMTDTLLINE1_NON_3RDPARTY_PAYMENT_MODE_PAYNOW = "PayNow";

        public const string PMTDTLLINE1_NON_3RDPARTY_PAYMENT_MODE_IBG = "Interbank GIRO to {_bankName_} {_bankAccountNo_}";

        public const string PMTDTLLINE1_NON_3RDPARTY_PAYMENT_MODE_CHEQUE = "Cheque payment";

        public const string PMTDTLLINE1_NON_3RDPARTY_PAYMENT_MODE_TELEGRAPHIC_TRANSFER = "Telegraphic Transfer to {_bankName_} {_bankAccountNo_}";

        public const string PMTDTLLINE1_NON_3RDPARTY_PAYMENT_MODE_DEMAND_DRAFT = "Demand Draft";

        public static readonly Dictionary<string, string> PMTDTLLINE1_NON_3RDPARTY_DICTIONARY = new Dictionary<string, string>
            {
                {INTERBANK_GIRO_CODE, PMTDTLLINE1_NON_3RDPARTY_PAYMENT_MODE_IBG},
                {CHEQUE_CODE, PMTDTLLINE1_NON_3RDPARTY_PAYMENT_MODE_CHEQUE},
                {PAYNOW_CODE, PMTDTLLINE1_NON_3RDPARTY_PAYMENT_MODE_PAYNOW},
                {TELEGRAPHIC_TRANSFER_CODE, PMTDTLLINE1_NON_3RDPARTY_PAYMENT_MODE_TELEGRAPHIC_TRANSFER},
                {DEMAND_DRAFT_CODE, PMTDTLLINE1_NON_3RDPARTY_PAYMENT_MODE_DEMAND_DRAFT}
            };

        #endregion

        #region 3rd Party Payment

        public const string PMTDTLLINE1_3RDPARTY_PAYMENT_MODE_IBG_DEFAULT = "through {_payeeName_} {_payeeIdNo_} via Interbank GIRO to {_bankName_} {_bankAccountNo_}";

        public const string PMTDTLLINE1_3RDPARTY_PAYMENT_MODE_CHEQUE_DEFAULT = "through {_payeeName_} {_payeeIdNo_} via cheque payment";

        public const string PMTDTLLINE1_3RDPARTY_PAYMENT_MODE_TELEGRAPHIC_TRANSFER_DEFAULT = "through {_payeeName_} {_payeeIdNo_} via Telegraphic Transfer to {_bankName_} {_bankAccountNo_}";

        public const string PMTDTLLINE1_3RDPARTY_PAYMENT_MODE_DEMAND_DRAFT_DEFAULT = "through {_payeeName_} {_payeeIdNo_} via Demand Draft";

        public static readonly Dictionary<string, string> PMTDTLLINE1_3RDPARTY_WITHOUT_BRACKET_DICTIONARY = new Dictionary<string, string>
            {
                {INTERBANK_GIRO_CODE, PMTDTLLINE1_3RDPARTY_PAYMENT_MODE_IBG_DEFAULT},
                {CHEQUE_CODE, PMTDTLLINE1_3RDPARTY_PAYMENT_MODE_CHEQUE_DEFAULT},
                {TELEGRAPHIC_TRANSFER_CODE, PMTDTLLINE1_3RDPARTY_PAYMENT_MODE_TELEGRAPHIC_TRANSFER_DEFAULT},
                {DEMAND_DRAFT_CODE, PMTDTLLINE1_3RDPARTY_PAYMENT_MODE_DEMAND_DRAFT_DEFAULT}
            };


        public const string PMTDTLLINE1_3RDPARTY_PAYMENT_MODE_IBG_WITH_BRACKET = "through {_payeeName_} ({_payeeIdNo_}) via Interbank GIRO to {_bankName_} {_bankAccountNo_}";

        public const string PMTDTLLINE1_3RDPARTY_PAYMENT_MODE_CHEQUE_WITH_BRACKET = "through {_payeeName_} ({_payeeIdNo_}) via cheque payment";

        public const string PMTDTLLINE1_3RDPARTY_PAYMENT_MODE_TELEGRAPHIC_TRANSFER_WITH_BRACKET = "through {_payeeName_} ({_payeeIdNo_}) via Telegraphic Transfer to {_bankName_} {_bankAccountNo_}";

        public const string PMTDTLLINE1_3RDPARTY_PAYMENT_MODE_DEMAND_DRAFT_WITH_BRACKET = "through {_payeeName_} ({_payeeIdNo_}) via Demand Draft";

        public static readonly Dictionary<string, string> PMTDTLLINE1_3RDPARTY_WITH_BRACKET_DICTIONARY = new Dictionary<string, string>
            {
                {INTERBANK_GIRO_CODE, PMTDTLLINE1_3RDPARTY_PAYMENT_MODE_IBG_WITH_BRACKET},
                {CHEQUE_CODE, PMTDTLLINE1_3RDPARTY_PAYMENT_MODE_CHEQUE_WITH_BRACKET},
                {TELEGRAPHIC_TRANSFER_CODE, PMTDTLLINE1_3RDPARTY_PAYMENT_MODE_TELEGRAPHIC_TRANSFER_WITH_BRACKET},
                {DEMAND_DRAFT_CODE, PMTDTLLINE1_3RDPARTY_PAYMENT_MODE_DEMAND_DRAFT_WITH_BRACKET}
            };

        #endregion


        #endregion

        #region GetWithdrawalHistory MS API -> DeductedFrom Value(s) For ObjPEAWDL

        public const string ORDINARY_ACCOUNT = "Ordinary Account";
        public const string RETIREMENT_ACCOUNT = "Retirement Account";

        public static readonly Dictionary<string, string> DEDUCTEDFROM_DICTIONARY = new Dictionary<string, string>
            {
                {"Q", ORDINARY_ACCOUNT},
                {"R", RETIREMENT_ACCOUNT}
            };

        #endregion
    }
}
