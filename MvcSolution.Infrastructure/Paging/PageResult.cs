using System.Collections.Generic;
using System.Linq;

namespace MvcSolution
{
    public class PageResult
    {
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public int TotalPages { get; set; }

        public bool HasPreviousPage => (PageIndex > 0);

        public bool HasNextPage => (PageIndex + 1 < TotalPages);

        public void UpdateFrom(PageResult result)
        {
            this.PageIndex = result.PageIndex;
            this.PageSize = result.PageSize;
            this.TotalCount = result.TotalCount;
            this.TotalPages = result.TotalPages;
        }
    }

    public class PageResult<T> : PageResult
    {
        public List<T> Value { get; set; }
        
        public PageResult()
        {
            this.Value = new List<T>();
        }

        public PageResult(IQueryable<T> source, int pageIndex, int pageSize)
            : this()
        {
            if (pageSize == 0)
            {
                return;
            }
            int total = source.Count();
            this.TotalCount = total;
            this.TotalPages = total / pageSize;

            if (total % pageSize > 0)
                TotalPages++;

            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.Value.AddRange(source.Skip(pageIndex * pageSize).Take(pageSize).ToList());
        }

        public PageResult(IList<T> source, int pageIndex, int pageSize)
            : this()
        {
            if (pageSize == 0)
            {
                return;
            }
            TotalCount = source.Count;
            TotalPages = TotalCount / pageSize;

            if (TotalCount % pageSize > 0)
                TotalPages++;

            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.Value.AddRange(source.Skip(pageIndex * pageSize).Take(pageSize).ToList());
        }

        public PageResult(IEnumerable<T> source, int pageIndex, int pageSize, int totalCount)
            : this()
        {
            if (pageSize == 0)
            {
                return;
            }
            TotalCount = totalCount;
            TotalPages = TotalCount / pageSize;

            if (TotalCount % pageSize > 0)
                TotalPages++;

            this.PageSize = pageSize;
            this.PageIndex = pageIndex;
            this.Value.AddRange(source.Skip(pageIndex * pageSize).Take(pageSize));
        }
    }
}

namespace MvcSolution
{
    public static class PageResultExtension
    {
        public static PageResult<T> ToPageResult<T>(this IEnumerable<T> source, int pageIndex, int pageSize)
        {
            return new PageResult<T>(source, pageIndex, pageSize, source.Count());
        }

        public static PageResult<T> ToPageResult<T>(this IQueryable<T> linq, int pageIndex, int pageSize)
        {
            return new PageResult<T>(linq, pageIndex, pageSize);
        }

        public static PageResult<T> ToPageResult<T>(this IQueryable<T> query, PageRequest request)
        {
            return new PageResult<T>(query.OrderBy(request.Sort, request.SortDirection), request.PageIndex, request.PageSize);
        }

        public static PageResult<T> ToPageResult<T>(this IList<T> source, int pageIndex, int pageSize)
        {
            return new PageResult<T>(source, pageIndex, pageSize);
        }
    }

}