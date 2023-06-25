using System;

public class TimerTick
{
    #region Constructor

    public TimerTick(float _seconds)
    {
        seconds = _seconds;
    }

    #endregion

    #region Fields

    private float seconds;
    private float minutes;
    

    #endregion
    #region Properties

    public float Seconds => seconds;

    #endregion

    #region Methods

    public void Add(float _value)
    {
        seconds += _value;
    }
    
    public void Tick(float _time)
    {
        seconds += _time;
    }

    #endregion
}
