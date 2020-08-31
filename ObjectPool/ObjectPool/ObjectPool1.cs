using ObjectPool.Interface;
using System;
using System.Collections.Generic;

namespace ObjectPool
{
    /// <summary>
    /// Object pool that does not rely on any property of the internal components, beside their ID.
    /// </summary>
    /// <typeparam name="T">Any type that inherits from IComponent.</typeparam>
    public class ObjectPool1<T> : IObjectPool<T>
        where T : IComponent
    {
        /// <summary>
        /// A native container that will hold all elements in the pool.
        /// </summary>
        private readonly List<T> pool = null;
        /// <summary>
        /// The index of the last element used by a third party.
        /// </summary>
        private int indexOfLastObjectUsed = -1;

        /// <summary>
        /// Object pool that does not rely on any property of the internal components, beside their ID.
        /// </summary>
        public ObjectPool1()
        {
            pool = new List<T>();
        }

        /// <summary>
        /// Adds an element to the pool.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool Add(T value)
        {
            if (value != null)
            {
                T component = FindObjectByID(value.ID);
                if (component == null)
                {
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
                //If the requested element was the last object in use
                if (value.ID == indexOfLastObjectUsed)
                {
                    //Decrement the index to the previous
                    if (indexOfLastObjectUsed >= 0 && indexOfLastObjectUsed < pool.Count)
                    {
                        indexOfLastObjectUsed--;
                    }
                }

                return pool.Remove(value);
            }

            return false;
        }
        /// <summary>
        /// Gets the next element of the pool.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetNext(out T value)
        {
            value = default;
            //Checks if there is an object selected
            if (indexOfLastObjectUsed >= 0)
            {
                //Checks if the current element is the last in the container
                if(indexOfLastObjectUsed == pool.Count - 1)
                {
                    //If yes, the returns the first object
                    indexOfLastObjectUsed = 0;
                }
                //Else, will try to pick the next element
                else
                {
                    int nextIndex = indexOfLastObjectUsed + 1;
                    if (nextIndex < pool.Count)
                    {
                        indexOfLastObjectUsed = nextIndex;
                    }
                    //If the next index is greater than the last index of the pool, return first element
                    else
                    {
                        indexOfLastObjectUsed = 0;
                    }
                }

                value = pool[indexOfLastObjectUsed];
            }
            else
            {
                //Returns first element case none had been picked before.
                if(pool.Count > 0)
                {
                    value = pool[0];
                    indexOfLastObjectUsed = 0;
                }
            }

            return indexOfLastObjectUsed >= 0;
        }
        /// <summary>
        /// Gets the previous element of the pool.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public bool TryGetPrevious(out T value)
        {
            value = default;
            //Checks if there is an object selected
            if (indexOfLastObjectUsed >= 0)
            {
                //Checks if the current element is the first in the container
                if (indexOfLastObjectUsed == 0)
                {
                    //If yes, the returns the last object
                    indexOfLastObjectUsed = pool.Count - 1;
                }
                //Else, will try to pick the previous element
                else
                {
                    int previous = indexOfLastObjectUsed - 1;
                    if (previous >= 0)
                    {
                        indexOfLastObjectUsed = previous;                 
                    }
                    //If the next index is greater than the last index of the pool, return first element
                    else
                    {
                        indexOfLastObjectUsed = pool.Count - 1;
                    }
                }

                value = pool[indexOfLastObjectUsed];
            }
            else
            {
                //Returns first element case none had been picked before.
                if (pool.Count > 0)
                {
                    indexOfLastObjectUsed = pool.Count - 1;
                    value = pool[indexOfLastObjectUsed];              
                }
            }

            return indexOfLastObjectUsed >= 0;
        }
        /// <summary>
        /// Locate an object inside the pool.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T FindObjectByID(uint id)
        {
            T component = default;
            for (int i = 0; i < pool.Count; i++)
            {
                T _component = pool[i];
                if (_component != null && _component.ID == id)
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
                if (_component != null && _component.Name == name)
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

            for(int i = 0; i < pool.Count; i++)
            {
                T _component = pool[i];
                if(_component != null && _component is T1)
                {
                    count++;
                }
            }

            return count;
        }
        /// <summary>
        /// Releases internal resources.
        /// </summary>
        public void Dispose()
        {
            for (int i = 0; i < pool.Count; i++)
            {
                T _component = pool[i];
                if (_component != null && typeof(IDisposable).IsAssignableFrom(_component.GetType()))
                {
                    (_component as IDisposable).Dispose();
                }
            }

            pool.Clear();
        }
    }
}
