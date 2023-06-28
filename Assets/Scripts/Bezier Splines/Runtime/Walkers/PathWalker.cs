using BezierSplines;
using UnityEngine;

public class PathWalker : MonoBehaviour 
{
    #region Fields

    public PathCreator Creator;
    public float tollerance = 0.15f;

    protected Vector3 position;
    public bool lookForward;
    
    public float duration;
    protected float progress;

    public SplineWalkerMode mode;
    private bool goingForward = true;

    public virtual bool Move { get; set; }

    #endregion

    #region Unity Methods

    protected virtual void Update()
    {
        if (!Move) return;
        
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
        
        position = Creator.path.GetPoint(progress);
        transform.localPosition = position;
        
        if (lookForward) transform.LookAt(position + Creator.path.GetDirection(progress));
    }

    #endregion
}