using System;
using DG.Tweening;
using UnityEngine;

namespace Entities
{
    public class ExplosiveBarrel : Entity
    {
        #region Fields

        [SerializeField] private int damage;
        [SerializeField, Range(1, 20)] private float explosionRange;

        [SerializeField] private GameObject mesh;
        [SerializeField] private ParticleSystem explosionParticles;

        #endregion

        #region Methods

        public override void OnDeath()
        {
            base.OnDeath();

            var colliders = Physics.OverlapSphere(transform.position, explosionRange);

            if (colliders.Length > 0)
            {
                foreach (var collider in colliders)
                {
                    if (collider.TryGetComponent(out IDamageable damageable))
                    {
                        if(damageable != this) damageable.TakeDamage(damage);
                    }
                }
            }
            
            PlayParticle();
            EventManager.OnExplosion?.Invoke();

            Sequence ??= DOTween.Sequence()
                .Append(mesh.transform.DOScale(Vector3.zero, .2f).SetEase(Ease.InOutBounce))
                .Append(mesh.transform.DOShakePosition(.2f, 0.1f, 3))
                .Append(mesh.transform.DOShakeScale(.2f))
                .AppendInterval(1.8f);

            Sequence.onComplete += () =>
            {
                gameObject.SetActive(false);
            };
        }

        private void PlayParticle()
        {
            explosionParticles.Play(true);
        }

        #endregion

        #region Unity Methods

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, explosionRange);
        }

        #endregion
    }
}
