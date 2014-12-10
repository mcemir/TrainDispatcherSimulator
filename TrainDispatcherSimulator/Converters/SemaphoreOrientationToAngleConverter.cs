using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using TrainDispatcherSimulator.Controls;

namespace TrainDispatcherSimulator.Converters
{
    class SemaphoreOrientationToAngleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            SemaphoreSignal.SemaphoreOrientation orientation = (SemaphoreSignal.SemaphoreOrientation)value;

            if (orientation == SemaphoreSignal.SemaphoreOrientation.Left)
                return 0;
            else
                return 180;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
