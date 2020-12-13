using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CGTeacherShared.AfinnisTransformations;
using Teacher.Annotations;
using static System.Single;

namespace Teacher.ViewModels.AffinisTransformations
{
    public class TransformationViewModel: INotifyPropertyChanged
    {
        public TransformationViewModel(Transformation transformation)
        {
            Transformation = transformation;
        }

        public Transformation Transformation { get; }

        public float OffsetX
        {
            get => Transformation.OffsetX;
            set
            {
                if (Math.Abs(Transformation.OffsetX - value) > Epsilon)
                {
                    Transformation.OffsetX = value;
                    OnPropertyChanged(nameof(OffsetX));
                }
            }
        }
        
        public float OffsetY
        {
            get => Transformation.OffsetY;
            set
            {
                if (Math.Abs(Transformation.OffsetY - value) > Epsilon)
                {
                    Transformation.OffsetY = value;
                    OnPropertyChanged(nameof(OffsetY));
                }
            }
        }
        
        public float WidthScale
        {
            get => Transformation.WidthScale;
            set
            {
                if (Math.Abs(Transformation.WidthScale - value) > Epsilon)
                {
                    Transformation.WidthScale = value;
                    OnPropertyChanged(nameof(WidthScale));
                }
            }
        }
        
        public float HeightScale
        {
            get => Transformation.HeightScale;
            set
            {
                if (Math.Abs(Transformation.HeightScale - value) > Epsilon)
                {
                    Transformation.HeightScale = value;
                    OnPropertyChanged(nameof(HeightScale));
                }
            }
        }

        public float RotateAngle
        {
            get => Transformation.RotateAngle;
            set
            {
                if (Math.Abs(Transformation.RotateAngle - value) > Epsilon)
                {
                    Transformation.RotateAngle = value;
                    OnPropertyChanged(nameof(RotateAngle));
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
