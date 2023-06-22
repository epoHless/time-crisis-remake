using UnityEngine;

public static class EventManager
{
    #region Walker Events

    public static Evt<Vector3> OnCheckpointReached = new Evt<Vector3>();

    #endregion

    #region Level Events

    public static Evt<CheckpointData> OnStageStart = new Evt<CheckpointData>();
    public static Evt OnStageEnd = new Evt();

    #endregion
}
