using ObjectPool.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace ObjectPool
{
    /// <summary>
    /// Dummy component.
    /// </summary>
    public class TestComponent : IComponent
    {
        public IComponent Owner { get; set; }
        public string Name { get; set; }
        public uint ID { get; set; }
        public bool IsEnabled { get; set; }

        public byte[] UniqueData { get; set; }

        public TestComponent()
        {
           
        }
    }
}
