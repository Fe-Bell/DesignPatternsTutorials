using ECSEngine.Component;
using System;
using System.Collections.Generic;
using System.Text;

namespace ECSEngine.Interface
{
    public interface ISystem : IComponent
    {
        event EventHandler OnSceneChanged;
        Scene CurrentScene { get; set; }
        void Update(float dt);
    }
}
