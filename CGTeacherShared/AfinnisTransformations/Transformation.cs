﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGTeacherShared.AfinnisTransformations
{
    public class Transformation
    {
        public float OffsetX { get; set; }
        public float OffsetY { get; set; }
        public float WidthScale { get; set; } = 1;
        public float HeightScale { get; set; } = 1;
        public float RotateAngle { get; set; }
    }
}