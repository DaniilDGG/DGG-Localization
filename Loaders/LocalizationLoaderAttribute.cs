//Copyright 2025 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using System;
using UnityEngine.Scripting;

namespace DGGLocalization.Loaders
{
    [AttributeUsage(AttributeTargets.Class), Preserve]
    public class LocalizationLoaderAttribute : Attribute
    {
        #region Fields
        
        /// <summary>
        /// Whether it is necessary to execute the loader in Runtime mode.
        /// </summary>
        public bool RuntimeSupported { get; }
        /// <summary>
        /// Whether it is necessary to execute the loader in Editor mode.
        /// </summary>
        public bool EditorSupported { get; }
        
        #endregion
        
        /// <summary>
        /// For use in any mode.
        /// </summary>
        public LocalizationLoaderAttribute()
        {
            RuntimeSupported = true;
            EditorSupported = true;
        }

        /// <summary>
        /// To operate in specific modes.
        /// </summary>
        public LocalizationLoaderAttribute(bool runtimeSupported, bool editorSupported)
        {
            RuntimeSupported = runtimeSupported;
            EditorSupported = editorSupported;
        }
    }
}