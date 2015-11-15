using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using CountingKs.Data.Entities;
using CountingKs.Models;

namespace CountingKs.Infrastructure
{
    public static class MappingExtensions
    {
        static MappingExtensions()
        {
            Mapper.CreateMap<Food, FoodModel>();
            Mapper.CreateMap<FoodModel, Food>();
            Mapper.CreateMap<Measure, MeasureModel>();
            Mapper.CreateMap<MeasureModel, Measure>();
        }

        public static TDest Map<TSource, TDest>(this TSource source)
        {
            return Mapper.Map<TSource, TDest>(source);
        }

        public static IEnumerable<TDest> Map<TSource, TDest>(this IEnumerable<TSource> sources)
        {
            if (sources == null)
                return Enumerable.Empty<TDest>();

            return sources.Select(Map<TSource, TDest>);
        }
    }
}