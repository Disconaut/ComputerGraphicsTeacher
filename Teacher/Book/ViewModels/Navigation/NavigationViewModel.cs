using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CGTeacherShared.Annotations;
using CGTeacherShared.Book;

namespace Teacher.Book.ViewModels.Navigation
{
    public class NavigationViewModel: INotifyPropertyChanged
    {
        private string _selectedPage;
        private readonly BookLoader _bookLoader;

        public NavigationViewModel()
        {
            _bookLoader = BookLoader.GetInstance();
            _selectedPage = BookPages.FirstOrDefault();
        }

        public Stream GetPageStream(string name) =>
            _bookLoader.GetPageStream(name);


        public ICollection<string> BookPages => _bookLoader.BookPagesName;

        public string SelectedPage
        {
            get => _selectedPage;
            set
            {
                if (_selectedPage != value)
                {
                    _selectedPage = value;
                    OnPropertyChanged(nameof(SelectedPage));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
