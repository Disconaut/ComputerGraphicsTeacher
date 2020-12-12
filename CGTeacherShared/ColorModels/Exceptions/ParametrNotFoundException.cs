using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGTeacherShared.ColorModels.Exceptions
{
    public class ParameterNotFoundException : Exception
    {
        public ParameterNotFoundException(string parameterName) :
            base($"Parameter {parameterName} is invalid")
        {
        }
    }
}
