using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CGTeacherShared.Annotations;

namespace Teacher.Book.ViewModels.BookPage
{
    public class BookPageViewModel: INotifyPropertyChanged
    {
        private StreamReader _bookPageStream;

        public BookPageViewModel(){}

        public BookPageViewModel(Stream bookPageStream)
        {
            _bookPageStream = new StreamReader(bookPageStream);
        }

        public string BookPageText => _bookPageStream?.ReadToEnd();

        public void SetNewStream(Stream stream)
        {
            _bookPageStream = new StreamReader(stream);
            OnPropertyChanged(nameof(BookPageText));
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
