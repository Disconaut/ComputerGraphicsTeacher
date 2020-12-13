
using CGTeacherShared.Annotations;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using Windows.UI;

namespace Teacher.ViewModels.Controls.AdvancedColorPicker
{
    public abstract class ColorModelViewModelBase : INotifyPropertyChanged
    {
        protected ResourceLoader ResourceLoader;

        protected ColorModelViewModelBase()
        {
            ResourceLoader = ResourceLoader.GetForCurrentView();
        }

        public abstract string Name { get; }

        public abstract Color RgbColor { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
