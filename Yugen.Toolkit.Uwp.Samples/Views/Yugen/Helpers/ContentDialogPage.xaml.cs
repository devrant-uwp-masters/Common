﻿using Microsoft.Toolkit.Mvvm.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Yugen.Toolkit.Uwp.Helpers;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Yugen.Toolkit.Uwp.Samples.Views.Helpers
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ContentDialogPage : Page
    {
        public ContentDialogPage()
        {
            this.InitializeComponent();
        }

        private void Command(string text)
        {
            System.Diagnostics.Debug.WriteLine(text);
        }

        private async void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            await ContentDialogHelper.Confirm("content", "title", "Close", new RelayCommand(() => Command("Yes")), "Yes");
        }

        private async void ConfirmDeleteButton_Click(object sender, RoutedEventArgs e)
        {

            await ContentDialogHelper.ConfirmDelete("content", "title", "Close", new RelayCommand(() => Command("Yes")), "Yes", new RelayCommand(() => Command("No")), "No");
        }

        private async void AlertButton_Click(object sender, RoutedEventArgs e)
        {

            await ContentDialogHelper.Alert("content", "", "Close");
        }
    }
}