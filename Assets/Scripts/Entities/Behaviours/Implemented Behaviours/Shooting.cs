using UnityEngine;

namespace Entities
{
    public class Shooting : Behaviour
    {
        [SerializeField] private float fireRate;
        [SerializeField] private Transform muzzle;

        private float currentTimer;
        public override bool IsEnabled { get; set; }

        public override void Tick(Entity _entity)
        {
            if (currentTimer <= 0)
            {
                EventManager.OnBulletRequested?.Invoke(muzzle);
                currentTimer = fireRate;
            }

            currentTimer -= Time.deltaTime;
        }
    }
}
