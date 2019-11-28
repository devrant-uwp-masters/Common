﻿using System.Collections.Generic;
using Windows.UI.Xaml.Controls;

namespace Yugen.Toolkit.Uwp.Controls.Validation
{
    public sealed class ValidatingFormControl : ItemsControl
    {
        private static List<BaseValidatingUserControl> _validatingControlsList = new List<BaseValidatingUserControl>();

        public ValidatingFormControl()
        {
            this.Loaded += ValidatingFormControl_Loaded;
        }

        private void ValidatingFormControl_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            foreach (var item in Items)
            {
                if (item is BaseValidatingUserControl validatingControl)
                    _validatingControlsList.Add(validatingControl);
            }
        }

        public bool FormIsValid()
        {
            var isValid = true;

            foreach (var validatingControl in _validatingControlsList)
            {
                validatingControl.ResetCustomValidation();

                if (!validatingControl.IsValid())
                    isValid = false;
            }

            return isValid;
        }
    }
}