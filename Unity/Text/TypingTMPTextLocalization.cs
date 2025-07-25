//Copyright 2023 Daniil Glagolev
//Licensed under the Apache License, Version 2.0

using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DGGLocalization.Unity.Base;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

namespace DGGLocalization.Unity.Text
{
    [RequireComponent(typeof(LocalizationInfo))]
    public sealed class TypingTMPTextLocalization : MonoBehaviour
    {
        #region Fields

        [SerializeField] private TMP_Text _tmp;
        
        [Space(30f)]
        [SerializeField, Range(MinimumCharTypingTime, MaximumCharTypingTime)] private float _charTypingTime = 0.16f;
        
        private LocalizationInfo _info;
        private CancellationTokenSource _source;
        
        private string _typingText;
        private int _symbolsCount;
        
        private bool _requiredFinish;
        private bool _isTyping;

        #region Properties

        public bool IsTyping => _isTyping;
        public bool IsFull => !_isTyping;

        #endregion

        #region Constants

        public const float MinimumCharTypingTime = 0.01f;
        public const float MaximumCharTypingTime = 1f;

        #endregion

        #region Events

        public UnityAction OnStartTyping;
        public UnityAction OnEndTyping;

        #endregion

        #endregion

        #region MonoBehavior

        private void OnValidate()
        {
            if (!_tmp) _tmp = GetComponent<TMP_Text>();
        }

        private void Awake()
        {
            _info = GetComponent<LocalizationInfo>();
            
            _info.OnSwitchLanguage += Typing;
            Typing(_info.GetLocalization());
        }

        private void OnDestroy() => Dispose();

        #endregion

        #region Public Members

        public void SetCharTypingTime(float value, bool isRedraw = false)
        {
            value = value switch
            {
                > MaximumCharTypingTime => MaximumCharTypingTime,
                < MinimumCharTypingTime => MinimumCharTypingTime,
                _ => value
            };

            _charTypingTime = value;

            if (!isRedraw) return;

            if (IsTyping) _symbolsCount = 0;
            else Typing(_typingText);
        }

        public void StopTyping() => _requiredFinish = true;
        
        #endregion

        private void Dispose()
        {
            if (_source == null) return;
            
            _source.Cancel();
            _source.Dispose();
            
            _source = null;
        }
        
        private async void Typing(string text)
        {
            _typingText = text;
            _requiredFinish = false;

            Dispose();
            
            if (string.IsNullOrEmpty(text)) return;
            
            _source = new CancellationTokenSource();
            
            var token = _source.Token;

            _symbolsCount = 0;
            var typingText = "";
            var index = -1;
            _isTyping = true;
            
            OnStartTyping?.Invoke();
            
            while (typingText.Length < text.Length && text == _typingText && !_requiredFinish)
            {
                index++;
                
                if (_tmp.richText && index != text.Length && text[index] == '<')
                {
                    var tagEnd = text.IndexOf('>', _symbolsCount);

                    if (tagEnd == -1) continue;

                    _symbolsCount = tagEnd + 1;
             
                    index = tagEnd;
                    typingText = text[.._symbolsCount];
                    
                    _tmp.text = typingText;
                    _symbolsCount++;
                    continue;
                }

                typingText = text[.._symbolsCount];
                _tmp.text = typingText;

                await UniTask.Delay(TimeSpan.FromSeconds(_charTypingTime));
                
                if (token.IsCancellationRequested) return;

                _symbolsCount++;
            }
            
            if (token.IsCancellationRequested) return;

            if (_typingText != text) return;
            
            _tmp.text = text;
            _symbolsCount = text.Length;
            
            _requiredFinish = false;
            _isTyping = false;
            
            OnEndTyping?.Invoke();
        }
    }
}
