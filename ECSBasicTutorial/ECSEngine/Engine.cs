using ECSEngine.Component;
using ECSEngine.Interface;
using System;
using System.Collections.Generic;

namespace ECSEngine
{
    /// <summary>
    /// COntainer for all application sub-systems.
    /// </summary>
    public class Engine
    {
        private IDictionary<string /*System Name*/, ISystem /*The system object*/> systems = null;

        public Engine()
        {
            systems = new Dictionary<string, ISystem>();
        }

        /// <summary>
        /// Includes a new system in the Engine.
        /// </summary>
        /// <param name="system"></param>
        public void AddSystem(ISystem system)
        {
            systems.Add(system.Name, system);
        }

        /// <summary>
        /// Removes a system from the engine.
        /// </summary>
        /// <param name="system"></param>
        /// <returns></returns>
        public bool RemoveSystem(ISystem system)
        {
            return systems.Remove(system.Name);
        }

        /// <summary>
        /// Changes the active scene in the engine.
        /// </summary>
        /// <param name="scene"></param>
        public void SetActiveScene(Scene scene)
        {
            foreach (var pair in systems)
            {
                pair.Value.CurrentScene = scene;
            }
        }

        /// <summary>
        /// Gets a system from the Engine.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ISystem GetSystem<T>() where T : ISystem
        {
            foreach(var pair in systems)
            {
                if(pair.Value is T)
                {
                    return pair.Value;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets a system from the Engine.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public ISystem GetSystem<T>(string name) where T : ISystem
        {
            if(systems.TryGetValue(name, out ISystem system))
            {
                return system;
            }

            return null;
        }

        /// <summary>
        /// Updates all systems.
        /// </summary>
        /// <param name="dt"></param>
        public void UpdateSystems(float dt)
        {
            foreach(var pair in systems)
            {
                pair.Value.Update(dt);
            }
        }
    }
}
