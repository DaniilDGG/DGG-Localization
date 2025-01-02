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