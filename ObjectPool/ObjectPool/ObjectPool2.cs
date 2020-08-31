using ObjectPool.Interface;
using System.Collections.Generic;

namespace ObjectPool
{
    /// <summary>
    /// Object pool that uses the ID and IsEnabled of the component class to manage the activity of the internal components.
    /// </summary>
    /// <typeparam name="T">Any type that inherits from IComponent.</typeparam>
    public class ObjectPool2<T> : IObjectPool<T>
        where T : IComponent
    {
        /// <summary>
        /// A native container that will hold all elements in the pool.
        /// </summary>
        private readonly List<T> pool = null;

        /// <summary>
        /// Object pool that uses the ID and IsEnabled of the component class to manage the activity of the internal components.
        /// </summary>
        public ObjectPool2()
        {
            pool = new List<T>();
        }

        /// <summary>
        /// Adds an object to the pool.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Add(T value)
        {
            if(value != null)
            {
                T component = FindObjectByID(value.ID);
                if (component == null)
                {
                    value.IsEnabled = false;

                    pool.Add(value);
                    return true;
                }
            }

            return false;
        }
        /// <summary>
        /// Removes an element from the pool.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Remove(T value)
        {
            if (value != null)
            {
                return pool.Remove(value);
            }

            return false;
        }
        /// <summary>
        /// Attempts to take an element from the pool. This removes the element from the container. Matches the first element that has the specified ID.
        /// </summary>
        /// <param name="id">The ID of an element.</param>
        /// <param name="value">The element taken. Null when it fails.</param>
        /// <returns>True if success, false otherwise.</returns>
        public bool TryTakeObject(uint id, out T value)
        {
            value = FindObjectByID(id);
            if(value != null)
            {
                value.IsEnabled = true;

                return pool.Remove(value); ;
            }

            return false;
        }
        /// <summary>
        /// Attempts to take an element from the pool. This removes the element from the container. Matches the first element that has the specified name.
        /// </summary>
        /// <param name="name">The name of an element.</param>
        /// <param name="value">The element taken. Null when it fails.</param>
        /// <returns>True if success, false otherwise.</returns>
        public bool TryTakeObject(string name, out T value)
        {
            value = FindObjectByName(name);
            if (value != null)
            {
                value.IsEnabled = true;

                return pool.Remove(value); ;
            }

            return false;
        }
        /// <summary>
        /// Locate an object inside the pool.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T FindObjectByID(uint id)
        {
            T component = default;
            for(int i = 0; i < pool.Count; i++)
            {
                T _component = pool[i];
                if(_component.ID == id)
                {
                    component = _component;
                    break;
                }
            }

            return component;
        }
        /// <summary>
        /// Locate an object inside the pool.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public T FindObjectByName(string name)
        {
            T component = default;
            for (int i = 0; i < pool.Count; i++)
            {
                T _component = pool[i];
                if (_component.Name == name)
                {
                    component = _component;
                    break;
                }
            }

            return component;
        }
        /// <summary>
        /// Retursn the size of the pool.
        /// </summary>
        /// <returns></returns>
        public int GetCount() => pool.Count;
        /// <summary>
        /// Returns the number of elements of type T1 in the pool.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <returns></returns>
        public int GetCount<T1>() where T1 : IComponent
        {
            int count = 0;

            for (int i = 0; i < pool.Count; i++)
            {
                T component = pool[i];
                if (component is T1)
                {
                    count++;
                }
            }

            return count;
        }
        /// <summary>
        /// Releases internal resources.
        /// </summary>
        public void Dispose() => pool.Clear();
    }
}
