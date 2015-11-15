using System.Collections.Generic;

namespace CountingKs.Models
{
    public class PageResult<T>
    {
        public int TotalPages { get; private set; }
        public int TotalCount { get; private set; }
        public string PreviousPageUrl { get; set; }
        public string NextPageUrl { get; set; }
        public IEnumerable<T> Results { get; private set; }

        public PageResult(IEnumerable<T> results, int totalPages, int totalCount)
        {
            Results = results;
            TotalCount = totalCount;
            TotalPages = totalPages;
        }
    }
}