using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGTeacherShared.Fractals.Abstract
{
    interface IFractalParameter
    {
        string Name { get; }

        object Value { get; set; }
    }
}
