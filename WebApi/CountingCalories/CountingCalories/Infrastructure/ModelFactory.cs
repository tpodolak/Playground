using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http.Routing;
using CountingCalories.Data;
using CountingCalories.Data.Entities;
using CountingCalories.Models;

namespace CountingCalories.Infrastructure
{
    public class ModelFactory : IModelFactory
    {
        private readonly ICountingCaloriesRepository repo;
        private readonly UrlHelper urlHelper;

        public ModelFactory(ICountingCaloriesRepository repo, UrlHelper urlHelper)
        {
            this.repo = repo;
            this.urlHelper = urlHelper;
        }

        public FoodModel Create(Food food)
        {
            var result = new FoodModel
            {
                Id = food.Id,
                Url = urlHelper.Link("Foods", new { foodid = food.Id }),
                Description = food.Description,
                Measures = food.Measures.Select(Create).ToList()
            };
            return result;
        }

        public IEnumerable<FoodModel> Create(IEnumerable<Food> foods)
        {
            if (foods == null)
                return Enumerable.Empty<FoodModel>();

            return foods.Select(this.Create);
        }

        public MeasureModel Create(Measure measure)
        {
            var result = new MeasureModel
            {
                Id = measure.Id,
                Url = urlHelper.Link("Measures", new { id = measure.Id, foodid = measure.Food.Id }),
                Calories = measure.Calories,
                Description = measure.Description,
            };
            return result;
        }

        public IEnumerable<MeasureModel> Create(IEnumerable<Measure> measures)
        {
            if (measures == null)
                return Enumerable.Empty<MeasureModel>();
            return measures.Select(this.Create);
        }

        public DiaryModel Create(Diary diary)
        {
            return new DiaryModel
            {
                Id = diary.Id,
                Url = urlHelper.Link("Diaries", new { diaryid = diary.CurrentDate.ToString("yyyy-MM-dd") }),
                CurrentDate = diary.CurrentDate,
                UserName = diary.UserName,
                Entries = diary.Entries.Select(Create).ToList()
            };
        }

        public IEnumerable<DiaryModel> Create(IEnumerable<Diary> measures)
        {
            if (measures == null)
                return Enumerable.Empty<DiaryModel>();
            return measures.Select(this.Create);
        }

        public DiaryEntryModel Create(DiaryEntry diaryEntry)
        {
            return new DiaryEntryModel
            {
                Url = urlHelper.Link("DiaryEntries", new { diaryid = diaryEntry.Diary.CurrentDate.ToString("yyyy-MM-dd"), id = diaryEntry.Id }),
                Quantity = diaryEntry.Quantity,
                FoodDescription = diaryEntry.FoodItem.Description,
                MeasureDescription = diaryEntry.Measure.Description,
                MeasureUrl = urlHelper.Link("Measures", new { foodid = diaryEntry.FoodItem.Id, id = diaryEntry.Measure.Id })
            };
        }

        public DiaryEntry Parse(DiaryEntryModel diaryEntry)
        {
            try
            {
                var entry = new DiaryEntry
                {
                    Quantity = diaryEntry.Quantity,
                };

                if (!string.IsNullOrWhiteSpace(diaryEntry.MeasureUrl))
                {
                    var uri = new Uri(diaryEntry.MeasureUrl);
                    var measureId = int.Parse(uri.Segments.Last());
                    var measure = repo.GetMeasure(measureId);
                    entry.Measure = measure;
                    entry.FoodItem = measure.Food;
                }
                return entry;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DiarySummaryModel CreateSummary(Diary diary)
        {
            return new DiarySummaryModel
            {
                DiaryDate = diary.CurrentDate,
                TotalCalories = Math.Round(diary.Entries.Sum(val => val.Measure.Calories*val.Quantity))
            };
        }

        public IEnumerable<DiaryEntryModel> Create(IEnumerable<DiaryEntry> diaryEntries)
        {
            if (diaryEntries == null)
                return Enumerable.Empty<DiaryEntryModel>();
            return diaryEntries.Select(Create);
        }
    }
}