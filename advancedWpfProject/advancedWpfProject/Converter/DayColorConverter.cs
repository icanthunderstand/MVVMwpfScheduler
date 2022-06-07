using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using advancedWpfProject.MVVM.ViewModel;

namespace advancedWpfProject.Converter
{
    public class DayColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime showDay = ((DateTime)values[0]).AddDays((int)parameter);

            DateTime temp = (DateTime)values[1];

            bool IsSameMonth = showDay.Month == temp.Month;

            if (IsSameMonth) return Brushes.Black;
            return Brushes.LightGray;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
