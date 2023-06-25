using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TMP_Text countdownTime;
    [SerializeField] private TMP_Text playerTime;

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
        countdownTime.DOScale(1.1f, 0.25f).SetLoops(2, LoopType.Yoyo);
    }
}
