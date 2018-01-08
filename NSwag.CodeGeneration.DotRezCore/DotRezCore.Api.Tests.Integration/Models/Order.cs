﻿namespace DotRezCore.Api.Tests.Integration.Models
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
        public OrderGender? Gender { get; set; } 
    }
}