using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private float startingTime;

    private TimerTick countdownTimer;
    private TimerTick playerTimer;

    private void Awake()
    {
        countdownTimer = new TimerTick(startingTime);
        playerTimer = new TimerTick(0);
    }

    private void OnEnable()
    {
        EventManager.OnCheckpointCleared.AddListener(OnCheckpointCleared);
    }

    private void OnDisable()
    {
        EventManager.OnCheckpointCleared.AddListener(OnCheckpointCleared);
    }

    private void OnCheckpointCleared()
    {
        countdownTimer.Add(15);
    }

    private void Update()
    {
        countdownTimer.Tick(-Time.deltaTime);
        playerTimer.Tick(Time.deltaTime);
        
        EventManager.OnCountdownTick?.Invoke(countdownTimer);
        EventManager.OnTimeTick?.Invoke(playerTimer);
    }
}
