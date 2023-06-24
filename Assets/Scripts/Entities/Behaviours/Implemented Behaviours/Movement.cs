using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(PathWalker))]
    public class Movement : Behaviour
    {
        private bool isEnabled;

        public override bool IsEnabled
        {
            get => isEnabled;
            set
            {
                isEnabled = value;
                walker.move = isEnabled;
            }
        }

        [SerializeField] private PathWalker walker;

        public override void Tick(Entity _entity)
        {
            walker.move = _entity.IsEnabled();
        }
    }
}
