using System.Runtime.Serialization;

namespace WCFIntegration
{
    [DataContract]
    public class ManagerResponse
    {
        [DataMember]
        public int ItemIdentifier { get; set; }

        [DataMember]
        public bool Approved { get; set; }
    }
}
