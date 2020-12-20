using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Teacher.Book.ViewModels.BookPage;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Teacher.Book.Views.BookPage
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class BookPage : Page
    {
        public BookPageViewModel ViewModel { get; }

        public BookPage()
        {
            ViewModel = new BookPageViewModel();
            this.InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            if (!(e.Parameter is Stream stream))
            {
                var dialog = new MessageDialog("Sorry, page was not found", "Error!");
                await dialog.ShowAsync();
                return;
            }

            ViewModel.SetNewStream(stream);

            base.OnNavigatedTo(e);
        }
    }
}
