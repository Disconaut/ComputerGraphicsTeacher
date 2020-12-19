using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CGTeacherShared.AfinnisTransformations;
using CGTeacherShared.Shared.Vector;
using Teacher.Annotations;
using static System.Single;

namespace Teacher.ViewModels.AffinisTransformations
{
    public class TransformationViewModel : INotifyPropertyChanged
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

        public ObservableVector2 CenterOfRotation
        {
            get => Transformation.CenterOfRotation;
            set
            {
                if (CenterOfRotation != value)
                {
                    Transformation.CenterOfRotation = value;
                    OnPropertyChanged(nameof(CenterOfRotation));
                }
            }
        }

        public ObservableVector2 CenterOfZoom
        {
            get => Transformation.CenterOfZoom;
            set
            {
                if (CenterOfZoom != value)
                {
                    Transformation.CenterOfZoom = value;
                    OnPropertyChanged(nameof(CenterOfZoom));
                }
            }
        }

        public float XCenterOfRotation
        {
            get => Transformation.XCenterOfRotation;
            set
            {
                if (Math.Abs(Transformation.XCenterOfRotation - value) > Epsilon)
                {
                    Transformation.XCenterOfRotation = value;
                    OnPropertyChanged(nameof(XCenterOfRotation));
                }
            }
        }

        public float YCenterOfRotation
        {
            get => Transformation.YCenterOfRotation;
            set
            {
                if (Math.Abs(Transformation.YCenterOfRotation - value) > Epsilon)
                {
                    Transformation.YCenterOfRotation = value;
                    OnPropertyChanged(nameof(YCenterOfRotation));
                }
            }
        }

        public float XCenterOfZoom
        {
            get => Transformation.XCenterOfZoom;
            set
            {
                if (Math.Abs(Transformation.XCenterOfZoom - value) > Epsilon)
                {
                    Transformation.XCenterOfZoom = value;
                    OnPropertyChanged(nameof(XCenterOfZoom));
                }
            }
        }

        public float YCenterOfZoom
        {
            get => Transformation.YCenterOfZoom;
            set
            {
                if (Math.Abs(Transformation.YCenterOfZoom - value) > Epsilon)
                {
                    Transformation.YCenterOfZoom = value;
                    OnPropertyChanged(nameof(YCenterOfZoom));
                }
            }
        }

        public float Scale
        {
            get => Transformation.Scale;
            set
            {
                if (Math.Abs(Transformation.Scale - value) > Epsilon)
                {
                    Transformation.Scale = value;
                    OnPropertyChanged(nameof(Scale));
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
