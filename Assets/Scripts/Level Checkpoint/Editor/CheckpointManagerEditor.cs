using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(CheckpointManager))]
public class CheckpointManagerEditor : Editor
{
    private CheckpointManager script;
    
    public override void OnInspectorGUI()
    {
        script ??= (CheckpointManager)target;
        
        base.OnInspectorGUI();

        if (GUILayout.Button("Set Control Points"))
        {
            script.Creator.SetY();
        }
    }
}
