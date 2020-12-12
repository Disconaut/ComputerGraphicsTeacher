using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CGTeacherShared.Fractals.Abstract;
using CGTeacherShared.Fractals.Exceptions;

namespace CGTeacherShared.Fractals
{
    public class FractalParametersSet : IFractalParametersSet
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

        public void AddValue(string name, Type type, object value)
        {
            var parameter = new FractalParameter(name, type, value);
            _parameters.Add(parameter);
        }

        public void SetValue(string name, object value)
        {
            var parameter = _parameters.First(x => x.Name == name);
            if(value.GetType() != parameter.Type)
                throw new ArgumentException("Invalid type of argument", nameof(value));
            parameter.Value = value;
        }

        public object GetValue(string name)
        {
            return _parameters.FirstOrDefault(x => x.Name == name)?.Value ?? throw new ParameterNotFoundException(name);
        }

        public object GetValue(string name, object defaultValue)
        {
            return _parameters.FirstOrDefault(x => x.Name == name)?.Value ?? defaultValue;
        }

        public T GetValue<T>(string name)
        {
            try
            {
                return (T)GetValue(name);
            }
            catch (InvalidCastException)
            {
                throw new ParameterNotFoundException(name);
            }
        }

        public T GetValue<T>(string name, T defaultValue)
        {
            try
            {
                return (T)GetValue(name);
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
