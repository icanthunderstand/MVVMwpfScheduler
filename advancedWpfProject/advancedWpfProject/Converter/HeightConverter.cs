using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace advancedWpfProject.Converter
{
    public class HeightConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime start = (DateTime)values[0];
            DateTime end = (DateTime)values[1];
            double height = (double)values[2] / (int)parameter;

            int smin = start.Minute >= 30 ? 1 : 0;
            int emin = end.Minute >= 30 ? 1 : 0;
            int temp = emin - smin + ((end.Hour - start.Hour) * 2);

            return temp * height;
            throw new NotImplementedException();
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
