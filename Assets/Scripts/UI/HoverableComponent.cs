using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverableComponent : MonoBehaviour , IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float relativeScale = 0.97f;
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.DOScale(Vector3.one * relativeScale, .25f).SetLoops(-1, LoopType.Yoyo). SetEase(Ease.InOutCirc);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (DOTween.IsTweening(transform))
        {
            DOTween.Kill(transform);
            transform.DOScale(Vector3.one, .15f);
        }
    }
}
