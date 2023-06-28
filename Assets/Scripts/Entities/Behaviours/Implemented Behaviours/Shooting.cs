using UnityEngine;

namespace Entities
{
    public class Shooting : Behaviour
    {
        [SerializeField] private Transform muzzle;
        
        private readonly float minFireRate = 0.5f;
        private readonly float maxFireRate = 2f;

        private float currentTimer;

        public override void Tick(Entity _entity)
        {
            if (currentTimer <= 0)
            {
                EventManager.OnBulletRequested?.Invoke(muzzle);
                currentTimer = Random.Range(minFireRate, maxFireRate);
            }

            currentTimer -= Time.deltaTime;
        }
    }
}
