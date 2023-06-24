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

    [SerializeField] private List<Entity> entities;

    [HideInInspector] public Vector3 point;

    #endregion
    
    #region Properties

    [field: SerializeField] public bool TriggerArea { get; private set; }

    #endregion

    #region Methods

    public void Initialise()
    {
        foreach (var entity in entities)
        {
            entity.Enable();
        }
    }

    public bool Contains(Entity _entity)
    {
        return entities.Contains(_entity);
    }

    public bool RemoveEntity(Entity _entity)
    {
        if (entities.Contains(_entity)) entities.Remove(_entity);
        return IsCleared();
    }

    private bool IsCleared()
    {
        var enemies = entities.Where(entity => entity is Enemy).ToList();
        return enemies.Count <= 0;
    }

    #endregion
}
