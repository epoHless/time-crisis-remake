using UnityEngine;

[CreateAssetMenu(menuName = "Checkpoint/New Checkpoint", order = 0)]
public class CheckpointData : ScriptableObject
{
    [SerializeField] private float speed;

    public float Speed
    {
        get => speed;
        set => speed = value;
    }
}
