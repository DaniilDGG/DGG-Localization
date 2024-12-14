//Copyright 2023 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using DGGLocalization.Unity.Base;
using UnityEngine;

namespace DGGLocalization.Unity.Text
{
    [RequireComponent(typeof(LocalizationInfo))]
    public sealed class TMPTextLocalization : AbstractTextLocalization
    {
        protected override string GetText(string original) => original;
    }
}
