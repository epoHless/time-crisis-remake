using UnityEngine;

public class PlayerPathWalker : PathWalker
{
    private Vector3 destination;
    
    #region Unity Methods

    private void Awake()
    {
        Move = false;
    }

    private void OnEnable()
    {
        EventManager.OnCheckpointReached.AddListener(OnCheckpointReached);
        EventManager.OnCheckpointCleared.AddListener(OnCheckpointCleared);
        EventManager.OnCheckpointChanged.AddListener(SetDestination);

        EventManager.OnGameStart.AddListener(OnCheckpointCleared);
    }
    
    private void OnDisable()
    {
        EventManager.OnCheckpointReached.RemoveListener(OnCheckpointReached);
        EventManager.OnCheckpointCleared.RemoveListener(OnCheckpointCleared);
        EventManager.OnCheckpointChanged.RemoveListener(SetDestination);

        EventManager.OnGameStart.RemoveListener(OnCheckpointCleared);
    }
    protected override void Update()
    {
        if (!Move) return;
        
        base.Update();

        if (Vector3.Distance(transform.localPosition, destination) <= tollerance)
        {
            EventManager.OnCheckpointReached?.Invoke(position);
        }
    }

    #endregion
    
    #region Methods

    private void SetDestination(Vector3 obj)
    {
        destination = obj;
    }
    
    private void OnCheckpointCleared()
    {
        Move = true;
    }

    private void OnCheckpointReached(Vector3 obj)
    {
        Move = false;
    }

    #endregion
}
