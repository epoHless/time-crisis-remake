using UnityEngine;

public static class EventManager
{
    #region Walker Events

    public static Evt<Vector3> OnCheckpointReached;

    #endregion

    #region Level Events

    public static Evt<CheckpointData> OnStageStart;
    public static Evt OnStageEnd;

    #endregion
}
