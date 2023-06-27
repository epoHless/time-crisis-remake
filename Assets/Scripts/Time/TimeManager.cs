using System;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    #region Fields

    [SerializeField] private float startingTime;

    private TimerTick countdownTimer;
    private TimerTick playerTimer;

    private bool tick;
    
    #endregion

    #region Methods

    private void Awake()
    {
        countdownTimer = new TimerTick(startingTime);
        playerTimer = new TimerTick(0);
    }

    private void Start()
    {
        tick = false;
    }

    private void OnEnable()
    {
        EventManager.OnTimeAdded.AddListener(OnTimeAdded);
        EventManager.OnGameOver.AddListener(SendFinalTime);
        
        EventManager.OnGameStart.AddListener(StartTick);
    }

    private void OnDisable()
    {
        EventManager.OnTimeAdded.RemoveListener(OnTimeAdded);
        EventManager.OnGameOver.RemoveListener(SendFinalTime);
        
        EventManager.OnGameStart.RemoveListener(StartTick);
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

    private void StartTick()
    {
        tick = true;
    }
    
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
