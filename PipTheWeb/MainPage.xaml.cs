using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PipTheWeb
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        private async void OpenWebsiteButton_Click(object sender, RoutedEventArgs e)
        {
            var contentDialog = new ContentDialog
            {
                Content = new TextBox { VerticalAlignment = VerticalAlignment.Center },
                Title = "Enter a web address",
                PrimaryButtonText = "Ok",
                CloseButtonText = "Cancel"
            };

            var result = await contentDialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                var url = (contentDialog.Content as TextBox).Text;

                var uri = GetUriFromInput(url);

                if (uri == null)
                {
                    await new MessageDialog("Please enter a valid address").ShowAsync();

                    return;
                }

                Window.Current.Content = new WebView { Source = uri };

                await ApplicationView.GetForCurrentView().TryEnterViewModeAsync(ApplicationViewMode.CompactOverlay);
            }
        }

        private Uri GetUriFromInput(string input, bool triedAppending = false)
        {
            try
            {
                return new Uri(input);
            }
            catch (Exception)
            {
                if (triedAppending) return null;

                return GetUriFromInput($"http://{input}", true);
            }
        }
    }
}
