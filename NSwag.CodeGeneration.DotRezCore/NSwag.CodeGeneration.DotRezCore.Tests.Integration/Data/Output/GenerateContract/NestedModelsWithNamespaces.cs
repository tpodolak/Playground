namespace NSwag.CodeGeneration.DotRezCore.Tests.Integration.Data.Output.GenerateContract
{
    public partial class Order 
    {
        public int? Id { get; set; } 
        public System.DateTime? CreatedDate { get; set; } 
        public System.DateTime? EffectiveDate { get; set; } 
        public string Customer { get; set; } 
        public OrderGender? Gender { get; set; } 
    }

    public partial class Person 
    {
        public int? Id { get; set; } 
        public string FirstName { get; set; } 
        public string LastName { get; set; } 
        public string Email { get; set; } 
        public City City { get; set; } 
        public System.Collections.Generic.List<City> Cities { get; set; } 
    }

    public partial class City 
    {
        public string Name { get; set; } 
    }

    public enum OrderGender
    {
        [System.Runtime.Serialization.EnumMember(Value = "Male")]
        Male = 0,
        [System.Runtime.Serialization.EnumMember(Value = "Female")]
        Female = 1,
    }
}