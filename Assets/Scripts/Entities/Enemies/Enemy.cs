using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Entities
{
    public class Enemy : Entity
    {
        #region Fields

        [SerializeReference] private List<Behaviour> behaviours;

        #endregion

        #region Properties

        public List<Behaviour> Behaviours => behaviours;

        #endregion

        #region Unity Methods

        protected virtual void Start()
        {
            transform.localScale = Vector3.zero;
        }

        private void Update()
        {
            foreach (var behaviour in behaviours)
            {
                if (behaviour.IsEnabled) behaviour.Tick(this);
            }
        }

        #endregion

        #region Methods

        protected override void Initialise()
        {
            transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.InOutBack).onComplete += () =>
            {
                Debug.Log($"{gameObject.name} is init!");
                
                foreach (var behaviour in behaviours)
                {
                    behaviour.IsEnabled = true;
                }
            };
        }

        public override void OnDeath()
        {
            base.OnDeath();
            
            Sequence ??= DOTween.Sequence()
                .Append(transform.DOScale(Vector3.zero, .2f).SetEase(Ease.InOutBounce))
                .Append(transform.DOShakePosition(.2f, 0.1f, 3))
                .Append(transform.DOShakeScale(.2f));

            Sequence.onComplete += () =>
            {
                gameObject.SetActive(false);
            };
        }

        #endregion
    }
}