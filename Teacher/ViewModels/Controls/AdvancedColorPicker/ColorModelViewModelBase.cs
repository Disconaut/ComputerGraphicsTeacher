using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teacher.ViewModels.Controls.AdvancedColorPicker
{
    public abstract class ColorModelViewModelBase
    {
        public abstract string Name { get; }
    }

    class ColorModelViewModelBaseImpl : ColorModelViewModelBase
    {
        public override string Name => "Hello";
    }
}
