using UnityEngine;

namespace Entities
{
    public abstract class Entity : MonoBehaviour, IDamageable
    {
        private bool isActive;

        private bool IsActive
        {
            get => isActive;
            set
            {
                isActive = value;
                if (isActive) Initialise();
            }
        }

        #region Methods

        public bool IsEnabled()
        {
            return isActive;
        }
        
        public void Enable() => IsActive = true;
        public void Disable() => IsActive = false;

        protected abstract void Initialise();

        #endregion

        #region IDamageable Implementation

        [SerializeField] private int health;

        public int Health
        {
            get => health;
            set => health = value;
        }

        public abstract void OnDeath();

        #endregion
    }
}
