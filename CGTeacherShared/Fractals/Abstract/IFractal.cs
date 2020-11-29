using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGTeacherShared.Fractals.EventArgs;

namespace CGTeacherShared.Fractals.Abstract
{
    interface IFractal
    {
        string Name { get; }

        IFractalParametersSet Parameters { get; }

        event EventHandler<RenderCompleteEventArgs> RenderComplete;

        Task BeginRenderAsync(double x, double y, double fractalWidth, double fractalHeight, double width,
            double height);
    }
}
