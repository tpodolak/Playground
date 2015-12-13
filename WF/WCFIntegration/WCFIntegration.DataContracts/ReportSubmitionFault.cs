using System.Runtime.Serialization;

namespace WCFIntegration.DataContracts
{
    [DataContract]
    public class ReportSubmissionFault
    {
        [DataMember]
        public string Field { get; set; }

        [DataMember]
        public string Message { get; set; }
    }
}