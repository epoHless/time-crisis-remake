using System.Collections.Generic;
using System.Linq;
using Entities;
using UnityEngine;

[System.Serializable]
public class Checkpoint
{
    #region Constructor

    public Checkpoint(Vector3 _point)
    {
        point = _point;
        TriggerArea = false;
    }

    #endregion

    #region Fields

    [SerializeField] private List<Wave> waves;
    [SerializeField] private Wave currentWave;

    [HideInInspector] public Vector3 point;

    #endregion
    
    #region Properties

    [field: SerializeField] public bool TriggerArea { get; private set; }

    #endregion

    #region Methods

    public void Initialise()
    {
        currentWave = waves[0];
        waves.Remove(currentWave);
        
        if(currentWave.entities.Count == 0)
        {
            EventManager.OnCheckpointCleared?.Invoke();
            return;
        }

        foreach (var entity in currentWave.entities)
        {
            entity.Enable();
        }
        
        EventManager.OnCameraRotationRequested?.Invoke(currentWave.cameraRotation);
    }

    public bool Contains(Entity _entity)
    {
        if (currentWave.entities.Contains(_entity)) return true;

        return false;
    }

    public void RemoveEntity(Entity _entity)
    {
        if (currentWave.entities.Contains(_entity)) currentWave.entities.Remove(_entity);
    }

    public bool IsCleared()
    {
        var enemies = currentWave.entities.Where(entity => entity is Enemy).ToList();
        return enemies.Count <= 0;
    }

    public bool HasWavesLeft()
    {
        return waves.Count > 0;
    }

    #endregion
}
