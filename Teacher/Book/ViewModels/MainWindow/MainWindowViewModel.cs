using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Calls.Background;
using Windows.UI.Xaml.Controls;
using Teacher.Book.Views.Navigation;

namespace Teacher.Book.ViewModels.MainWindow
{
    public class MainWindowViewModel
    {
        public ObservableCollection<Frame> Pages { get; }

        public MainWindowViewModel()
        {
            Pages = new ObservableCollection<Frame>();
            CreateNewPage();
        }

        public void CreateNewPage()
        {
            var frame = new Frame();
            frame.Navigate(typeof(NavigationPage));
            Pages.Add(frame);
        }

        public void ClosePage(Frame frame)
        {
            Pages.Remove(frame);
        }
    }
}
