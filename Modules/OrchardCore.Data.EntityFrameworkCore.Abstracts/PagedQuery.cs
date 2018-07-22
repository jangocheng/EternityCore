using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
using OrchardCore.Navigation;

namespace OrchardCore.Data
{
    public class PagedQuery<T> : IQueryable<T> where T : class
    {
        private readonly IQueryable<T> _source;
        public Pager Pager { get; internal set; }

        public int Count { get; internal set; }

        internal PagedQuery(IQueryable<T> source, Pager pager, int count)
        {
            _source = source.AsNoTracking();
            Pager = pager;
            Count = count;
        }

        public PagedModel<T> ToModel()
        {
            return ToModel(_source);

        }

        public PagedModel<TResult> ToModel<TResult>(IQueryable<TResult> source) where TResult : class
        {
            return new PagedModel<TResult>()
            {
                TotalItemCount = Count,
                Items = source.ToList(),
                Pager = Pager

            };
        }

        public PagedModel ToDynamicModel()
        {
            return new PagedModel()
            {
                TotalItemCount = Count,
                Items = _source.ToList(),
                Pager = Pager

            };
        }



        public PagedModel<TResult> ToModel<TResult>(Expression<Func<T, TResult>> selector) where TResult : class
        {

            return ToModel(_source.Select(selector));
        }


        public IEnumerator<T> GetEnumerator()
        {
            return _source.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return _source.GetEnumerator();
        }
        public Type ElementType => _source.ElementType;

        public Expression Expression => _source.Expression;

        public IQueryProvider Provider => _source.Provider;
    }
}
