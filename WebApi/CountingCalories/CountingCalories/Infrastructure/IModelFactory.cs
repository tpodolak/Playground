using System.Collections.Generic;
using CountingCalories.Data.Entities;
using CountingCalories.Models;

namespace CountingCalories.Infrastructure
{
    public interface IModelFactory
    {
        FoodModel Create(Food food);
        IEnumerable<FoodModel> Create(IEnumerable<Food> foods);
        MeasureModel Create(Measure measure);
        IEnumerable<MeasureModel> Create(IEnumerable<Measure> measures);
        DiaryModel Create(Diary diary);
        IEnumerable<DiaryModel> Create(IEnumerable<Diary> measures);
        IEnumerable<DiaryEntryModel> Create(IEnumerable<DiaryEntry> diaryEntries);
        DiaryEntryModel Create(DiaryEntry diaryEntry);
        DiaryEntry Parse(DiaryEntryModel diaryEntry);
        DiarySummaryModel CreateSummary(Diary diary);
        AuthTokenModel Create(AuthToken authToken);
    }
}