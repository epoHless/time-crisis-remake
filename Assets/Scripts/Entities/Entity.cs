using DG.Tweening;
using UnityEngine;

namespace Entities
{
    public abstract class Entity : MonoBehaviour, IDamageable
    {
        protected virtual Sequence Sequence { get; set; }

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

        #region Unity Methods

        private void Awake()
        {
            Collider = GetComponent<CapsuleCollider>();
        }

        #endregion
        
        #region Methods

        public bool IsEnabled()
        {
            return isActive;
        }
        
        public void Enable() => IsActive = true;

        protected virtual void Initialise() {}

        #endregion

        #region IDamageable Implementation

        [SerializeField] private int health;

        public CapsuleCollider Collider { get; set; }

        public int Health
        {
            get => health;
            set => health = value;
        }

        public virtual void OnDeath()
        {
            EventManager.OnEnemyKilled?.Invoke(this);
        }

        #endregion
    }
}
