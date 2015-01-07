using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TrainDispatcherSimulator.Helpers;

namespace TrainDispatcherSimulator.Converters
{
    public static class LogTypeToColor
    {
        public static SolidColorBrush Convert(LogType type)
        {
            switch(type){
                case LogType.Info:
                    return new SolidColorBrush(Color.FromArgb(255, 64, 224, 208));
                    break;
                case LogType.Warning:
                    return new SolidColorBrush(Color.FromArgb(255, 255, 215, 0));
                    break;
                case LogType.Error:
                    return new SolidColorBrush(Color.FromArgb(255, 255, 69, 0));
                    break;
                case LogType.Critical:
                    return new SolidColorBrush(Color.FromArgb(255, 128, 0, 0));
                    break;
            }

            return new SolidColorBrush(Color.FromArgb(255, 0, 0, 0));
        }
    }
}
