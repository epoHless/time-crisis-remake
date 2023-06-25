using DG.Tweening;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    #region Fields

    [SerializeField] private TMP_Text countdownTime;
    [SerializeField] private TMP_Text playerTime;

    #endregion

    #region Unity Methods

    private void OnEnable()
    {
        EventManager.OnCountdownTick.AddListener(OnCountdownTick);
        EventManager.OnTimeTick.AddListener(OnTimeTick);
        
        EventManager.OnCheckpointCleared.AddListener(OnCheckpointCleared);
    }
    
    private void OnDisable()
    {
        EventManager.OnCountdownTick.RemoveListener(OnCountdownTick);
        EventManager.OnTimeTick.RemoveListener(OnTimeTick);
        
        EventManager.OnCheckpointCleared.RemoveListener(OnCheckpointCleared);
    }

    #endregion

    #region Event Methods

    private void OnTimeTick(TimerTick _time)
    {
        playerTime.text = $"{Mathf.Floor(_time.Seconds / 60).ToString("00")}:{(_time.Seconds % 60).ToString("00.00")}";
    }

    private void OnCountdownTick(TimerTick _time)
    {
        countdownTime.text = $"{Mathf.Floor(_time.Seconds / 60).ToString("00")}:{(_time.Seconds % 60).ToString("00.00")}";
    }
    
    private void OnCheckpointCleared()
    {
        countdownTime.DOFontSize(60f, 0.1f).SetLoops(2, LoopType.Yoyo);
    }

    #endregion
}
