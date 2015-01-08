using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TrainDispatcherSimulator.Converters;

namespace TrainDispatcherSimulator.Helpers
{
    public class Log
    {
        public Log(String contentData, LogType type, DateTime time)
        {
            Content = contentData;
            LogType = type;
            Timestamp = time;
            //Izgleda da mora konverter se registrovati preko resursa.
            Color = "Black";
        }

        public LogType LogType { get; set; }
        public DateTime Timestamp { get; set; }
        public String Content { get; set; }
        public String Color { get; set; }
        
        
    }
}
