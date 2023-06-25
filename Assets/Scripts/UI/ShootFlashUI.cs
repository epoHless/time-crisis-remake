using System;
using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup)), DisallowMultipleComponent]
public class ShootFlashUI : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        EventManager.OnBulletFired.AddListener(OnBulletFired);
    }

    private void OnDisable()
    {
        EventManager.OnBulletFired.RemoveListener(OnBulletFired);
    }

    private void OnBulletFired(int _value)
    {
        canvasGroup.DOFade(1, 0.05f).SetLoops(2, LoopType.Yoyo);
    }
}
