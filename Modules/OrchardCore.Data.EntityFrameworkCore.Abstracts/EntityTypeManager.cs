using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OrchardCore.Environment.Shell.Builders.Models;

namespace OrchardCore.Data
{
    public class EntityTypeManager
    {
        private readonly ShellBlueprint _shellBlueprint;
        public IEnumerable<IEntityTypeConfigurationRegister> Registers { get; }

        public EntityTypeManager(IEnumerable<IEntityTypeConfigurationRegister> registers, ShellBlueprint shellBlueprint)
        {
            _shellBlueprint = shellBlueprint;
            Registers = registers;
        }

        public Type[] GetEntityTypes<T>()
        {
            var interfaceType = typeof(T);
            return _shellBlueprint.Dependencies.Keys.Where(w => w.IsClass && !w.IsAbstract && interfaceType.IsAssignableFrom(w)).ToArray();
        }
    }
}
