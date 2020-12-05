using System;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;
using CGTeacherShared.Annotations;

namespace CGTeacherShared.Shared.Vector
{
    public class ObservableVector2 : INotifyPropertyChanged
    {
        private double? _x;
        private double? _y;

        public double? X
        {
            get => _x;
            set
            {
                if (Math.Abs(_x ?? 0f - value ?? 0f) > float.Epsilon)
                {
                    _x = value;
                    OnPropertyChanged(nameof(X));
                }
            }
        }

        public double? Y
        {
            get => _y;
            set
            {
                if (Math.Abs(_y ?? 0f - value ?? 0f) > float.Epsilon)
                {
                    _y = value;
                    OnPropertyChanged(nameof(Y));
                }
            }
        }

        public ObservableVector2()
        {
        }

        public ObservableVector2(float x, float y)
        {
            X = x;
            Y = y;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static explicit operator Vector2(ObservableVector2 obj)
        {
            return new Vector2(
                (float)(obj.X ?? 0.0), 
                (float)(obj.Y ?? 0.0));
        }
    }
}
