using DG.Tweening;
using UnityEngine;

public class QuitButton : MenuButton
{
    [SerializeField] private CanvasGroup canvasGroup;
    
    protected override void Click()
    {
        DOTween.Sequence()
            .Append(canvasGroup.DOFade(1, .2f))
            .AppendInterval(2f).onComplete += Application.Quit;
    }
}
