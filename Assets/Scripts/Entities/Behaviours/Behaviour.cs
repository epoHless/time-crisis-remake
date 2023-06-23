using UnityEngine;

namespace Entities
{
    [System.Serializable]

    public abstract class Behaviour
    {
        [field: SerializeField] public bool IsEnabled { get; private set; }

        public abstract void Tick(Entity _entity);
    }
}
