using ECSEngine.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECSEngine.Component
{
    public class Entity : IComponent
    {
        /// <summary>
        /// The component owner.
        /// </summary>
        public IComponent Owner { get; set; }
        /// <summary>
        /// The name of an object.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// The ID of an object.
        /// </summary>
        public uint ID { get; set; }
        /// <summary>
        /// Checks if the current object is enabled or not.
        /// </summary>
        public bool IsEnabled { get; set; }
        /// <summary>
        /// A container of components.
        /// </summary>
        public List<IComponent> Components { get; private set; }

        public Entity()
        {
            ID = 0u;
            Name = "";
            IsEnabled = true;           
            Components = new List<IComponent>();
        }

        /// <summary>
        /// Checks if the current entity has a component of the specified type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public bool HasComponent<T>() where T : IComponent
        {
            //Performs a linear search util a match is found.
            for (int i = 0; i < Components.Count; i++)
            {
                IComponent component = Components[i];
                if (component is T)
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns the first component of the specified type T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetComponent<T>() where T : IComponent
        {
            //Performs a linear search util a match is found.
            for (int i = 0; i < Components.Count; i++)
            {
                IComponent component = Components[i];
                if (component is T)
                {
                    return (T)component;
                }
            }

            return default;
        }

        /// <summary>
        /// Returns the all components of the specified type T.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public IEnumerable<T> GetComponents<T>() where T : IComponent
        {
            //Performs a linear search util a match is found.
            List<T> components = new List<T>();
            for (int i = 0; i < Components.Count; i++)
            {
                IComponent component = Components[i];
                if (component is T)
                {
                    components.Add((T)component);
                }
            }

            return components;
        }

        /// <summary>
        /// Adds a new component to the current instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component"></param>
        public void AddComponent<T>(T component) where T : IComponent
        {
            component.Owner = this;
            Components.Add(component);
        }

        /// <summary>
        /// Removes a component from the current instance.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="component"></param>
        /// <returns></returns>
        public bool RemoveComponent<T>(T component) where T : IComponent
        {
            return Components.Remove(component);
        }

        /// <summary>
        /// Returns the first component matching the specified name and type.
        /// </summary>
        /// <typeparam name="T">The type of the component to be queried.</typeparam>
        /// <param name="value">The name for the component lookup.</param>
        /// <returns>The match.</returns>
        public T FindComponentByName<T>(string value) where T : IComponent
        {
            //Performs a linear search util a match is found.
            for (int i = 0; i < Components.Count; i++)
            {
                IComponent component = Components[i];
                if (component is T && component.Name == value)
                {
                    return (T)component;
                }
            }

            return default;
        }

        /// <summary>
        /// Returns the first component matching the specified id and type.
        /// </summary>
        /// <typeparam name="T">The type of the component.</typeparam>
        /// <param name="name">An entity</param>
        /// <returns>The match.</returns>
        public T FindComponentByID<T>(uint value) where T : Entity
        {
            //Performs a linear search util a match is found.
            for (int i = 0; i < Components.Count; i++)
            {
                IComponent component = Components[i];
                if (component is T && component.ID == value)
                {
                    return (T)component;
                }
            }

            return default;
        }
    }
}
