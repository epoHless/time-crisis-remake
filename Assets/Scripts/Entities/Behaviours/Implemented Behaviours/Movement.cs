using UnityEngine;

namespace Entities
{
    public class Movement : Behaviour
    {
        [SerializeField] private PathWalker walker;

        public override void Tick(Entity _entity)
        {
            walker.move = true;
        }
    }
}
