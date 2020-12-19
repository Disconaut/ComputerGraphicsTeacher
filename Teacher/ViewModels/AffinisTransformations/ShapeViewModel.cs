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
        private IShape _transformedShape;

        protected ShapeViewModel(IShape shape)
        {
            Shape = shape;
            _transformedShape = shape;
        }

        public CanvasGeometry DrawShape(ICanvasResourceCreator canvasResourceCreator) =>
            _transformedShape.DrawShape(canvasResourceCreator);

        public void Transform(TransformationViewModel transformation) =>
            _transformedShape = Shape.Transform(transformation.Transformation);
    }
}
