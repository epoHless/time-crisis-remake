using System;
using UnityEngine;

public static class EventManager
{
    #region Game Events

    public static Evt OnGameOver = new Evt();

    #endregion
    
    #region Timer Events

    public static Evt OnTimeAdded = new Evt();
    
    public static Evt<TimerTick> OnCountdownTick = new Evt<TimerTick>();
    public static Evt<TimerTick> OnTimeTick = new Evt<TimerTick>();

    #endregion
    
    #region Walker Events

    public static Evt<Vector3> OnCheckpointReached = new Evt<Vector3>();

    #endregion

    #region Level Events

    public static Evt OnCheckpointStart = new Evt();
    public static Evt OnCheckpointCleared = new Evt();

    public static Evt<Entities.Entity> OnEnemyKilled = new Evt<Entities.Entity>();

    #endregion

    #region Player Events

    #region Shooting Events

    public static Evt<int> OnBulletFired = new Evt<int>();
    public static Evt OnReload = new Evt();
    
    public static Evt<Vector3> OnBulletHit = new Evt<Vector3>();

    #endregion

    #endregion
}
