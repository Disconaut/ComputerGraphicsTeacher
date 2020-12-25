using CGTeacherShared.AfinnisTransformations;
using CGTeacherShared.Shared.Vector;

namespace Teacher.ViewModels.AffinisTransformations
{
    public class PolygonViewModel : ShapeViewModel
    {
        public PolygonViewModel(IPolygon shape) : base(shape)
        {
        }

        public ObservableVector2[] Points => (Shape as IPolygon)?.Points;

        public ObservableVector2[] TransformedPoints => (TransformedShape as IPolygon)?.Points;
    }
}