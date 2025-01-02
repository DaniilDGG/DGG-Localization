using DGGLocalization.Unity.Base;
using UnityEditor;
using UnityEngine;

namespace DGGLocalization.Editor.GUI
{
    [CustomEditor(typeof(LocalizationInfo))]
    public class LocalizationInfoGUI : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var info = (LocalizationInfo)target;
            
            if (GUILayout.Button("Open Localization")) LocalizationEditor.OpenLocalizationSetting(info.LocalizationCode);
        }
    }
}
