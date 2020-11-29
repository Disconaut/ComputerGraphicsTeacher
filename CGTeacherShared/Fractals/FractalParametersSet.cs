using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGTeacherShared.Fractals.Abstract;

namespace CGTeacherShared.Fractals
{
    public class FractalParametersSet: IFractalParametersSet
    {
        private readonly ICollection<IFractalParameter> _parameters;

        public FractalParametersSet()
        {
            _parameters = new List<IFractalParameter>();
        }

        public bool HasValue(string name)
        {
            return _parameters.Any(x => x.Name == name);
        }

        public void AddValue(string name, object value)
        {
            var parameter = new FractalParameter(name, value);
            _parameters.Add(parameter);
        }

        public void SetValue(string name, object value)
        {
            _parameters.First(x => x.Name == name).Value = value;
        }

        public object GetValue(string name)
        {
            return _parameters.First(x => x.Name == name).Value;
        }

        public object GetValue(string name, object defaultValue)
        {
            return _parameters.FirstOrDefault(x => x.Name == name)?.Value ?? defaultValue;
        }

        public T GetValue<T>(string name)
        {
            return (T) GetValue(name);
        }

        public T GetValue<T>(string name, T defaultValue)
        {
            try
            {
                return (T) GetValue(name);
            }
            catch
            {
                return defaultValue;
            }
        }

        public IEnumerator<IFractalParameter> GetEnumerator()
        {
            return _parameters.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
