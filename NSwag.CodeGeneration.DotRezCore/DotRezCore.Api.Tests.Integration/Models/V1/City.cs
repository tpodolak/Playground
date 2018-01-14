namespace DotRezCore.Api.Tests.Integration.Models.V1
{
    public partial class City 
    {
        [Newtonsoft.Json.JsonProperty(PropertyName = "name")]
        public string Name { get; set; } 
    }
}