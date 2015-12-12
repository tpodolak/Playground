using System.Runtime.Serialization;

namespace WCFIntegration.DataContracts
{
    [DataContract]
    public class ExpenseReportConfirmation : ExpenseReport
    {
        [DataMember]
        public int ReportID { get; set; }

        [DataMember]
        public ReportStatusType Status { get; set; }
    }
}
