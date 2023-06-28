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
                walker.Move = isEnabled;
            }
        }

        [SerializeField] private PathWalker walker;
    }
}
