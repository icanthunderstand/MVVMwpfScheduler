using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace advancedWpfProject.ExtensionMarkup
{
    public class DoubleExtension : MarkupExtension
    {
        public DoubleExtension(double value) { this.Value = value; }
        public double Value { get; set; }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Value;
        }
    }
}
