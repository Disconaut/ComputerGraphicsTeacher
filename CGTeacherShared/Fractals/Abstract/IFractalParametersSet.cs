using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CGTeacherShared.Fractals.Abstract
{
    interface IFractalParametersSet: IEnumerable, IEnumerator
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
