namespace DotRezCore.Api.Tests.Integration.Models.V1
{
    public partial class GetOrderRequest 
    {
        [Newtonsoft.Json.JsonProperty(PropertyName = "id")]
        public string Id { get; set; } 
        [Newtonsoft.Json.JsonProperty(PropertyName = "type")]
        public string Type { get; set; } 
    }
}