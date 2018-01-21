using System.ComponentModel.DataAnnotations;

namespace DotRezCore.Api.Models.V1
{
    public class GetOrderRequest
    {
        public string Id { get; set; }

        public string Type { get; set; }
        
        [Required]
        public Gender Gender { get; set; }
    }
}