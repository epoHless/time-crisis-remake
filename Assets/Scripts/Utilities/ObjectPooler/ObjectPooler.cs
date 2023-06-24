using System.Collections.Generic;
using UnityEngine;

namespace MobileFramework.ObjectPooler
{
    /// <summary>
    /// Create and fetch GameObjects without the need of destroying and instantiating constantly.
    /// This is the abstract class, to make your own create a new class that inherits from this
    /// </summary>
    /// <typeparam name="T">The type of your class, this allows multiple instances of object poolers making a singleton out of them</typeparam>
    public abstract class ObjectPooler<T> : Singleton<T> where T : ObjectPooler<T>
    {
        /// <summary>
        /// Use this list to setup the ObjectPooler and it's objects.
        /// </summary>
        public List<ObjectToPool> poolables = new List<ObjectToPool>();
        
        /// <summary>
        /// Private queue that manages the fetching of the desired objects.
        /// </summary>
        private Queue<GameObject> pooledObjects;

        /// <summary>
        /// The start function is used to call the Init function and instantiate the choosen objects.
        /// </summary>
        protected virtual void Start()
        {
            InitPool();
        }

        /// <summary>
        /// Fetch a GameObject of a given T from the queue.
        /// </summary>
        /// <param name="itemType">The Type of GameObject to be returned.</param>
        /// <typeparam name="T">The class type this GameObject has attached.</typeparam>
        /// <returns>A GameObject with the attached T.</returns>
        public GameObject GetPooledObject<T>()
        {
            var type = typeof(T);
            
            foreach (var item in pooledObjects)
            {
                if (!item.activeInHierarchy && item.GetType() == type)
                {
                    return item;
                }
            }

            foreach (ObjectToPool poolable in poolables)
            {
                if (poolable.objectToPool.GetType() == type)
                {
                    if (poolable.shouldExpand)
                    {
                        GameObject obj = Instantiate(poolable.objectToPool);
                        pooledObjects.Enqueue(obj);
                        return obj.gameObject;
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Instantiate all the GameObjects inside the pooledObjects list.
        /// </summary>
        private void InitPool()
        {
            pooledObjects = new Queue<GameObject>();

            foreach (var poolable in poolables)
            {
                for (int i = 0; i < poolable.amount; i++)
                {
                    GameObject obj = Instantiate(poolable.objectToPool, gameObject.transform, true);
                    pooledObjects.Enqueue(obj);
                    obj.SetActive(false);
                }
            }
        }
    }
}