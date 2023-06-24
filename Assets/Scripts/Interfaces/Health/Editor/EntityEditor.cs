using Entities;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Entity), true)]
public class EntityEditor : Editor
{
    private IDamageable script;
    
    public override void OnInspectorGUI()
    {
        script ??= (IDamageable)target;
        base.OnInspectorGUI();

        if (GUILayout.Button("Take Damage"))
        {
            script.TakeDamage(1);
        }
    }
}
