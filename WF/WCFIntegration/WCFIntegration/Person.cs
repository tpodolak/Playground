using System.Runtime.Serialization;

namespace WCFIntegration
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
