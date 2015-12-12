using System;
using System.Runtime.Serialization;

namespace WCFIntegration.DataContracts
{
    [DataContract]
    public class ExpenseReport
    {
        [DataMember]
        public Person Employee { get; set; }
        [DataMember]
        public double Amount { get; set; }
        [DataMember]
        public string Client { get; set; }
        [DataMember]
        public DateTime StartDate { get; set; }
        [DataMember]
        public DateTime EndDate { get; set; }
        [DataMember]
        public string City { get; set; }

    }
}
