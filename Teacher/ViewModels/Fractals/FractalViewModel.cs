using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel.Resources;
using CGTeacherShared.Fractals.Abstract;
using CGTeacherShared.Fractals.EventArgs;
using Teacher.ViewModels.AffinisTransformations;

namespace Teacher.ViewModels.Fractals
{
    public class FractalViewModel
    {
        private readonly IFractal _fractal;
        private readonly ResourceLoader _resourceLoader;

        public FractalViewModel(IFractal fractal)
        {
            _fractal = fractal;
            _resourceLoader = ResourceLoader.GetForCurrentView();
            FractalParameters = new List<FractalParameterViewModel>(
                _fractal.Parameters.Select(x => new FractalParameterViewModel(x)));
        }

        public string Name
        {
            get
            {
                var name = _resourceLoader.GetString(_fractal.Name);
                return string.IsNullOrEmpty(name) ? _fractal.Name : name;
            }
        }

        public event EventHandler<RenderCompleteEventArgs> RenderComplete
        {
            add => _fractal.RenderComplete += value;
            remove => _fractal.RenderComplete -= value;
        }

        public IList<FractalParameterViewModel> FractalParameters { get; }

        public void StartRendering(TransformationViewModel transformation, float width, float height, float dpi, CancellationToken cancellationToken)
        {
            _fractal.BeginRenderAsync(transformation.Transformation, width, height, dpi, cancellationToken);
        }
    }
}
