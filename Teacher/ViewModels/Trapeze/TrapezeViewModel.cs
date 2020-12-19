using CGTeacherShared.Trapeze;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Teacher.ViewModels
{
    public class TrapezeViewModel:INotifyPropertyChanged
    {
        public Parameters Parameters { get; }

        public TrapezeViewModel(Parameters parameters)
        {
            Parameters = parameters;
        }

        public float A
        {
            get => Parameters.A;
            set
            {
                Parameters.A = value;
                OnPropertyChanged(nameof(A));
            }
        }

        public float B
        {
            get => Parameters.B;
            set
            {
                Parameters.B = value;
                OnPropertyChanged(nameof(B));
            }
        }
        public float C
        {
            get => Parameters.C;
            set
            {
                Parameters.C = value;
                OnPropertyChanged(nameof(C));
            }
        }
        public float D
        {
            get => Parameters.D;
            set
            {
                Parameters.D = value;
                OnPropertyChanged(nameof(D));
            }
        }

        public float WidthScale
        {
            get => Parameters.WidthScale;
            set
            {
                Parameters.WidthScale = value;
                OnPropertyChanged(nameof(WidthScale));
            }
        }

        public float HeightScale
        {
            get => Parameters.HeightScale;
            set
            {
                Parameters.HeightScale = value;
                OnPropertyChanged(nameof(HeightScale));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
