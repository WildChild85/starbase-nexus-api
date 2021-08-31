using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace starbase_nexus_api.Models.Api
{

    public class PagedList<T> : List<T>, IPagedList
    {
        public int Page { get; set; }

        public int PageSize { get; set; }

        public int TotalPages { get; set; }

        public int TotalCount { get; set; }

        public PagedList()
        {
            throw new NotImplementedException("Please use the parameterized constructor.");
        }

        public PagedList(List<T> items, int totalCount, int page, int pageSize)
        {
            TotalCount = totalCount;
            TotalPages = pageSize < 1 ? 1 : (int)Math.Ceiling(totalCount / (double)pageSize);
            Page = page;
            PageSize = pageSize;
            AddRange(items);
        }

        public static async Task<PagedList<T>> Create(IQueryable<T> source, int page, int pageSize)
        {
            int count = source.Count();
            List<T> items;
            if(pageSize > 0)
                items = await source.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
            else
                items = await source.Skip((page - 1) * pageSize).ToListAsync();

            return new PagedList<T>(items, count, page, pageSize);
        }
    }
}
