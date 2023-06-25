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

    private void Update()
    {
        countdownTimer.Tick(-Time.deltaTime);
        playerTimer.Tick(Time.deltaTime);
        
        EventManager.OnCountdownTick?.Invoke(countdownTimer);
        EventManager.OnTimeTick?.Invoke(playerTimer);
        
        Debug.Log($"player {playerTimer.Seconds.ToString("00.00")} | countdown {countdownTimer.Seconds.ToString("00.00")}");
    }

    // [SerializeField] private double startingMinutes;
    //
    // private bool tick = true;
    //
    // private DateTime timeStarted;
    //
    // private TimeSpan totalTime;
    // private TimeSpan currentTime;
    //
    // private TimeSpan TimeLeft
    // {
    //     get
    //     {
    //         var result = totalTime - (DateTime.UtcNow - timeStarted);
    //         
    //         if (result.TotalSeconds <= 0)
    //         {
    //             EventManager.OnGameOver?.Invoke();
    //             return TimeSpan.Zero;
    //         }
    //
    //         return result;
    //     }
    // }
    //
    // private void Start()
    // {
    //     StartCountDown(TimeSpan.FromMinutes(startingMinutes));
    // }
    //
    // private void Update()
    // {
    //     if (!tick) return;
    //     double delta = Time.deltaTime;
    //     currentTime += TimeSpan.FromMilliseconds(delta);
    //     Debug.Log(currentTime.ToString());
    //     EventManager.OnTimeTick?.Invoke(TimeLeft);
    // }
    //
    // private void StartCountDown(TimeSpan totalTime)
    // {
    //     timeStarted = DateTime.UtcNow;
    //     this.totalTime = totalTime;
    // }
}
