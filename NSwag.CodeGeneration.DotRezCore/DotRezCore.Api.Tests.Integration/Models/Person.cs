namespace DotRezCore.Api.Tests.Integration.Models
{
    public partial class Person 
    {
        [Newtonsoft.Json.JsonProperty(PropertyName = "id")]
        public int? Id { get; set; } 
        [Newtonsoft.Json.JsonProperty(PropertyName = "firstName")]
        public string FirstName { get; set; } 
        [Newtonsoft.Json.JsonProperty(PropertyName = "lastName")]
        public string LastName { get; set; } 
        [Newtonsoft.Json.JsonProperty(PropertyName = "email")]
        public string Email { get; set; } 
        [Newtonsoft.Json.JsonProperty(PropertyName = "city")]
        public DotRezCore.Api.Tests.Integration.Models.City City { get; set; } 
        [Newtonsoft.Json.JsonProperty(PropertyName = "cities")]
        public System.Collections.Generic.List<DotRezCore.Api.Tests.Integration.Models.City> Cities { get; set; } 
    }
}