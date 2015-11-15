using System;
using System.Collections.Generic;

namespace CountingKs.Models
{
    public class DiaryModel : ModelBase
    {
        public int Id { get; set; }
        public DateTime CurrentDate { get; set; }
        public ICollection<DiaryEntryModel> Entries { get; set; }
        public string UserName { get; set; }
    }
}