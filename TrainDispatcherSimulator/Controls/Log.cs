using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainDispatcherSimulator.Helpers
{
    public class Log
    {
        public String content { get; set; }
        public LogType logType { get; set; }
        public DateTime timestamp { get; set; }
    }
}
