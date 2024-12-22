using UnityEngine.UIElements;

namespace DGGLocalization.Editor.Helpers
{
    public partial class EditorCustomWindow<T>
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