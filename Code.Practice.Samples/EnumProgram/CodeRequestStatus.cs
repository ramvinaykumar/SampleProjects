using Code.Practice.Samples.Common;

namespace Code.Practice.Samples.EnumProgram
{
    public enum CodeRequestStatus
    {
        [DescriptionWithString("Pending (Uploading)", "00")]
        code0 = 1,
        [DescriptionWithString("Pending (Acceptance)", "01")]
        code1,
        [DescriptionWithString("Successfully Processed", "02")]
        code2,
        [DescriptionWithString("Not Successful", "03")]
        code3,
        [DescriptionWithString("Superseded", "04")]
        code4,
        [DescriptionWithString("Pending User Acceptance", "05")]
        code5,
        [DescriptionWithString("Pending User Manager Acceptance", "06")]
        code6,
        [DescriptionWithString("User Accepted Application", "07")]
        code7,
        [DescriptionWithString("User Rejected Application", "08")]
        code8,
        [DescriptionWithString("Pending Closure", "09")]
        code9,
        [DescriptionWithString("Cancelled", "10")]
        code10,
        [DescriptionWithString("Completed", "11")]
        code11,
        [DescriptionWithString("In-edit", "12")]
        code12,
        [DescriptionWithString("Submitted", "13")]
        code13,
        [DescriptionWithString("Payment Approved", "14")]
        code14,
        [DescriptionWithString("Payment Rejected", "15")]
        code15,
        [DescriptionWithString("Payment Incomplete", "16")]
        code16,
        [DescriptionWithString("Pending (Approval)", "17")]
        code17,
        [DescriptionWithString("Pending (Principal Officer Approval)", "18")]
        code18,
        [DescriptionWithString("Approved", "35")]
        code35,
        [DescriptionWithString("Rejected", "36")]
        code36,
        [DescriptionWithString("Pending Reply", "23")]
        code23,
        [DescriptionWithString("Pending Document", "24")]
        code24,
        [DescriptionWithString("Pending Payment", "25")]
        code25,
        [DescriptionWithString("Pending Receipt of Supporting Documents", "26")]
        code26,
        [DescriptionWithString("Pending Processing", "27")]
        code27,
        [DescriptionWithString("Under Review", "28")]
        code28,
        [DescriptionWithString("Pending Evaluation By Consultant", "29")]
        code29,
        [DescriptionWithString("Status Updated", "31")]
        code31,
        [DescriptionWithString("Pending(Co-Owner)", "32")]
        code32,
        [DescriptionWithString("Pending Financiers Endorsement", "33")]
        code33,
        [DescriptionWithString("Partially Successful", "34")]
        code34,
        [DescriptionWithString("Application Accepted", "37")]
        code37,
        [DescriptionWithString("Member Rejected Application", "38")]
        code38,
        [DescriptionWithString("Pending Retirement", "40")]
        code40,
        [DescriptionWithString("Pending Healthcare", "42")]
        code42,
        [DescriptionWithString("Pending Insurance", "43")]
        code43,
        [DescriptionWithString("Pending Withdrawal Under Non-Retirement Grounds", "44")]
        code44,
        [DescriptionWithString("Pending Self-Employed", "45")]
        code45,
        [DescriptionWithString("Pending Housing", "47")]
        code47,
        [DescriptionWithString("Pending Education", "52")]
        code52,
        [DescriptionWithString("Pending Investment", "54")]
        code54,
        [DescriptionWithString("Pending MAR", "56")]
        code56,
        [DescriptionWithString("Pending Nomination", "58")]
        code58,
        [DescriptionWithString("Pending GIRO", "60")]
        code60,
        [DescriptionWithString("Pending Refund", "63")]
        code63,
        [DescriptionWithString("Pending Adjustment", "64")]
        code64,
        [DescriptionWithString("Pending Waiver", "65")]
        code65,
        [DescriptionWithString("Pending Lifelong Income", "70")]
        code70,
        [DescriptionWithString("Pending GML", "66")]
        code66,
        [DescriptionWithString("Pending FWL", "72")]
        code72,
        [DescriptionWithString("Pending ERN", "73")]
        code73,
        [DescriptionWithString("Pending WIS", "76")]
        code76,
        [DescriptionWithString("Approved in principle", "30")]
        code30,
        [DescriptionWithString("Pending Housing", "41")]
        code41,
        [DescriptionWithString("Pending Retirement", "46")]
        code46,
        [DescriptionWithString("Pending Healthcare", "48")]
        code48,
        [DescriptionWithString("Pending Insurance", "49")]
        code49,
        [DescriptionWithString("Pending Withdrawal under non Retirement grounds", "50")]
        code50,
        [DescriptionWithString("Pending Self-Employed", "51")]
        code51,
        [DescriptionWithString("Pending Nomination", "53")]
        code53,
        [DescriptionWithString("Pending GIRO", "55")]
        code55,
        [DescriptionWithString("Pending Education", "57")]
        code57,
        [DescriptionWithString("Pending Investment", "59")]
        code59,
        [DescriptionWithString("Pending MAR", "61")]
        code61,
        [DescriptionWithString("Pending GML", "62")]
        code62,
        [DescriptionWithString("Pending Lifelong Income", "71")]
        code71,
        [DescriptionWithString("Pending Refund", "67")]
        code67,
        [DescriptionWithString("Pending Adjustment", "68")]
        code68,
        [DescriptionWithString("Pending Waiver", "69")]
        code69,
        [DescriptionWithString("Pending FWL", "74")]
        code74,
        [DescriptionWithString("Pending ERN", "75")]
        code75,
        [DescriptionWithString("Pending WIS", "77")]
        code77,
        [DescriptionWithString("Pending HBL", "78")]
        code78,
        [DescriptionWithString("Pending HBL", "79")]
        code79,
        [DescriptionWithString("Pending Retirement Withdrawals", "80")]
        code80,
        [DescriptionWithString("Pending Retirement Withdrawals", "81")]
        code81,
        [DescriptionWithString("Pending RWD", "82")]
        code82,
        [DescriptionWithString("In-Progress", "83")]
        code83,
        [DescriptionWithString("Failed", "84")]
        code84,
        [DescriptionWithString("Pending Witnessing", "85")]
        code85,
        [DescriptionWithString("Expired", "86")]
        code86,
        [DescriptionWithString("Withdrawn", "87")]
        code87,
        [DescriptionWithString("Pending Review", "88")]
        code88
    }


    public enum A55WithdrawalStatus
    {
        [DescriptionWithString("-", "")]
        None,
        [DescriptionWithString("Successful", "SC")]
        SC,
        [DescriptionWithString("In-Progress", "PQP")]
        PQP,
        [DescriptionWithString("In-Progress", "PV")]
        PV,
        [DescriptionWithString("Not Successful", "RJ")]
        RJ,
        [DescriptionWithString("Cancelled", "C")]
        C
    }
}
