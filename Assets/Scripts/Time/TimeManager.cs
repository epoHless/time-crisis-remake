using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    #region Fields

    [SerializeField] private float startingTime;

    private TimerTick countdownTimer;
    private TimerTick playerTimer;

    private bool tick = true;
    
    #endregion

    #region Methods

    private void Awake()
    {
        countdownTimer = new TimerTick(startingTime);
        playerTimer = new TimerTick(0);
    }

    private void OnEnable()
    {
        EventManager.OnTimeAdded.AddListener(OnTimeAdded);
        EventManager.OnGameOver.AddListener(SendFinalTime);
    }

    private void OnDisable()
    {
        EventManager.OnTimeAdded.RemoveListener(OnTimeAdded);
        EventManager.OnGameOver.RemoveListener(SendFinalTime);
    }

    private void Update()
    {
        if (!tick) return;
        
        countdownTimer.Tick(-Time.deltaTime);
        playerTimer.Tick(Time.deltaTime);
        
        EventManager.OnCountdownTick?.Invoke(countdownTimer);
        EventManager.OnTimeTick?.Invoke(playerTimer);
    }

    #endregion

    #region Event Methods

    private void OnTimeAdded(float _time)
    {
        countdownTimer.Add(_time);
    }
    
    private void SendFinalTime(string _value)
    {
        EventManager.OnFinalTimeRequested?.Invoke(playerTimer);
        tick = false;
    }

    #endregion
}
