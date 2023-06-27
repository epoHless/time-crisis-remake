using System;
using UnityEngine;

public class PlayerPathWalker : PathWalker
{
    #region Unity Methods

    private void Awake()
    {
        move = false;
    }

    private void OnEnable()
    {
        EventManager.OnCheckpointStart.AddListener(OnCheckpointStart);
        EventManager.OnCheckpointCleared.AddListener(OnCheckpointCleared);
        
        EventManager.OnGameStart.AddListener(OnCheckpointCleared);
    }

    private void OnDisable()
    {
        EventManager.OnCheckpointStart.RemoveListener(OnCheckpointStart);
        EventManager.OnCheckpointCleared.RemoveListener(OnCheckpointCleared);
        
        EventManager.OnGameStart.RemoveListener(OnCheckpointCleared);
    }
    protected override void Update()
    {
        base.Update();

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
