﻿using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.Text;
using Windows.System;

namespace Yugen.Toolkit.Uwp.Handlers
{
    public class BarcodeHandler
    {
        private readonly Stopwatch _inputStopwatch = new Stopwatch();
        private readonly StringBuilder _builderBarcode = new StringBuilder();
        private readonly ILogger _logger;

        public bool IsHandledBarcode;
        public string BuilderBarcodeString => _builderBarcode.ToString();

        public static int BarcodeThreshold => 100;

        public BarcodeHandler(ILogger logger = null)
        {
            _logger = logger;

            if (!_inputStopwatch.IsRunning)
                _inputStopwatch.Start();
        }

        public void OnKeyUp(VirtualKey virtualKey, Action handleBarcode, Action<string> search, string query)
        {
            if (virtualKey == VirtualKey.Enter && IsThreshold())
            {
                IsHandledBarcode = true;

                handleBarcode();

                _builderBarcode.Clear();
            }
            else
            {
                IsHandledBarcode = false;

                if (!IsThreshold())
                    search(query);

                _logger?.LogDebug($"{GetType()} DEBUG KEY={virtualKey} - ELAPSED TIME={_inputStopwatch.ElapsedMilliseconds}ms - IsHandledBarcode={IsHandledBarcode} - IsThreshold={IsThreshold()}");

                _inputStopwatch.Restart();
            }
        }

        public void OnKeyDown(VirtualKey originalKey)
        {
            char pressedCharacter = (char)originalKey;

            if (char.IsLetterOrDigit(pressedCharacter) || char.IsWhiteSpace(pressedCharacter))
                _builderBarcode.Append(pressedCharacter);

            if (!_inputStopwatch.IsRunning)
                _inputStopwatch.Start();
        }

        public void OnTextChanged(Action<string> search, string query)
        {
            if (!IsHandledBarcode)
                _builderBarcode.Clear();

            if (!string.IsNullOrEmpty(query)) return;

            IsHandledBarcode = false;
            search(query);
        }

        public bool IsThreshold() => _inputStopwatch.ElapsedMilliseconds < BarcodeThreshold;
    }
}