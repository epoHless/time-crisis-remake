using System;
using UnityEditor;
using UnityEngine;

namespace Entities
{
    [CustomEditor(typeof(Enemy))]
    public class EnemyEditor : Editor
    {
        private Enemy script;

        private int behaviour;
        
        public override void OnInspectorGUI()
        {
            script ??= (Enemy)target;
            base.OnInspectorGUI();

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Add Behaviour"))
            {
                EBehaviour behaviourType = (EBehaviour)behaviour;
                var newBehaviour = (behaviourType) switch
                {
                    EBehaviour.MOVEMENT => new Movement(),
                    EBehaviour.SHOOTING => new Movement(), //todo Switch to shooting class
                    _ => throw new ArgumentOutOfRangeException()
                };
                
                script.Behaviours.Add(newBehaviour);
            }
            
            behaviour = EditorGUILayout.Popup("", behaviour, new[] { "Movement", "Shooting" }, GUILayout.Width(100));
            GUILayout.EndHorizontal();

        }
    }
}
