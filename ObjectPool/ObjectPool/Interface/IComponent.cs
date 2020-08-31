using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectPool.Interface
{
    /// <summary>
    /// A generic interface for a component.
    /// </summary>
    public interface IComponent
    {
        /// <summary>
        /// The component owner.
        /// </summary>
        IComponent Owner { get; set; }
        /// <summary>
        /// The name of an object.
        /// </summary>
        string Name { get; set; }
        /// <summary>
        /// The ID of an object.
        /// </summary>
        uint ID { get; set; }
        /// <summary>
        /// Checks if the component is enabled or not.
        /// </summary>
        bool IsEnabled { get; set; }
    }
}
