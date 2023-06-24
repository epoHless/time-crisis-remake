using UnityEditor;
using UnityEngine;

namespace BezierSplines
{
    [CustomEditor(typeof(PathCreator))]

    public class PathEditor : Editor
    {
        private PathCreator creator;
        private Path Path => creator.path;

        private const float segmentSelectDistanceThreshold = 0.1f;
        private int selectedSegmentIndex = -1;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            EditorGUI.BeginChangeCheck();

            bool autoSetControlPoints = GUILayout.Toggle(Path.AutoSetControlPoints, "Auto Set Control Points");
            bool isClosed = GUILayout.Toggle(Path.IsClosed, "Closed");

            if (GUILayout.Button("Create New"))
            {
                Undo.RecordObject(creator, "Create New");
                creator.CreatePath();
            }

            if (isClosed != Path.IsClosed)
            {
                Undo.RecordObject(creator, "Toggle Closed");
                Path.IsClosed = isClosed;
            }

            if (autoSetControlPoints != Path.AutoSetControlPoints)
            {
                Undo.RecordObject(creator, "Toggle Auto Set Controls");
                Path.AutoSetControlPoints = autoSetControlPoints;
            }

            if (EditorGUI.EndChangeCheck())
            {
                SceneView.RepaintAll();
            }
        }

        void Input()
        {
            Event guiEvent = Event.current;

            Vector3 mousePos = HandleUtility.GUIPointToWorldRay(guiEvent.mousePosition).origin;
            mousePos = new Vector3(mousePos.x, 0, mousePos.z);

            if (guiEvent.type == EventType.MouseDown && guiEvent.button == 0 && guiEvent.shift)
            {
                if (selectedSegmentIndex != -1)
                {
                    Undo.RecordObject(creator, "Split Segment");
                    Path.SplitSegment(mousePos, selectedSegmentIndex);
                }
                else if (!Path.IsClosed)
                {
                    Undo.RecordObject(creator, "Added Segment");
                    var pos = mousePos;
                    pos.y = 0;
                    Path.AddSegment(pos);
                }
            }

            if (guiEvent.type == EventType.MouseDown && guiEvent.button == 1)
            {
                float minDistanceToAnchor = creator.anchorDiameter * 0.5f;
                int closestAnchorIndex = -1;

                for (int i = 0; i < Path.NumPoints; i += 3)
                {
                    float distance = Vector3.Distance(mousePos, Path[i]);

                    if (distance < minDistanceToAnchor)
                    {
                        minDistanceToAnchor = distance;
                        closestAnchorIndex = i;
                    }
                }

                if (closestAnchorIndex != -1)
                {
                    Undo.RecordObject(creator, "Deleted Segment");
                    Path.DeleteSegment(closestAnchorIndex);
                }
            }

            if (guiEvent.type == EventType.MouseMove)
            {
                float minDistanceToSegment = segmentSelectDistanceThreshold;
                int newSelectedSegmentIndex = -1;

                for (int i = 0; i < Path.NumSegments; i++)
                {
                    Vector3[] points = Path.GetPointsInSegment(i);
                    float distance =
                        HandleUtility.DistancePointBezier(mousePos, points[0], points[3], points[1], points[2]);

                    if (distance < minDistanceToSegment)
                    {
                        minDistanceToSegment = distance;
                        newSelectedSegmentIndex = i;
                    }
                }

                if (newSelectedSegmentIndex != selectedSegmentIndex)
                {
                    selectedSegmentIndex = newSelectedSegmentIndex;
                    HandleUtility.Repaint();
                }
            }

            HandleUtility.AddDefaultControl(0);
        }

        private void OnSceneGUI()
        {
            Input();
            Draw();
        }

        void Draw()
        {
            for (int i = 0; i < Path.NumSegments; i++)
            {
                Vector3[] points = Path.GetPointsInSegment(i);

                if (creator.displayControlPoint)
                {
                    Handles.color = Color.black;
                    Handles.DrawLine(points[1], points[0]);
                    Handles.DrawLine(points[2], points[3]);
                }

                Color segmentColor = (i == selectedSegmentIndex && Event.current.shift)
                    ? creator.selectedSegmentColor
                    : creator.segmentColor;
                Handles.DrawBezier(points[0], points[3], points[1], points[2], segmentColor, null, 2);
            }

            Handles.color = Color.red;

            for (int i = 0; i < Path.NumPoints; i++)
            {
                if (i % 3 == 0 || creator.displayControlPoint)
                {
                    Handles.color = (i % 3) == 0 ? creator.anchorColor : creator.controlColor;
                    float handleSize = (i % 3 == 0) ? creator.anchorDiameter : creator.controlDiameter;

                    Vector3 newPos = Handles.FreeMoveHandle(Path[i], Quaternion.identity, handleSize, Vector2.zero,
                        Handles.CylinderHandleCap);

                    if (Path[i] != newPos)
                    {
                        Undo.RecordObject(creator, "Moved Point");
                        Path.MovePoint(i, newPos);
                    }
                }
            }
        }

        private void OnEnable()
        {
            creator = (PathCreator)target;

            if (creator.path == null)
            {
                creator.CreatePath();
            }
        }
    }
}
