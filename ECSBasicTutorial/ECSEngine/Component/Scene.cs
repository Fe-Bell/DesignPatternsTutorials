using ECSEngine.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECSEngine.Component
{
    public class Scene : Entity
    {
        /// <summary>
        /// A collection of entities.
        /// </summary>
        public List<Entity> Entities { get; private set; }

        public Scene() : base()
        {
            Entities = new List<Entity>();
        }

        /// <summary>
        /// Returns the first entity matching the specified name and type.
        /// </summary>
        /// <typeparam name="T">The type of the entity to be queried.</typeparam>
        /// <param name="value">The name for the entity lookup.</param>
        /// <returns>The match.</returns>
        public T FindEntityByName<T>(string value) where T : Entity
        {
            //Performs a linear search util a match is found.
            for (int i = 0; i < Entities.Count; i++)
            {
                Entity entity = Entities[i];
                if (entity is T && entity.Name == value)
                {
                    return (T)entity;
                }
            }

            return null;
        }

        /// <summary>
        /// Returns the first entity matching the specified id and type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="name">An entity</param>
        /// <returns></returns>
        public T FindEntityByID<T>(uint value) where T : Entity
        {
            //Performs a linear search util a match is found.
            for (int i = 0; i < Entities.Count; i++)
            {
                Entity entity = Entities[i];
                if (entity is T && entity.ID == value)
                {
                    return (T)entity;
                }
            }

            return null;
        }

        /// <summary>
        /// Adds a new component to the current instance.
        /// </summary>
        /// <typeparam name="T">The type of the entity.</typeparam>
        /// <param name="value">The entity to be added.</param>
        public void AddEntity<T>(T value) where T : Entity
        {
            value.Owner = this;
            Entities.Add(value);
        }

        /// <summary>
        /// Removes an entity from the current instance.
        /// </summary>
        /// <typeparam name="T">The type of the entity to be removed.</typeparam>
        /// <param name="value">The entity to be removed.</param>
        /// <returns>If the entity was removed or not.</returns>
        public bool RemoveEntity<T>(T value) where T : Entity
        {
            return Entities.Remove(value);
        }
    }
}
