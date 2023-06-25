using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    #region Fields

    [SerializeField] private float startingTime;

    private TimerTick countdownTimer;
    private TimerTick playerTimer;

    #endregion

    #region Methods

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

    private void Update()
    {
        countdownTimer.Tick(-Time.deltaTime);
        playerTimer.Tick(Time.deltaTime);
        
        EventManager.OnCountdownTick?.Invoke(countdownTimer);
        EventManager.OnTimeTick?.Invoke(playerTimer);
    }

    #endregion

    #region Event Methods

    private void OnCheckpointCleared()
    {
        countdownTimer.Add(15);
    }

    #endregion
}
