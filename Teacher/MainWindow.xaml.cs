using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Resources;
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
using Teacher.Views.Errors;
using NavigationView = Microsoft.UI.Xaml.Controls.NavigationView;
using NavigationViewItem = Microsoft.UI.Xaml.Controls.NavigationViewItem;
using NavigationViewBackRequestedEventArgs = Microsoft.UI.Xaml.Controls.NavigationViewBackRequestedEventArgs;
using NavigationViewSelectionChangedEventArgs = Microsoft.UI.Xaml.Controls.NavigationViewSelectionChangedEventArgs;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Teacher
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private readonly Type _underConstructionPageType;

        public MainPage()
        {
            this.InitializeComponent();
            _underConstructionPageType = typeof(UnderConstruction);

            MainNavigation.SelectedItem =
                MainNavigation.MenuItems
                    .OfType<Microsoft.UI.Xaml.Controls.NavigationViewItem>()
                    .First();

            MainNavigation.UpdateLayout();
        }

        private void MainNavigation_OnSelectionChanged(NavigationView sender,
            NavigationViewSelectionChangedEventArgs args)
        {
            const string viewsNamespace = "Teacher.Views";

            var selectedTab = args.SelectedItem as NavigationViewItem;
            var pageTypeFullName = selectedTab?.Tag as string;
            var pageType = Type.GetType($"{viewsNamespace}.{pageTypeFullName}");
            if (pageType?.IsSubclassOf(typeof(Page)) ?? false)
            {
                MainFrame.Navigate(pageType, pageTypeFullName);
            }
            else
            {
                MainFrame.Navigate(_underConstructionPageType, pageTypeFullName);
            }
        }

        private void MainNavigation_OnBackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
        {
            if (!MainFrame.CanGoBack) return;

            var backStackLength = MainFrame.BackStack.Count;
            var backPage = MainFrame.BackStack.Last();
            MainFrame.BackStack.RemoveAt(backStackLength - 1);

            var backPageTypeName = backPage.Parameter as string;

            MainNavigation.SelectedItem = MainNavigation.MenuItems
                .OfType<Microsoft.UI.Xaml.Controls.NavigationViewItem>()
                .First(x => x.Tag as string == backPageTypeName);
        }

        private async void BookButton_OnClick(object sender, RoutedEventArgs e)
        {
            var resourceLoader = ResourceLoader.GetForCurrentView();
            var bookDialog = new MessageDialog(resourceLoader.GetString("BookIsNotImplemented"));
            await bookDialog.ShowAsync();
        }
    }
}