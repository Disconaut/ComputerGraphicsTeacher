using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.Appointments.DataProvider;
using CGTeacherShared.Shared.Vector;

namespace CGTeacherShared.AfinnisTransformations
{
    public class Transformation
    {
        private float _scale;
        public float OffsetX { get; set; }
        public float OffsetY { get; set; }
        public float WidthScale { get; set; }
        public float HeightScale { get; set; }
        public float RotateAngle { get; set; }
        public ObservableVector2 CenterOfRotation { get; set; }
        public ObservableVector2 CenterOfZoom { get; set; }
        public float XCenterOfRotation
        {
            get => (float) (CenterOfRotation.X ?? 0);
            set => CenterOfRotation.X = value;
        }
        public float YCenterOfRotation {
            get => (float)(CenterOfRotation.Y ?? 0);
            set => CenterOfRotation.Y = value;
        }
        public float XCenterOfZoom {
            get => (float)(CenterOfZoom.X ?? 0);
            set => CenterOfZoom.X = value;
        }
        public float YCenterOfZoom {
            get => (float)(CenterOfZoom.Y ?? 0);
            set => CenterOfZoom.Y = value;
        }
        public float Scale
        {
            get => _scale;
            set
            {
                _scale = value;
                WidthScale = value;
                HeightScale = value;
            }
        }

        public Transformation()
        {
            _scale = 1;
            WidthScale = 1;
            HeightScale = 1;
            CenterOfRotation = new ObservableVector2(0, 0);
            CenterOfZoom = new ObservableVector2(0, 0);
        }
    }
}
