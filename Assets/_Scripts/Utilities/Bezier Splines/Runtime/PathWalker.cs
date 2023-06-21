using UnityEngine;

public class PathWalker : MonoBehaviour 
{
    public PathCreator Creator;

    public bool lookForward;
    
    public float duration;
    private float progress;

    public SplineWalkerMode mode;

    private bool goingForward = true;

    private void Update () 
    {
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
        if (lookForward) {
            transform.LookAt(position + Creator.path.GetDirection(progress));
        }
    }
}