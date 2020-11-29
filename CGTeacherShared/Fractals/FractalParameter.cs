using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGTeacherShared.Fractals.Abstract;

namespace CGTeacherShared.Fractals
{
    public class FractalParameter: IFractalParameter
    {
        public string Name { get; }
        public object Value { get; set; }
        public Type Type => Value?.GetType();

        public FractalParameter(string name, object value = null)
        {
            Name = name;
            Value = value;
        }
    }
}
