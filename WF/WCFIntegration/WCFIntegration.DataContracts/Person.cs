using System.Runtime.Serialization;

namespace WCFIntegration.DataContracts
{
    [DataContract]
    public class Person
    {
        [DataMember]
        public string Email { get; set; }
        [DataMember]
        public string Name { get; set; }    
    }
}
