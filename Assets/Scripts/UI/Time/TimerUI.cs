using System;
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
    }

    private void OnDisable()
    {
        EventManager.OnCountdownTick.RemoveListener(OnCountdownTick);
        EventManager.OnTimeTick.RemoveListener(OnTimeTick);
    }

    private void OnTimeTick(TimerTick _time)
    {
        playerTime.text = $"{_time.Minutes.ToString("00")}:{_time.Seconds.ToString("00.00")}";
    }

    private void OnCountdownTick(TimerTick _time)
    {
        countdownTime.text = $"{_time.Minutes.ToString("00")}:{_time.Seconds.ToString("00.00")}";
    }
}
