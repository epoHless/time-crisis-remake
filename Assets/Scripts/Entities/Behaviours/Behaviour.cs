using UnityEngine;

namespace Entities
{
    [System.Serializable]
    public abstract class Behaviour
    {
        [field: SerializeField] public virtual bool IsEnabled { get; set; }
        public virtual void Tick(Entity _entity){}
    }
}
