using System;
using System.Collections.Generic;

namespace CountingKs.Data.Entities
{
  public class Diary
  {
    public Diary()
    {
      Entries = new List<DiaryEntry>();
    }

    public int Id { get; set; }
    public DateTime CurrentDate { get; set; }
    public ICollection<DiaryEntry> Entries { get; set; }
    public string UserName { get; set; }
  }
}
