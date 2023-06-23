using System.Collections.Generic;
using Entities;
using UnityEngine;

[System.Serializable]
public class Checkpoint
{
    public Checkpoint(Vector3 _point)
    {
        point = _point;
        TriggerArea = false;
    }

    [field: SerializeField] public bool TriggerArea { get; private set; }
    
    [SerializeField] private List<Entity> entities;

    
    [HideInInspector] public Vector3 point;

    public void Initialise()
    {
        foreach (var entity in entities)
        {
            entity.Initialise();
        }
    }
}
