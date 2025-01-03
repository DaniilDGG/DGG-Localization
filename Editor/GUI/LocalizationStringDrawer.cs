//Copyright 2025 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using DGGLocalization.Data;
using UnityEditor;
using UnityEngine;

namespace DGGLocalization.Editor.GUI
{
    [CustomPropertyDrawer(typeof(LocalizationString))]
    public class LocalizationStringDrawer : PropertyDrawer
    {
        private const float ButtonWidth = 120f; 
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var valueProperty = property.FindPropertyRelative("_value");
            
            if (valueProperty == null)
            {
                EditorGUI.EndProperty();
                return;
            }

            var textFieldRect = new Rect(position.x, position.y, position.width - ButtonWidth - 10, position.height);
            var buttonRect = new Rect(position.x + position.width - ButtonWidth, position.y, ButtonWidth, position.height);

            valueProperty.stringValue = EditorGUI.TextField(textFieldRect, valueProperty.stringValue);

            if (UnityEngine.GUI.Button(buttonRect, "Open Localization")) LocalizationEditor.OpenLocalizationSetting(valueProperty.stringValue);

            EditorGUI.EndProperty();
        }
    }
}