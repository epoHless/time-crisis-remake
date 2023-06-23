using System;
using System.Collections.Generic;
using BezierSplines;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    [SerializeField] private PathCreator creator;

    public PathCreator Creator
    {
        get => creator;
        set
        {
            creator = value;

            SetControlPoints();
        }
    }

    [SerializeField] private List<Checkpoint> checkpoints;

    #region Unity Methods

    private void Awake()
    {
        if(checkpoints.Count == 0) SetControlPoints();
    }

    private void OnEnable()
    {
        EventManager.OnCheckpointReached.AddListener(OnCheckpointReached);
    }

    private void OnDisable()
    {
        EventManager.OnCheckpointReached.RemoveListener(OnCheckpointReached);
    }

    #endregion

    #region Events

    private void OnCheckpointReached(Vector3 position)
    {
        var checkpoint = checkpoints.Find(_checkpoint => Vector3.Distance(_checkpoint.point, position) <= 0.1f);

        if (checkpoint != null)
        {
            if (checkpoint.TriggerArea)
            {
                EventManager.OnStageStart?.Invoke(checkpoint.checkpointData);
            }
            
            checkpoints.Remove(checkpoint);
        }
    }

    #endregion

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

    private void OnDrawGizmos()
    {
        for (int i = 0; i < checkpoints.Count; i++)
        {
            if(checkpoints[i].TriggerArea)
            {
                Gizmos.DrawSphere(checkpoints[i].point, 0.25f);

                // if(i < checkpoints.Count - 1) Gizmos.DrawLine(checkpoints[i].point, checkpoints[i + 1].point);
            }
        }
    }
}
