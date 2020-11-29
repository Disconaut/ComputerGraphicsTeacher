using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGTeacherShared.Fractals.Abstract
{
    public interface IFractalParametersSet: IEnumerable<IFractalParameter>
    {
        bool HasValue(string name);

        void AddValue(string name, object value);

        void SetValue(string name, object value);

        object GetValue(string name);

        object GetValue(string name, object defaultValue);

        T GetValue<T>(string name);

        T GetValue<T>(string name, T defaultValue);
    }
}
