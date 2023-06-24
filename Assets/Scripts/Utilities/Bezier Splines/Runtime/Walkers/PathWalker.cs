using BezierSplines;
using UnityEngine;

public class PathWalker : MonoBehaviour 
{
    #region Fields

    public PathCreator Creator;
    public float tollerance = 0.15f;

    public bool lookForward;
    
    public float duration;
    protected float progress;

    public SplineWalkerMode mode;
    private bool goingForward = true;

    public bool move;

    #endregion

    #region Unity Methods

    protected virtual void Update()
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
    }

    #endregion
}