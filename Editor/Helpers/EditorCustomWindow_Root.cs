//Copyright 2025 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using UnityEngine.UIElements;

namespace DGGLocalization.Editor.Helpers
{
    public abstract partial class EditorCustomWindow<T>
    {
        /// <summary>
        /// Used to customize the Root Scroll View space of a window.
        /// </summary>
        public virtual ScrollerVisibility GetHorizontalRootVisibility() => ScrollerVisibility.Hidden;
        /// <summary>
        /// Used to customize the Root Scroll View space of a window.
        /// </summary>
        public virtual ScrollerVisibility GetVerticalRootVisibility() => ScrollerVisibility.Auto;
    }
}