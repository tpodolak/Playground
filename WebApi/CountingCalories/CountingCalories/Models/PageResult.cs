using System.Collections.Generic;

namespace CountingCalories.Models
{
    public class PageResult<T> : ModelBase
    {
        public int TotalPages { get; private set; }
        public int TotalCount { get; private set; }
        public IEnumerable<T> Results { get; private set; }

        public PageResult(IEnumerable<T> results, int totalPages, int totalCount)
        {
            Links = new List<LinkModel>();
            Results = results;
            TotalCount = totalCount;
            TotalPages = totalPages;
        }
    }
}