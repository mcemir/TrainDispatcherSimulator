using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;

namespace TrainDispatcherSimulator.Converters
{
    class SectionDimensionsToPointConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            FrameworkElement element = value as FrameworkElement;
            PointCollection points = new PointCollection();
            Action fillPoints = () =>
            {
                double railwayHeight = (double)App.Current.Resources["RailwayHeight"];
                points.Clear();
                points.Add(new Point(0, 0));
                points.Add(new Point(element.ActualWidth, element.ActualHeight - railwayHeight));
                points.Add(new Point(element.ActualWidth, element.ActualHeight));
                points.Add(new Point(0, railwayHeight));
            };
            fillPoints();
            element.SizeChanged += (s, ee) => fillPoints();
            return points;// store;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
