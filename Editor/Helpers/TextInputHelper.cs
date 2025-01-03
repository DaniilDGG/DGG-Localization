//Copyright 2025 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using UnityEngine.UIElements;

namespace DGGLocalization.Editor.Helpers
{
    public static class TextInputHelper
    {
        public static TextField CreateTextInput(string text, string label, VisualElement root, bool multiline = false)
        {
            var textElement = new TextField(label, int.MaxValue, false, false, ' ')
            {
                value = text,
                multiline = multiline,
            };

            if (multiline)
            {
                var scrollView = new ScrollView
                {
                    style =
                    {
                        flexGrow = 1, 
                        maxHeight = 200,
                        overflow = Overflow.Hidden
                    },
                    horizontalScrollerVisibility = ScrollerVisibility.Auto,
                    verticalScrollerVisibility = ScrollerVisibility.Auto
                };

                textElement.style.whiteSpace = WhiteSpace.Normal;

                scrollView.Add(textElement);
                root.Add(scrollView);
            }
            else
            {
                root.Add(textElement);
            }

            return textElement;
        }
    }
}