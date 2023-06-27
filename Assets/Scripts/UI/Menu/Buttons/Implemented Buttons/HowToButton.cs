using DG.Tweening;
using UnityEngine;

public class HowToButton : MenuButton
{
    [SerializeField] private RectTransform rectTransform;

    private bool isOpen = false;
    
    protected override void Click()
    {
        isOpen = !isOpen;
        
        rectTransform.DOAnchorPosX( isOpen ? -900 : 0, .5f)
            .SetEase(isOpen ? Ease.OutBack : Ease.InBack);
    }
}
