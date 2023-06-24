using UnityEngine;

public static class EventManager
{
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

    #endregion

    #endregion
}
