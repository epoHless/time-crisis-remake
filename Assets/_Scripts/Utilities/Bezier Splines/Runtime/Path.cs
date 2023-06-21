using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Path 
{
    [SerializeField] private List<Vector3> points;
    [SerializeField, HideInInspector] public bool isClosed;

    [SerializeField, HideInInspector] private bool autoSetControlPoints;
    
    public Path(Vector2 centre)
    {
        points = new List<Vector3>()
        {
            centre + Vector2.left,
            centre + (Vector2.left + Vector2.up) * 0.5f,
            centre + (Vector2.right + Vector2.down) * 0.5f,
            centre + Vector2.right
        };
    }

    public Vector3 this[int i] => points[i];

    public bool IsClosed
    {
        get => isClosed;
        set
        {
            if (isClosed != value)
            {
                isClosed = value;

                if (isClosed)
                {
                    points.Add(points[^1] * 2 - points[^2]);
                    points.Add(points[0] * 2 - points[1]);

                    if (autoSetControlPoints)
                    {
                        AutoSetAnchorControlPoints(0);
                        AutoSetAnchorControlPoints(points.Count - 3);
                    }
                }
                else
                {
                    points.RemoveRange(points.Count - 2, 2);

                    if (autoSetControlPoints)
                    {
                        AutoSetStartAndEndControls();
                    }
                }
            }
        }
    }
    
    public bool AutoSetControlPoints
    {
        get
        {
            return autoSetControlPoints;
        }
        set
        {
            if (autoSetControlPoints != value)
            {
                autoSetControlPoints = value;
                if (autoSetControlPoints)
                {
                    AutoSetAllControlPoints();
                }
            }
        }
    }
    public int NumPoints => points.Count;
    public int NumSegments => points.Count / 3;

    public void AddSegment(Vector3 anchorPos)
    {
        points.Add(points[^1] * 2 - points[^2]);
        points.Add((points[^1] + anchorPos) * 0.5f);
        points.Add(anchorPos);

        if (autoSetControlPoints)
        {
            AutoSetAllAffectedControlPoints(points.Count - 1);
        }
    }

    public void SplitSegment(Vector3 anchorPos, int segmentIndex)
    {
        points.InsertRange(segmentIndex * 3 + 2, new Vector3[]{Vector3.zero, anchorPos, Vector3.zero});
        
        if (autoSetControlPoints)
        {
            AutoSetAllAffectedControlPoints(segmentIndex * 3 + 3);
        }
        else
        {
            AutoSetAnchorControlPoints(segmentIndex * 3 + 3);    
        }
    }

    public void DeleteSegment(int anchorIndex)
    {
        if (NumSegments > 2 || !isClosed && NumSegments > 1)
        {
            if (anchorIndex == 0)
            {
                if (isClosed)
                {
                    points[^1] = points[2];
                }
                points.RemoveRange(0, 3);
            }
            else if (anchorIndex == points.Count - 1 && !isClosed)
            {
                points.RemoveRange(anchorIndex - 2, 3);
            }
            else
            {
                points.RemoveRange(anchorIndex - 1, 3);
            }
        }
    }
    
    public Vector3[] GetPointsInSegment(int i)
    {
        return new Vector3[] { points[i * 3], points[i * 3 + 1], points[i * 3 + 2], points[LoopIndex(i * 3 + 3)] };
    }

    public void MovePoint(int i, Vector3 pos)
    {
        Vector3 deltaMove = pos - points[i];
        
        if (i % 3 == 0 || !autoSetControlPoints)
        {
            points[i] = pos;

            if (autoSetControlPoints)
            {
                AutoSetAllAffectedControlPoints(i);
            }
            else
            {
                if (i % 3 == 0)
                {
                    if (i + 1 < points.Count || isClosed)
                    {
                        points[LoopIndex(i + 1)] += deltaMove;
                    }
                    if (i - 1 >= 0 || isClosed)
                    {
                        points[LoopIndex(i - 1)] += deltaMove;
                    }
                }
                else
                {
                    bool nextPointIsAnchor = (i + 1) % 3 == 0;
                    int correspondingControlIndex = (nextPointIsAnchor) ? i + 2 : i - 2;
                    int anchorIndex = (nextPointIsAnchor) ? i + 1 : i - 1;
            
                    if (correspondingControlIndex >= 0 && correspondingControlIndex < points.Count || isClosed)
                    {
                        float distance = (points[LoopIndex(anchorIndex)] - points[LoopIndex(correspondingControlIndex)]).magnitude;
                        Vector3 dir = (points[LoopIndex(anchorIndex)] - pos).normalized;
                        points[LoopIndex(correspondingControlIndex)] = points[LoopIndex(anchorIndex)] + dir * distance;
                    }
                }
            }
        }
    }
    
    public Vector3 GetDirection (float t) 
    {
        return GetPoint(t).normalized;
    }
    
    public Vector3 GetPoint (float t) 
    {
        int i;
        
        if (t >= 1f) 
        {
            t = 1f;
            i = points.Count - 4;
        }
        else 
        {
            t = Mathf.Clamp01(t) * (points.Count - 1) / 3;
            i = (int)t;
            t -= i;
            i *= 3;
        }
        
        return BezierSplines.Bezier.EvaluateQubic(
            points[i], points[i + 1], points[i + 2], points[i + 3], t);
    }
    
    public Vector3[] CalculateEvenlySpacedPoints(float spacing, float resolution = 1)
    {
        List<Vector3> spacedPoints = new List<Vector3>();
        spacedPoints.Add(points[0]);
        Vector3 previousPoint = points[0];

        float distanceSiceLastEvenPoint = 0;

        for (int segmentIndex = 0; segmentIndex < NumSegments; segmentIndex++)
        {
            Vector3[] p = GetPointsInSegment(segmentIndex);
            
            float controlNetLenght = Vector3.Distance(p[0], p[1]) + Vector3.Distance(p[1], p[2]) + Vector3.Distance(p[2], p[3]);
            float estimatedCurveLenght = Vector3.Distance(p[0], p[3]) + controlNetLenght / 2f;
            int divisions = Mathf.CeilToInt(estimatedCurveLenght * resolution * 10);
            
            float t = 0;

            while (t <= 1)
            {
                t += 1f/divisions;
                Vector3 pointOnCurve = BezierSplines.Bezier.EvaluateQubic(p[0], p[1], p[2], p[3], t);
                distanceSiceLastEvenPoint += Vector3.Distance(previousPoint, pointOnCurve);

                while (distanceSiceLastEvenPoint >= spacing)
                {
                    float overShootDistance = distanceSiceLastEvenPoint - spacing;
                    
                    Vector3 newEvenlySpacedPoint =
                        pointOnCurve + (previousPoint - pointOnCurve).normalized * overShootDistance;
                    
                    spacedPoints.Add(newEvenlySpacedPoint);
                    
                    distanceSiceLastEvenPoint = overShootDistance;
                    previousPoint = newEvenlySpacedPoint;
                }
                
                previousPoint = pointOnCurve;
            }
        }

        spacedPoints.Add(!isClosed ? points[^1] : points[0]);

        return spacedPoints.ToArray();
    }

    void AutoSetAllAffectedControlPoints(int updatedAnchorIndex)
    {
        for (int i = updatedAnchorIndex - 3; i < updatedAnchorIndex + 3; i+=3)
        {
            if (i >= 0 && i < points.Count || isClosed)
            {
                AutoSetAnchorControlPoints(LoopIndex(i));
            }
        }
        
        AutoSetStartAndEndControls();
    }
    
    void AutoSetAllControlPoints()
    {
        for (int i = 0; i < points.Count; i+=3)
        {
            AutoSetAnchorControlPoints(i);
        }
        
        AutoSetStartAndEndControls();
    }
    
    void AutoSetAnchorControlPoints(int anchorIndex)
    {
        Vector3 anchorPos = points[anchorIndex];
        Vector3 dir = Vector3.zero;
        float[] neighbourDistances = new float[2];

        if (anchorIndex - 3 >= 0 || isClosed)
        {
            Vector3 offset = points[LoopIndex(anchorIndex - 3)] - anchorPos;
            dir += offset.normalized;
            neighbourDistances[0] = offset.magnitude;
        }
        
        if (anchorIndex + 3 >= 0 || isClosed)
        {
            Vector3 offset = points[LoopIndex(anchorIndex + 3)] - anchorPos;
            dir -= offset.normalized;
            neighbourDistances[1] = -offset.magnitude;
        }
        
        dir.Normalize();

        for (int i = 0; i < 2; i++)
        {
            int controlIndex = anchorIndex + i * 2 - 1;
            if (controlIndex >= 0 && controlIndex < points.Count || isClosed)
            {
                points[LoopIndex(controlIndex)] = anchorPos + dir * (neighbourDistances[i] * 0.5f);
            }
        }
    }

    void AutoSetStartAndEndControls()
    {
        if (!isClosed)
        {
            points[1] = (points[0] + points[2]) * 0.5f;
            points[^2] = (points[^1] + points[^3]) * 0.5f;
        }
    }
    
    int LoopIndex(int i)
    {
        return (i + points.Count) % points.Count;
    }
}
