//Copyright 2025 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using DGGLocalization.Config;
using UnityEditor;
using UnityEngine;

namespace DGGLocalization.Editor.GUI
{
    [CustomEditor(typeof(LocalizationProfile))]
    public class LocalizationProfileGUI : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Apply")) LocalizationEditor.Reboot();
        }
    }
}