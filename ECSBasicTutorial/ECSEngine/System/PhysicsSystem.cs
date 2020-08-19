using ECSEngine.Component;
using ECSEngine.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECSEngine.System
{
    /// <summary>
    /// Represents a custom physics system.
    /// </summary>
    public class PhysicsSystem : ISystem
    {
        private Scene currentScene = null;

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
        /// The active application scene.
        /// </summary>
        public Scene CurrentScene
        {
            get => currentScene;
            set
            {
                if (currentScene != value)
                {
                    currentScene = value;
                    OnSceneChanged?.Invoke(this, null);
                }
            }
        }
        /// <summary>
        /// Event fired once a scene changes.
        /// </summary>
        public event EventHandler OnSceneChanged;

        public PhysicsSystem()
        {
            OnSceneChanged += PhysicsSystem_OnSceneChanged;
        }

        /// <summary>
        /// SceneChanged event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PhysicsSystem_OnSceneChanged(object sender, EventArgs e)
        {
            //If needed, changes the state of the system when the scene changes
        }

        /// <summary>
        /// Processes objects of type 'Rigidbody'.
        /// </summary>
        /// <param name="value"></param>
        private void Process(Rigidbody value)
        {
            //Here we could add all sorts of physics related calculations, such as the integration for gravity.

            //Prints the mass of the rigidbody
            Console.WriteLine(string.Format("\"{0}\" rigidbody \"{1}\" Mass is : {2}", value.Owner.Name, value.Name, value.Mass));

            //Integrate if object can be moved
            if (!value.IsFixed)
            {
                //Calculate velocity, acceleration and new position in space.
            }
        }

        /// <summary>
        /// Processes the current scene.
        /// </summary>
        private void Process()
        {
            //Processes all the rigidbodies that belong to the scene
            IEnumerable<Rigidbody> components = CurrentScene.GetComponents<Rigidbody>();
            foreach (var component in components)
            {
                if (component.IsEnabled)
                {
                    Process(component);
                }
            }

            //Processes all rigidbodies per entity
            foreach (var entity in CurrentScene.Entities)
            {
                components = entity.GetComponents<Rigidbody>();
                foreach (var component in components)
                {
                    if (component.IsEnabled)
                    {
                        Process(component);
                    }
                }
            }
        }

        /// <summary>
        /// Updates the system.
        /// </summary>
        /// <param name="dt"></param>
        public void Update(float dt)
        {
            if(IsEnabled && CurrentScene != null)
            {
                Process();
            }
        }
    }
}
