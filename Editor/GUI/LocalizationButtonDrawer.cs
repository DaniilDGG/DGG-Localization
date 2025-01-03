//Copyright 2025 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using DGGLocalization.Unity.Attributes;
using UnityEditor;
using UnityEngine;

namespace DGGLocalization.Editor.GUI
{
    [CustomPropertyDrawer(typeof(LocalizationStringAttribute))]
    public class LocalizationButtonDrawer : PropertyDrawer
    {
        #region Constants

        private const float ButtonHeight = 20f;
        private const float Padding = 2f;
        
        #endregion
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.PropertyField(position, property, label);
            
            var buttonRect = new Rect(position.x, position.y + EditorGUI.GetPropertyHeight(property) + Padding, position.width, ButtonHeight);

            if (!UnityEngine.GUI.Button(buttonRect, "Open Localization")) return;
            
            var localizationKey = property.stringValue;
            LocalizationEditor.OpenLocalizationSetting(localizationKey);
        }
        
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true) + ButtonHeight + Padding;
        }
    }
}