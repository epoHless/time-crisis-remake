using UnityEngine;

[System.Serializable]
public class Checkpoint
{
    public Checkpoint(Vector3 _point)
    {
        point = _point;
        TriggerArea = false;
    }
    
    public Vector3 point;
    
    [field: SerializeField] public bool TriggerArea { get; private set; }
    public CheckpointData checkpointData;
}
