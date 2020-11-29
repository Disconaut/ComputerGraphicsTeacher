using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGTeacherShared.Fractals.Abstract;

namespace Teacher.ViewModels.Fractals
{
    public class FractalViewModel
    {
        private IFractal _fractal;

        public FractalViewModel(IFractal fractal)
        {
            _fractal = fractal;
            FractalParameters = new List<FractalParameterViewModel>(
                _fractal.Parameters.Select(x => new FractalParameterViewModel(x)));
        }

        public IList<FractalParameterViewModel> FractalParameters { get; }
    }
}
