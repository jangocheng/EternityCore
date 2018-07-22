using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace OrchardCore.Data
{
    public interface IEntityTypeConfigurationRegister
    {
        void Register(ModelBuilder builder);
    }

    public abstract class EntityTypeConfigurationBase<TEntityType> : IEntityTypeConfiguration<TEntityType>, IEntityTypeConfigurationRegister where TEntityType : class
    {
        public abstract void Configure(EntityTypeBuilder<TEntityType> builder);

        public virtual void Register(ModelBuilder builder)
        {
            builder.ApplyConfiguration(this);

        }
    }
}
