using System.Collections.Generic;
using CountingCalories.Data.Entities;
using CountingCalories.Models;

namespace CountingCalories.Infrastructure.Mappers
{
    public interface IModelFactory
    {
        FoodModel Create(Food food);
        IEnumerable<FoodModel> Create(IEnumerable<Food> foods);
        MeasureModel Create(Measure measure);
        MeasureV2Model CreateMeasure(Measure measure);
        IEnumerable<MeasureV2Model> CreateMeasure(IEnumerable<Measure> measures);
        IEnumerable<MeasureModel> Create(IEnumerable<Measure> measures);
        DiaryModel Create(Diary diary);
        IEnumerable<DiaryModel> Create(IEnumerable<Diary> measures);
        IEnumerable<DiaryEntryModel> Create(IEnumerable<DiaryEntry> diaryEntries);
        DiaryEntryModel Create(DiaryEntry diaryEntry);
        DiaryEntry Parse(DiaryEntryModel diaryEntry);
        DiarySummaryModel CreateSummary(Diary diary);
        AuthTokenModel Create(AuthToken authToken);
        LinkModel CreateLink(string href, string rel, string method = "GET", bool isTempleted = false);
    }
}