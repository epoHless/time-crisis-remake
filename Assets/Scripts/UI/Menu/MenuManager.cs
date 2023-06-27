using DG.Tweening;
using UnityEngine;

[RequireComponent(typeof(CanvasGroup)), DisallowMultipleComponent]
public class MenuManager : MonoBehaviour
{
    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        EventManager.OnGameStart.AddListener(FadeOut);
        EventManager.OnMenuRequested.AddListener(FadeIn);
    }

    private void OnDisable()
    {
        EventManager.OnGameStart.AddListener(FadeOut);
    }
    
    private void FadeIn()
    {
        canvasGroup.DOFade(1, .25f);
        ToggleCanvasGroup(true);
    }
    
    private void FadeOut()
    {
        canvasGroup.DOFade(0, .25f);
        ToggleCanvasGroup(false);
    }

    private void ToggleCanvasGroup(bool _value)
    {
        canvasGroup.interactable = _value;
        canvasGroup.blocksRaycasts = _value;
    }
}
