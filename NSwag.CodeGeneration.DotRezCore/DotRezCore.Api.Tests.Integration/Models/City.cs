namespace DotRezCore.Api.Tests.Integration.Models
{
    public partial class City 
    {
        [Newtonsoft.Json.JsonProperty(PropertyName = "name")]
        public string Name { get; set; } 
    }
}