using UnityEngine;

namespace BezierSplines
{
    public class PathCreator : MonoBehaviour
    {
        public Path path;

        public Color anchorColor = Color.red;
        public Color controlColor = Color.white;
        public Color segmentColor = Color.green;
        public Color selectedSegmentColor = Color.yellow;

        public float anchorDiameter = 0.1f;
        public float controlDiameter = 0.075f;

        public bool displayControlPoint = true;

        public void CreatePath()
        {
            path = new Path(transform.position);
        }

        void Reset()
        {
            CreatePath();
        }

        [ContextMenu("Reset Y")]
        public void SetY()
        {
            for (int i = 0; i < path.Points.Count; i++)
            {
                var newPoint = path.Points[i];
                newPoint.y = 0;
                path.Points[i] = newPoint;
            }
        }
    }
}
