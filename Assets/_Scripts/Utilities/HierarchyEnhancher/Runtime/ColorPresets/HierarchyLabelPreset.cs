﻿using System;
using UnityEditor;
using UnityEngine;

public class HierarchyLabelPreset : ScriptableObject
{
    public string identifier;

    public Texture icon;
    public FontStyle fontStyle;
    public TextAnchor alignment = TextAnchor.MiddleLeft;

    public Color textColor;
    public Color backgroundColor = new Color(0.2196079f, 0.2196079f, 0.2196079f, 1);

    public bool useCustomInactiveColors;
    public Color inactiveTextColor;
    public Color inactiveBackgroundColor = new Color(0.2196079f, 0.2196079f, 0.2196079f, 1);
}