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
        public Type Type{ get; }

        public FractalParameter(string name, Type type = null, object value = null)
        {
            Name = name;
            Value = value;
            Type = type;
        }
    }
}
