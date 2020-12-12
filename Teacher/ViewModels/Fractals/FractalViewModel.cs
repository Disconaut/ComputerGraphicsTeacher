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

        public void StartRendering(float x, float y, float widthScale, float heightScale, float width, float height, float dpi, float angle, CancellationToken cancellationToken)
        {
            _fractal.BeginRenderAsync(x, y, widthScale, heightScale, width, height, dpi,  angle, cancellationToken);
        }
    }
}
