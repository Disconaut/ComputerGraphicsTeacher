using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.UI.Xaml;
using CGTeacherShared.Fractals.Abstract;

namespace Teacher.ViewModels.Fractals
{
    public class FractalParameterViewModel: INotifyPropertyChanged
    {
        private readonly IFractalParameter _fractalParameter;
        private readonly ResourceLoader _resourceLoader;

        public FractalParameterViewModel(IFractalParameter fractalParameter)
        {
            _fractalParameter = fractalParameter;
            _resourceLoader = ResourceLoader.GetForCurrentView();
        }

        public string Name
        {
            get
            {
                var name = _resourceLoader.GetString(_fractalParameter.Name);
                return string.IsNullOrEmpty(name) ? _fractalParameter.Name : name;
            }
        }

        public Type Type => _fractalParameter.Type;

        public object Value
        {
            get => _fractalParameter.Value;
            set
            {
                if (_fractalParameter.Value?.Equals(value) ?? false) return;
                _fractalParameter.Value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
