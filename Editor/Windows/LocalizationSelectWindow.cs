using DGGLocalization.Data;
using UnityEditor;
using UnityEngine;

namespace DGGLocalization.Editor.Windows
{
    public class LocalizationSelectWindow : EditorWindow
    {
        #region Fields

        private Localization _localization;

        #endregion
        
        // ReSharper disable PossibleLossOfFraction
        public static Localization Open()
        {
            switch (LocalizationEditor.Localizations.Count)
            {
                case 1:
                    return LocalizationEditor.Localizations[0].data;
                case 0:
                    Debug.LogError("No localization files found!");
                    return null;
            }
            
            var window = CreateInstance<LocalizationSelectWindow>();
            
            window.titleContent = new GUIContent("Localization select");
            window.position = new Rect(Screen.width / 2, Screen.height / 2, 250, 150);
            
            window.ShowModalUtility();

            return window._localization;
        }

        private void OnGUI()
        {
            GUILayout.Label("Choice localization package:", EditorStyles.boldLabel);

            GUILayout.Space(20);

            foreach (var localization in LocalizationEditor.Localizations)
            {
                if (!GUILayout.Button(localization.displayName)) continue;
                
                _localization = localization.data;
                    
                Close();
            }
        }
    }
}