using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Graphics.Canvas;

namespace CGTeacherShared.Fractals.EventArgs
{
    public class RenderCompleteEventArgs: System.EventArgs
    {
        public CanvasRenderTarget RenderTarget { get; set; }
    }
}
