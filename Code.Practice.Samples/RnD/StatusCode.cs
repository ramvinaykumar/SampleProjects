using Code.Practice.Samples.Common;
using Code.Practice.Samples.EnumProgram;

namespace Code.Practice.Samples.RnD
{
    public static class StatusCode
    {
        public static CodeRequestStatus GetTransactionStatus(int lsRequestedsStatus, int fiftyFiveRequestedStatus)
        {
            string lumpSumwithdrawalRequestedstatus = string.Empty;
            string fiftyFiveWithdrawalRequestedstatus = string.Empty;

            // Check process type and assign the value
            if (fiftyFiveRequestedStatus == 1)
                fiftyFiveWithdrawalRequestedstatus = A55WithdrawalStatus.SC.GetEnumStringCode();
            else if (fiftyFiveRequestedStatus == 2)
                fiftyFiveWithdrawalRequestedstatus = A55WithdrawalStatus.RJ.GetEnumStringCode();
            else if (fiftyFiveRequestedStatus == 3)
                fiftyFiveWithdrawalRequestedstatus = A55WithdrawalStatus.C.GetEnumStringCode();
            else
                fiftyFiveWithdrawalRequestedstatus = string.Empty;

            if (lsRequestedsStatus == 1)
                lumpSumwithdrawalRequestedstatus = A55WithdrawalStatus.SC.GetEnumStringCode();
            else if (lsRequestedsStatus == 2)
                lumpSumwithdrawalRequestedstatus = A55WithdrawalStatus.RJ.GetEnumStringCode();
            else if (lsRequestedsStatus == 3)
                lumpSumwithdrawalRequestedstatus = A55WithdrawalStatus.C.GetEnumStringCode();
            else
                lumpSumwithdrawalRequestedstatus = string.Empty;

            CodeRequestStatus finalStatus = new CodeRequestStatus();

            if (!string.IsNullOrEmpty(lumpSumwithdrawalRequestedstatus) && !string.IsNullOrEmpty(fiftyFiveWithdrawalRequestedstatus))
            {
                if (fiftyFiveWithdrawalRequestedstatus == A55WithdrawalStatus.RJ.ToString() && lumpSumwithdrawalRequestedstatus == A55WithdrawalStatus.RJ.ToString())
                {
                    finalStatus = CodeRequestStatus.code3;
                }
                else if (fiftyFiveWithdrawalRequestedstatus == A55WithdrawalStatus.C.ToString() && lumpSumwithdrawalRequestedstatus == A55WithdrawalStatus.C.ToString())
                {
                    finalStatus = CodeRequestStatus.code10;
                }
                else if (fiftyFiveWithdrawalRequestedstatus == A55WithdrawalStatus.SC.ToString() && lumpSumwithdrawalRequestedstatus == A55WithdrawalStatus.C.ToString())
                {
                    finalStatus = CodeRequestStatus.code11;
                }
                else if (fiftyFiveWithdrawalRequestedstatus == A55WithdrawalStatus.RJ.ToString() && lumpSumwithdrawalRequestedstatus == A55WithdrawalStatus.C.ToString())
                {
                    finalStatus = CodeRequestStatus.code3;
                }
                else
                {
                    finalStatus = CodeRequestStatus.code11;
                }
            }
            else if (!string.IsNullOrEmpty(lumpSumwithdrawalRequestedstatus) && string.IsNullOrEmpty(fiftyFiveWithdrawalRequestedstatus))
            {
                if (lumpSumwithdrawalRequestedstatus == A55WithdrawalStatus.RJ.ToString())
                {
                    finalStatus = CodeRequestStatus.code3;
                }
                else if (lumpSumwithdrawalRequestedstatus == A55WithdrawalStatus.C.ToString())
                {
                    finalStatus = CodeRequestStatus.code10;
                }
                else
                {
                    finalStatus = CodeRequestStatus.code11;
                }
            }
            else if (string.IsNullOrEmpty(lumpSumwithdrawalRequestedstatus) && !string.IsNullOrEmpty(fiftyFiveWithdrawalRequestedstatus))
            {
                if (fiftyFiveWithdrawalRequestedstatus == A55WithdrawalStatus.RJ.ToString())
                {
                    finalStatus = CodeRequestStatus.code3;
                }
                else if (fiftyFiveWithdrawalRequestedstatus == A55WithdrawalStatus.C.ToString())
                {
                    finalStatus = CodeRequestStatus.code10;
                }
                else
                {
                    finalStatus = CodeRequestStatus.code11;
                }
            }

            return finalStatus;
        }

        public static int GetRAWithdrawalStatus(string withdrawalStatus)
        {
            int finalStatus = 0;

            if (withdrawalStatus == A55WithdrawalStatus.SC.ToString())
            {
                finalStatus = (int)CodeRequestStatus.code11;
            }
            else if (withdrawalStatus == A55WithdrawalStatus.PV.ToString() || withdrawalStatus == A55WithdrawalStatus.PQP.ToString())
            {
                finalStatus = (int)CodeRequestStatus.code83;
            }
            else if (withdrawalStatus == A55WithdrawalStatus.RJ.ToString())
            {
                finalStatus = (int)CodeRequestStatus.code3;
            }
            else
            {
                finalStatus = (int)CodeRequestStatus.code83;
            }
            return finalStatus;
        }
    }
}
