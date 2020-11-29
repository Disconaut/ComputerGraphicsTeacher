using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CGTeacherShared.Fractals;
using CGTeacherShared.Fractals.Abstract;

namespace Teacher.ViewModels.Fractals
{
    public class FractalsPageViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<FractalViewModel> Fractals { get; }

        private FractalViewModel _currentFractal;
        public FractalViewModel CurrentFractal
        {
            get => _currentFractal;
            set
            {
                if (_currentFractal == value) return;

                _currentFractal = value;
                OnPropertyChanged(nameof(CurrentFractal));
            }
        }

        public FractalsPageViewModel()
        {
            Fractals = new ObservableCollection<FractalViewModel>
            {
                new FractalViewModel(new HHDragonFractal())
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
