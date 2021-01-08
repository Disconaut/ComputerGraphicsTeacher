using System.Collections.Generic;
using CGTeacherShared.Shared.Vector;

namespace CGTeacherShared.AfinnisTransformations
{
    public interface IPolygon : IShape
    {
        ObservableVector2[] Points { get; }

        void SetPoints(IEnumerable<ObservableVector2> points);
    }
}