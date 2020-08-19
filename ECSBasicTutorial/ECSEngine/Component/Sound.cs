using ECSEngine.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECSEngine.Component
{
    public class Sound : IComponent
    {
        /// <summary>
        /// Enum to represent the playback state of the current sound.
        /// </summary>
        public enum PlaybackState_e
        {
            Stopped,
            Paused,
            Playing
        }

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
        /// Path to the track.
        /// </summary>
        public string Path { get; set; }
        /// <summary>
        /// The playback state of the sound file.
        /// </summary>
        public PlaybackState_e PlaybackState { get; set; }

        public Sound()
        {
            IsEnabled = false;
            PlaybackState = PlaybackState_e.Stopped;
        }
    }
}
