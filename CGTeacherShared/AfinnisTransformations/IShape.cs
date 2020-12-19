using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;

namespace CGTeacherShared.AfinnisTransformations
{
    public interface IShape
    {
        IShape Transform(Transformation transformation);
        CanvasGeometry DrawShape(ICanvasResourceCreator canvasResourceCreator);
    }
}
