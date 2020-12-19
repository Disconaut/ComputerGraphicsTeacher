using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CGTeacherShared.AfinnisTransformations;
using CGTeacherShared.Annotations;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;

namespace Teacher.ViewModels.AffinisTransformations
{
    public class AffinisTransformationsPageViewModel
    {
        public AffinisTransformationsPageViewModel()
        {
            Polygon = new PolygonViewModel(new Trapeze());
            Transformation = new TransformationViewModel(new Transformation());
            Transformation.PropertyChanged += (sender, args) =>
            {
                Polygon.Transform(Transformation);
            };
        }

        public PolygonViewModel Polygon { get; }

        public TransformationViewModel Transformation { get; }

        public CanvasGeometry DrawShape(ICanvasResourceCreator canvasResourceCreator) =>
            Polygon.DrawShape(canvasResourceCreator);
    }
}
