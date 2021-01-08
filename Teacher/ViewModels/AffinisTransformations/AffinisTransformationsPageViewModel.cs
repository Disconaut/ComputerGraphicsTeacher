using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CGTeacherShared.AfinnisTransformations;
using CGTeacherShared.Annotations;
using CGTeacherShared.Shared.Vector;
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
            foreach (var point in Polygon.Points)
            {
                point.PropertyChanged += (sender, args) =>
                {
                    Polygon.Transform(Transformation);
                };
            }

            Transformation.PropertyChanged += (sender, args) =>
            {
                Polygon.Transform(Transformation);
            };

            Trapezes = new ObservableCollection<KeyValuePair<string, IPolygon>>
            {
                new KeyValuePair<string, IPolygon>(
                    "Обернена трапеція",
                    new Trapeze(new []
                {
                    new ObservableVector2(-100, -100),
                    new ObservableVector2(200, -100),
                    new ObservableVector2(200, 100),
                    new ObservableVector2(-50, 100)
                })),
                new KeyValuePair<string, IPolygon>(
                    "Звичайна трапеція",
                    new Trapeze(new []
                {
                    new ObservableVector2(-50, -100), 
                    new ObservableVector2(200, -100),
                    new ObservableVector2(200, 100),
                    new ObservableVector2(-100, 100)
                }))
            };
        }

        public PolygonViewModel Polygon { get; }

        public TransformationViewModel Transformation { get; }

        public ObservableCollection<KeyValuePair<string, IPolygon>> Trapezes { get; }

        public CanvasGeometry DrawShape(ICanvasResourceCreator canvasResourceCreator) =>
            Polygon.DrawShape(canvasResourceCreator);

        public void SelectTrapeze(IPolygon trapeze)
        {
            Polygon.SetPoints(trapeze.Points);
        }
    }
}
