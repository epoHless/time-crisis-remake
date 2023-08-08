using System.Collections.Generic;
using System.Linq;
using BezierSplines;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    #region Fields

    [SerializeField] private PathCreator creator;

    [SerializeField] private List<Checkpoint> checkpoints;
    [SerializeField] private List<Checkpoint> actualCheckpoints;
    private List<Checkpoint> Checkpoints => checkpoints.Where(checkpoint => checkpoint.TriggerArea).ToList();
    
    [SerializeField] private Checkpoint currentCheckpoint;
    private Checkpoint lastCheckpoint;

    #endregion

    #region Properties

    public PathCreator Creator
    {
        get => creator;
        set
        {
            creator = value;
        }
    }

    #endregion
    
    #region Unity Methods

    private void Awake()
    {
        actualCheckpoints = Checkpoints;
    }

    private void Start()
    {
        EventManager.OnCheckpointChanged?.Invoke(Checkpoints[0].point);
        lastCheckpoint = Checkpoints[^1];
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
        if (currentCheckpoint == lastCheckpoint && !currentCheckpoint.HasWavesLeft())
        {
            EventManager.OnGameOver?.Invoke("completed");
            return;
        }
        
        var checkpoint = Checkpoints.Find(_checkpoint => Vector3.Distance(_checkpoint.point, position) <= 0.1f);

        if (checkpoint != null)
        {
            currentCheckpoint = checkpoint;

            if (currentCheckpoint.TriggerArea)
            {
                currentCheckpoint.Initialise();
                
                EventManager.OnCheckpointStart?.Invoke();
                EventManager.OnPeekChanged?.Invoke(currentCheckpoint.PeekDirection);
            }

            if (Checkpoints.Count > Checkpoints.IndexOf(currentCheckpoint) + 1)
            {
                var next = Checkpoints[Checkpoints.IndexOf(currentCheckpoint) + 1];

                if (next != null && next.TriggerArea)
                {
                    EventManager.OnCheckpointChanged?.Invoke(next.point);
                }
            }

            Checkpoints.Remove(checkpoint);
        }
    }
    
    private void OnEnemyKilled(Entities.Entity _enemy)
    {
        if (!currentCheckpoint.Contains(_enemy)) return;
        
        currentCheckpoint.RemoveEntity(_enemy);
            
        if (currentCheckpoint.HasWavesLeft() && currentCheckpoint.IsWaveCleared())
        {
            currentCheckpoint.Initialise();
        }
        else if (!currentCheckpoint.HasWavesLeft() && currentCheckpoint.IsWaveCleared())
        {
            if(Checkpoints.Count > 0)
            {
                EventManager.OnCheckpointCleared?.Invoke();
                EventManager.OnTimeAdded?.Invoke(15);
            }
            else 
                EventManager.OnGameOver?.Invoke("completed");
        }
    }

    #endregion
}