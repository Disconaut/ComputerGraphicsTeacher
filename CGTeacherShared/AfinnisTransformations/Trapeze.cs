using System.Linq;
using System.Numerics;
using Windows.UI.Xaml.Controls;
using CGTeacherShared.Shared.Vector;
using Microsoft.Graphics.Canvas;
using Microsoft.Graphics.Canvas.Geometry;

namespace CGTeacherShared.AfinnisTransformations
{
    public class Trapeze: IPolygon
    {
        public Trapeze()
        {
            Points = new []
            {
                new ObservableVector2(),
                new ObservableVector2(),
                new ObservableVector2(),
                new ObservableVector2() 
            };
        }

        public IShape Transform(Transformation transformation)
        {
            throw new System.NotImplementedException();
        }

        public CanvasGeometry DrawShape(ICanvasResourceCreator canvasResourceCreator)
        {
            return CanvasGeometry.CreatePolygon(canvasResourceCreator, Points.Select(x => (Vector2)x).ToArray());
        }

        public ObservableVector2[] Points { get; }
    }
}
