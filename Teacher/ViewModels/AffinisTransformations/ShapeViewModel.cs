using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGTeacherShared.AfinnisTransformations;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;

namespace Teacher.ViewModels.AffinisTransformations
{
    public abstract class ShapeViewModel
    {
        protected readonly IShape Shape;
        protected IShape TransformedShape;

        protected ShapeViewModel(IShape shape)
        {
            Shape = shape;
            TransformedShape = shape;
        }

        public CanvasGeometry DrawShape(ICanvasResourceCreator canvasResourceCreator) =>
            TransformedShape.DrawShape(canvasResourceCreator);

        public void Transform(TransformationViewModel transformation) =>
            TransformedShape = Shape.Transform(transformation.Transformation);
    }
}
