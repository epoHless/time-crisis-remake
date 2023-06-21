﻿using UnityEditor;
using UnityEngine;

namespace HierarchyEnhancer
{
    internal class LabelInfoEditorWindow : EditorWindow
    {
        public Label label;
        public int index;

        private static void ShowWindow()
        {
            var window = GetWindow<LabelInfoEditorWindow>();
            window.titleContent = new GUIContent($"Label Info");
            window.Show();
        }

        public void Open(Label _preset, int _index)
        {
            label = _preset;
            index = _index;

            string text = label.name.Split('_')[1];
            titleContent.text = $"{text} | {label.tooltips[index].tooltip} info";
            Show();
        }

        private void OnGUI()
        {
            label.tooltips[index].info = GUILayout.TextArea(label.tooltips[index].info, GUILayout.Height(maxSize.y),
                GUILayout.ExpandHeight(true));
        }
    }
}
