//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     $DotRezCore.Api.Tests.Integration.ModelGenerator
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------
namespace DotRezCore.Api.Tests.Integration.Models.V1
{
    public partial class Order 
    {
        [Newtonsoft.Json.JsonProperty(PropertyName = "id")]
        public int? Id { get; set; } 
        [Newtonsoft.Json.JsonProperty(PropertyName = "createdDate")]
        public System.DateTime? CreatedDate { get; set; } 
        [Newtonsoft.Json.JsonProperty(PropertyName = "effectiveDate")]
        public System.DateTime? EffectiveDate { get; set; } 
        [Newtonsoft.Json.JsonProperty(PropertyName = "customer")]
        public string Customer { get; set; } 
        [Newtonsoft.Json.JsonProperty(PropertyName = "gender")]
        public DotRezCore.Api.Tests.Integration.Models.V1.Gender Gender { get; set; } 
    }
}
