using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Entities
{
    public class Enemy : Entity
    {
        #region Fields

        [SerializeField] private List<Behaviour> behaviours;

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

        public override void Initialise()
        {
            transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.InOutBack).onComplete += () =>
            {
                Debug.Log($"{gameObject.name} is init!");
            };
        }

        public override void OnDeath()
        {
        }

        #endregion
    }
}