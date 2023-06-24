﻿using UnityEngine;

public class PlayerPathWalker : PathWalker
{
    #region Unity Methods

    private void OnEnable()
    {
        EventManager.OnCheckpointStart.AddListener(OnCheckpointStart);
        EventManager.OnCheckpointCleared.AddListener(OnCheckpointCleared);
    }

    private void OnDisable()
    {
        EventManager.OnCheckpointStart.RemoveListener(OnCheckpointStart);
        EventManager.OnCheckpointCleared.RemoveListener(OnCheckpointCleared);
    }
    protected override void Update()
    {
        base.Update();
        
        Vector3 position = Creator.path.GetPoint(progress);
        transform.localPosition = position;
        
        if (lookForward) transform.LookAt(position + Creator.path.GetDirection(progress));

        if (Vector3.Distance(transform.localPosition, position) <= tollerance)
        {
            EventManager.OnCheckpointReached?.Invoke(position);
        }
    }

    #endregion
    
    #region Methods

    private void OnCheckpointCleared()
    {
        move = true;
    }

    private void OnCheckpointStart()
    {
        move = false;
    }

    #endregion
}