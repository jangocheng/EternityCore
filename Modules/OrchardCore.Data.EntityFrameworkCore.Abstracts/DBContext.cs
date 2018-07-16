using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OrchardCore.Environment.Shell.Builders.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore.Infrastructure;


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
        protected DbContext(DbContextOptions<TContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var interfaceType = typeof(T);

            var shellBlueprint = this.GetService<ShellBlueprint>();
            var entities = shellBlueprint.Dependencies.Keys.Where(w => w.IsClass && !w.IsAbstract && interfaceType.IsAssignableFrom(w));

            foreach (var entity in entities)
            {
                if (modelBuilder.Model.FindEntityType(entity) != null)
                    continue;
                modelBuilder.Entity(entity);

            }
            base.OnModelCreating(modelBuilder);
        }

    }
}
