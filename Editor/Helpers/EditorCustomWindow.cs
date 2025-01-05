//Copyright 2023 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace DGGLocalization.Editor.Helpers
{
    public abstract partial class EditorCustomWindow<T> : EditorWindow where T : EditorCustomWindow<T>
    {
        #region Fields
        
        private static T _editorCustomWindow;

        

        #endregion

        #region Properties

        /// <summary>
        /// Displayed window name in the editor.
        /// </summary>
        public abstract string DisplayName { get; }
        
        protected VisualElement Root { get; private set; }

        #endregion

        #region Unity Methods

        protected virtual void OnEnable()
        {
            Root = new ScrollView()
            {
                style =
                {
                    flexGrow = 1, 
                },
                horizontalScrollerVisibility = GetHorizontalRootVisibility(),
                verticalScrollerVisibility = GetVerticalRootVisibility()
            };
            
            rootVisualElement.Add(Root);
        }

        #endregion
        
        /// <summary>
        /// Show custom editor window.
        /// </summary>
        public static T ShowWindow()
        {
            if (_editorCustomWindow) return _editorCustomWindow;
            
            _editorCustomWindow = GetWindow<T>();
            _editorCustomWindow.titleContent = new GUIContent(_editorCustomWindow.DisplayName);

            return _editorCustomWindow;
        }
    }
}