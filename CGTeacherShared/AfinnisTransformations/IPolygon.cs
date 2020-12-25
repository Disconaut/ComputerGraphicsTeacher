using CGTeacherShared.Shared.Vector;

namespace CGTeacherShared.AfinnisTransformations
{
    public interface IPolygon : IShape
    {
        ObservableVector2[] Points { get; }
    }
}