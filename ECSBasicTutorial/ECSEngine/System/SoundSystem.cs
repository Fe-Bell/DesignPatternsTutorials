using ECSEngine.Component;
using ECSEngine.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECSEngine.System
{
    /// <summary>
    /// Represents a custom sound system.
    /// </summary>
    public class SoundSystem : ISystem
    {
        private IDictionary<string /*Sound path*/, Sound.PlaybackState_e /*Last sound state*/> soundStates = null;
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
        /// The active application scene.
        /// </summary>
        public Scene CurrentScene 
        { 
            get => currentScene; 
            set
            {
                if(currentScene != value)
                {
                    currentScene = value;
                    OnSceneChanged?.Invoke(this, null);
                }
            }
        }
        /// <summary>
        /// Flag for enabling or disabling the system.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Event fired once a scene changes.
        /// </summary>
        public event EventHandler OnSceneChanged;

        public SoundSystem()
        {
            soundStates = new Dictionary<string, Sound.PlaybackState_e>();
            OnSceneChanged += SoundSystem_OnSceneChanged;
        }

        /// <summary>
        /// SceneChanged event handler.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SoundSystem_OnSceneChanged(object sender, EventArgs e)
        {
            //If needed, changes the state of the system when the scene changes

            //Stops monitoring sounds from previous scene
            soundStates.Clear();
        }

        /// <summary>
        /// Processes objects of type 'Sound'.
        /// </summary>
        /// <param name="value"></param>
        private void Process(Sound value)
        {
            //Checks if sound is being monitored by the system
            if(!soundStates.TryGetValue(value.Path, out Sound.PlaybackState_e lastState))
            {
                lastState = Sound.PlaybackState_e.Stopped;
                soundStates.Add(value.Path, lastState);
            }

            if(value.IsEnabled)
            {
                //If the state has changed from the previous frame
                if (value.PlaybackState != lastState)
                {
                    //Change state of sound
                    soundStates[value.Path] = value.PlaybackState;
                    //Here we would call the appropriate methods for playing, stopping and pausing.
                    Console.WriteLine(string.Format("\"{0}\" sound \"{1}\" changed to {2}!", value.Owner.Name, value.Path, value.PlaybackState.ToString()));
                }
            }
            else
            {
                //Force the state to stopped when disabled
                if (value.PlaybackState != Sound.PlaybackState_e.Stopped)
                {
                    value.PlaybackState = Sound.PlaybackState_e.Stopped;
                    soundStates[value.Path] = Sound.PlaybackState_e.Stopped;
                    Console.WriteLine(string.Format("\"{0}\" sound \"{1}\" has been disabled!", value.Owner.Name, value.Path));
                }

                //Do nothing.
            }
        }

        /// <summary>
        /// Processes the current scene.
        /// </summary>
        private void Process()
        {
            //Processes all the sounds that belong to the scene
            IEnumerable<Sound> components = CurrentScene.GetComponents<Sound>();         
            foreach(var component in components)
            {
                Process(component);
            }

            //Processes all sounds per entity
            foreach(var entity in CurrentScene.Entities)
            {
                components = entity.GetComponents<Sound>();
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
            if (IsEnabled && CurrentScene != null)
            {
                Process();
            }
        }
    }
}
