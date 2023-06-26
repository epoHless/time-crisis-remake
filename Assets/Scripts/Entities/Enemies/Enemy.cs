using System;
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

        private void OnEnable()
        {
            EventManager.OnGameOver.AddListener(Deactivate);
        }

        private void OnDisable()
        {
            EventManager.OnGameOver.AddListener(Deactivate);
        }

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

        #region Event Methods

        private void Deactivate(string obj)
        {
            ToggleBehaviours(false);
        }

        #endregion
        
        #region Methods

        protected override void Initialise()
        {
            transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.OutBack).onComplete += () => { ToggleBehaviours(true); };

            var position = Utilities.PlayerTransform.position;
            position.y = transform.position.y;
            
            transform.LookAt(position);
        }

        private void ToggleBehaviours(bool _value)
        {
            foreach (var behaviour in behaviours)
            {
                behaviour.IsEnabled = _value;
            }
        }

        public override void OnDeath()
        {
            base.OnDeath();

            transform.DOScale(Vector3.zero, .5f).SetEase(Ease.OutBack).onComplete += () =>
            {
                gameObject.SetActive(false);
            };
        }

        #endregion
    }
}