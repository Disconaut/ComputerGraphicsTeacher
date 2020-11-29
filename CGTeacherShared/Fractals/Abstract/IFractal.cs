﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGTeacherShared.Fractals.EventArgs;

namespace CGTeacherShared.Fractals.Abstract
{
    public interface IFractal
    {
        string Name { get; }

        IFractalParametersSet Parameters { get; }

        event EventHandler<RenderCompleteEventArgs> RenderComplete;

        Task BeginRenderAsync(float x, float y, float fractalWidth, float fractalHeight, float width,
            float height);
    }
}
