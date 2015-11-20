namespace CountingCalories.Models
{
    public class LinkModel
    {
        public string Href { get; set; } 
        public string Rel { get; set; }
        public string Method { get; set; }
        public bool IsTempleted { get; set; }
    }
}