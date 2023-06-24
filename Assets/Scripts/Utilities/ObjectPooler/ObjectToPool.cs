using UnityEngine;

namespace MobileFramework.ObjectPooler
{
    /// <summary>
    /// This class is used to initialise a GameObject in the pool.
    /// </summary>
    [System.Serializable]
    public class ObjectToPool
    {
        /// <summary>
        /// The object you desire to have pooled.
        /// </summary>
        public GameObject objectToPool;
        
        /// <summary>
        /// The amount to create.
        /// </summary>
        public int amount;
        
        /// <summary>
        /// Whether this object should be re-instantiated when there is none available.
        /// </summary>
        public bool shouldExpand;

        public ObjectToPool(GameObject objectToPool, int amount, bool shouldExpand)
        {
            this.objectToPool = objectToPool;
            this.amount = amount;
            this.shouldExpand = shouldExpand;
        }
    }
}