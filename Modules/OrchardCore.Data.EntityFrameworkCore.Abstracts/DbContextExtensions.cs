using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using OrchardCore.Navigation;
using OrchardCore.Settings;

namespace OrchardCore.Data
{
    public static class DbContextExtensions
    {
        public static PagedQuery<TSource> Paged<TSource>(this IOrderedQueryable<TSource> source, Pager pager, int count) where TSource : class
        {
            return new PagedQuery<TSource>(source.Paging(pager.Page, pager.PageSize, count), pager, count);
        }
        public static IQueryable<TSource> Paging<TSource>(this IOrderedQueryable<TSource> source, int page, int pageSize, int count)
        {
            return count > 0 ? source.Skip((page - 1) * pageSize).Take(pageSize) : source;
        }

    }
}
