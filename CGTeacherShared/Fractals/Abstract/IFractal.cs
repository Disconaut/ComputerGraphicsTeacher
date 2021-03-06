﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CGTeacherShared.AfinnisTransformations;
using CGTeacherShared.Fractals.EventArgs;

namespace CGTeacherShared.Fractals.Abstract
{
    public interface IFractal
    {
        string Name { get; }

        IFractalParametersSet Parameters { get; }

        event EventHandler<RenderCompleteEventArgs> RenderComplete;

        Task BeginRenderAsync(Transformation transformation, float width,
            float height, float dpi, CancellationToken cancellationToken);
    }   
}
