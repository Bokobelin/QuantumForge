using System.Collections.Generic;

namespace QuantumForge.Engine
{
    public static class EntityManager
    {
        private static List<Entity> entities = new List<Entity>();

        public static IReadOnlyList<Entity> Entities => entities;

        public static void AddEntity(Entity entity)
        {
            entities.Add(entity);
        }

        public static void RemoveEntity(Entity entity)
        {
            entities.Remove(entity);
        }

        public static void ClearEntities()
        {
            entities.Clear();
        }
    }
}
