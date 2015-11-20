using System;
using System.Collections.Generic;

namespace CountingCalories.Models
{
    public class DiaryModel : ModelBase
    {
        public int Id { get; set; }
        public DateTime CurrentDate { get; set; }
        public ICollection<DiaryEntryModel> Entries { get; set; }
        public string UserName { get; set; }
        public ICollection<LinkModel> Links { get; set; } 
    }
}