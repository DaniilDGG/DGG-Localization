using DGGLocalization.Unity.Attributes;
using UnityEditor;
using UnityEngine;

namespace DGGLocalization.Editor.GUI
{
    [CustomPropertyDrawer(typeof(LocalizationStringAttribute))]
    public class LocalizationButtonDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property, label);

            if (!UnityEngine.GUI.Button(new Rect(position.x, position.y + EditorGUI.GetPropertyHeight(property) + 2, position.width, 20), "Open Localization")) return;
            
            var localizationKey = property.stringValue;
            
            LocalizationEditor.OpenLocalizationSetting(localizationKey);
        }
    }
}