using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Image hitImage;
    [SerializeField] private List<RectTransform> rectTransforms;

    private void OnEnable()
    {
        EventManager.OnDamageTaken.AddListener(OnDamageTaken);
        EventManager.OnGameStart.AddListener(Fill);
    }

    private void OnDisable()
    {
        EventManager.OnDamageTaken.RemoveListener(OnDamageTaken);
        EventManager.OnGameStart.RemoveListener(Fill);
    }
    
    private void Fill()
    {
        foreach (var rectTransform in rectTransforms)
        {
            rectTransform.gameObject.SetActive(true);
        }
    }

    private void OnDamageTaken(int _currentHealth)
    {
        rectTransforms[_currentHealth].gameObject.SetActive(false);
        hitImage.DOFade(.2f, 0.15f).SetLoops(2, LoopType.Yoyo);
    }
}
