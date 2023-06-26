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

                Behaviour newBehaviour;
                
                switch ((behaviourType))
                {
                    case EBehaviour.MOVEMENT:
                        newBehaviour = new Movement();
                        break;
                    case EBehaviour.SHOOTING:
                        newBehaviour = new Shooting();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                script.Behaviours.Add(newBehaviour);
            }
            
            behaviour = EditorGUILayout.Popup("", behaviour, new[] { "Movement", "Shooting" }, GUILayout.Width(100));
            GUILayout.EndHorizontal();
        }
    }
}
