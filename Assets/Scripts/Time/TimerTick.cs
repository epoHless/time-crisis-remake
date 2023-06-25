using System;

public class TimerTick
{
    public TimerTick(float _seconds)
    {
        seconds = _seconds;
    }

    private float seconds;
    public float Seconds => seconds;

    private float minutes;
    public float Minutes => minutes;

    public void Tick(float _time)
    {
        seconds += _time;

        if (seconds >= 60)
        {
            seconds = 0;
            minutes++;
        }
    }
}
