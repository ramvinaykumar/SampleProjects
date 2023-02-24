using System.ComponentModel;

namespace RandomActivity.API.Models
{
    public enum BankNameEnum
    {
        [Description("DBS")]
        DBS,
        [Description("POSB")]
        POSB,
        [Description("OCBC")]
        OCBC,
        [Description("UOB")]
        UOB
    }
}
