using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(PathWalker))]
    public class Movement : Behaviour
    {
        [SerializeField] private PathWalker walker;

        public override void Tick(Entity _entity)
        {
            walker.move = _entity.IsEnabled();
        }
    }
}
