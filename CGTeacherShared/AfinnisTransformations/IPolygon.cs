using CGTeacherShared.Shared.Vector;
using Microsoft.Graphics.Canvas.Text;

namespace CGTeacherShared.AfinnisTransformations
{
    public interface IPolygon : IShape
    {
        ObservableVector2[] Points { get; }
    }
}