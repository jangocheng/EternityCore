using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using OrchardCore.Navigation;
using OrchardCore.Settings;
using System;
using System.Linq;

namespace OrchardCore.Data
{
    public class DBContext : DbContext<DBContext, IEntity>
    {
        public DBContext(DbContextOptions<DBContext> options) : base(options)
        {
        }
    }


    public abstract class DbContext<TContext, T> : DbContext where T : IEntity where TContext : DbContext
    {
        protected DbContext(DbContextOptions<TContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var entityTypeManager = this.GetService<EntityTypeManager>();

            entityTypeManager.GetEntityTypes<T>().AsParallel().ForAll(type =>
            {
                modelBuilder.Model.GetOrAddEntityType(type);
            });

            entityTypeManager.Registers.AsParallel().ForAll(config => { config.Register(modelBuilder); });



            base.OnModelCreating(modelBuilder);
        }

        public PagedQuery<TSource> Paged<TSource>(Func<DbContext, IOrderedQueryable<TSource>> func, PagerParameters pagerParameters, int count) where TSource : class
        {
            var pager = new Pager(pagerParameters, this.GetService<ISite>().MaxPageSize);

            return func(this).Paged(pager, count);
        }
        public PagedQuery<TSource> Paged<TSource>(Func<DbContext, int> countQuery, Func<DbContext, IOrderedQueryable<TSource>> func, PagerParameters pagerParameters) where TSource : class
        {
            return Paged(func, pagerParameters, countQuery(this));
        }
    }
}

