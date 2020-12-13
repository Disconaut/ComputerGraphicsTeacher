using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teacher.Attributes
{
    class UIElementAttribute: Attribute
    {
        public Type ElementType { get; }

        public string Name { get; set; }  

        public UIElementAttribute(Type elementType)
        {
            ElementType = elementType;
        }
    }
}
