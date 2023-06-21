﻿using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;

public static class OpenAdditionalLockedInpsector
{
    public static void DisplayLockedInspector()
    {
        // Create new inspector window
        var inspectorWindow = ScriptableObject.CreateInstance(GetInspectorWindowType()) as EditorWindow;
        inspectorWindow.Show();

        // Lock new inspector window
        LockInspector(GetInspectorWindowType());
    }

    private static System.Type GetInspectorWindowType()
    {
        return typeof(UnityEditor.Editor).Assembly.GetType("UnityEditor.InspectorWindow");
    }

    private static void LockInspector(Type _obj)
    {
        var fields = _obj.GetFields(BindingFlags.Instance | BindingFlags.Public);

        foreach (var field in fields)
        {
            if(field.Name == "isLocked")
                field.SetValue(_obj, true);
        }
    }
}