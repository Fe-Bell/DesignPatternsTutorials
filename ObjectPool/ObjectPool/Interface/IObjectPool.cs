using System;

namespace ObjectPool.Interface
{
    /// <summary>
    /// Interface for ObjectPools.
    /// </summary>
    /// <typeparam name="T">Any type that implements the IComponent interface.</typeparam>
    public interface IObjectPool<T> : IDisposable where T : IComponent
    {
        /// <summary>
        /// Adds an element to the pool.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool Add(T value);
        /// <summary>
        /// Removes an element from the pool.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool Remove(T value);
        /// <summary>
        /// Locate an object inside the pool.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T FindObjectByID(uint id);
        /// <summary>
        /// Locate an object inside the pool.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        T FindObjectByName(string name);
        /// <summary>
        /// Retursn the size of the pool.
        /// </summary>
        /// <returns></returns>
        int GetCount();
        /// <summary>
        /// Returns the number of elements of type T1 in the pool.
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <returns></returns>
        int GetCount<T1>() where T1 : IComponent;
    }
}
