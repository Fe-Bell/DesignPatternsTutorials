using ECSEngine.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECSEngine.Component
{
    public class Rigidbody : IComponent
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

        public float Mass { get; set; }
        public bool IsFixed { get; set; }

        public Rigidbody()
        {

        }
    }
}
