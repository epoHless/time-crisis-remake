using BezierSplines;
using UnityEngine;

public class PathWalker : MonoBehaviour 
{
    #region Fields

    public PathCreator Creator;
    public float tollerance = 0.15f;

    public bool lookForward;
    
    public float duration;
    private float progress;

    public SplineWalkerMode mode;
    private bool goingForward = true;

    public bool move;

    #endregion

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

    private void Update()
    {
        if (!move) return;
        
        if (goingForward) 
        {
            progress += Time.deltaTime / duration;
            
            if (progress > 1f) 
            {
                if (mode == SplineWalkerMode.Once) 
                {
                    progress = 1f;
                }
                else if (mode == SplineWalkerMode.Loop) 
                {
                    progress -= 1f;
                }
                else 
                {
                    progress = 2f - progress;
                    goingForward = false;
                }
            }
        }
        else 
        {
            progress -= Time.deltaTime / duration;
            if (progress < 0f) 
            {
                progress = -progress;
                goingForward = true;
            }
        }

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