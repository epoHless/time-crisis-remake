using DG.Tweening;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    #region Fields

    [SerializeField] private TMP_Text countdownTime;
    [SerializeField] private TMP_Text additionalTime;
    [SerializeField] private TMP_Text playerTime;

    private string countdownText;
    private string playerText;
    
    #endregion

    #region Unity Methods

    private void OnEnable()
    {
        EventManager.OnCountdownTick.AddListener(OnCountdownTick);
        EventManager.OnTimeTick.AddListener(OnTimeTick);
        
        EventManager.OnTimeAdded.AddListener(OnCheckpointCleared);
    }
    
    private void OnDisable()
    {
        EventManager.OnCountdownTick.RemoveListener(OnCountdownTick);
        EventManager.OnTimeTick.RemoveListener(OnTimeTick);
        
        EventManager.OnTimeAdded.RemoveListener(OnCheckpointCleared);
    }

    #endregion

    #region Event Methods

    private void OnTimeTick(TimerTick _time)
    {
        playerText = $"{Mathf.Floor(_time.Seconds / 60).ToString("00")}:{(_time.Seconds % 60).ToString("00.00")}";
        playerTime.text = playerText;
    }

    private void OnCountdownTick(TimerTick _time)
    {
        countdownText = $"{Mathf.Floor(_time.Seconds / 60).ToString("00")}:{(_time.Seconds % 60).ToString("00.00")}";
        countdownTime.text = countdownText;
    }
    
    private void OnCheckpointCleared(float _time)
    {
        additionalTime.text = $"+{_time} sec";

        if (DOTween.IsTweening(additionalTime))
        {
            DOTween.Kill(additionalTime, true);
        }
        
        additionalTime.DOFade(1, .75f)
            .SetLoops(2, LoopType.Yoyo);
        
        additionalTime.rectTransform.DOAnchorPosX(120f, .75f)
            .SetLoops(2, LoopType.Yoyo)
            .SetEase(Ease.OutBack);
        
        countdownTime.DOFontSize(60f, 0.1f).SetLoops(2, LoopType.Yoyo);
    }

    #endregion
}
