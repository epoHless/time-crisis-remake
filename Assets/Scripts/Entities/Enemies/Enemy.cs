using System;
using DG.Tweening;
using UnityEngine;

public class Enemy : Entity
{
    #region Unity Methods

    protected virtual void Start()
    {
        transform.localScale = Vector3.zero;
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
    { }

    #endregion
}