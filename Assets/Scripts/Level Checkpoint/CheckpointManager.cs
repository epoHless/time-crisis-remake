﻿using System.Collections.Generic;
using BezierSplines;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    #region Fields

    [SerializeField] private PathCreator creator;

    [SerializeField] private List<Checkpoint> checkpoints;
    [SerializeField] private Checkpoint currentCheckpoint;

    #endregion

    #region Properties

    public PathCreator Creator
    {
        get => creator;
        set
        {
            creator = value;

            SetControlPoints();
        }
    }

    #endregion
    
    #region Unity Methods

    private void Awake()
    {
        if(checkpoints.Count == 0) SetControlPoints();
    }

    private void OnEnable()
    {
        EventManager.OnCheckpointReached.AddListener(OnCheckpointReached);
        EventManager.OnEnemyKilled.AddListener(OnEnemyKilled);
    }

    private void OnDisable()
    {
        EventManager.OnCheckpointReached.RemoveListener(OnCheckpointReached);
        EventManager.OnEnemyKilled.RemoveListener(OnEnemyKilled);
    }
    
    #endregion

    #region Event Methods

    private void OnCheckpointReached(Vector3 position)
    {
        var checkpoint = checkpoints.Find(_checkpoint => Vector3.Distance(_checkpoint.point, position) <= 0.1f);

        if (checkpoint != null)
        {
            currentCheckpoint = checkpoint;

            if (checkpoint.TriggerArea)
            {
                currentCheckpoint.Initialise();
                
                EventManager.OnCheckpointStart?.Invoke();
                EventManager.OnPeekChanged?.Invoke(currentCheckpoint.PeekDirection);
            }

            checkpoints.Remove(checkpoint);
        }
    }
    
    private void OnEnemyKilled(Entities.Entity _enemy)
    {
        if (!currentCheckpoint.Contains(_enemy)) return;
        
        currentCheckpoint.RemoveEntity(_enemy);
            
        if (currentCheckpoint.HasWavesLeft() && currentCheckpoint.IsCleared())
        {
            currentCheckpoint.Initialise();
        }
        else if (!currentCheckpoint.HasWavesLeft() && currentCheckpoint.IsCleared())
        {
            if(checkpoints.Count > 1)
            {
                EventManager.OnCheckpointCleared?.Invoke();
                EventManager.OnTimeAdded?.Invoke(15);
            }
            else 
                EventManager.OnGameOver?.Invoke("Completed");
        }
    }

    #endregion

    #region Methods

    [ContextMenu("Add Control Points")]
    public void SetControlPoints()
    {
        if (!creator) return;
        
        var controlPoints = creator.path.GetControlPoints();
        checkpoints = new List<Checkpoint>();

        foreach (var controlPoint in controlPoints)
        {
            checkpoints.Add(new Checkpoint(controlPoint));
        }
    }

    #endregion
}
