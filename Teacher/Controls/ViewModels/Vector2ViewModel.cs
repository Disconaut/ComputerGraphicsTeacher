using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Teacher.Controls.ViewModels
{
    public class Vector2ViewModel: INotifyPropertyChanged
    {
        private Vector2 _vector;

        public Vector2ViewModel(Vector2 vector)
        {
            _vector = vector;
        }

        public float X
        {
            get => _vector.X;
            set
            {
                if (Math.Abs(_vector.X - value) > float.Epsilon)
                {
                    _vector.X = X;
                    OnPropertyChanged(nameof(X));
                }
            }
        }

        public float Y
        {
            get => _vector.Y;
            set
            {
                if (Math.Abs(_vector.Y - value) > float.Epsilon)
                {
                    _vector.Y = Y;
                    OnPropertyChanged(nameof(Y));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
