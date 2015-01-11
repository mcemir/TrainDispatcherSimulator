using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainDispatcherSimulator.Controls;

namespace TrainDispatcherSimulator.Helpers
{
    public enum ScheduleType { Arrival, Departure };
    public class ScheduleItem
    {
        public Train Train { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public string Target { get; set; }
        public DateTime Time { get; set; }
        public ScheduleType ScheduleType { get; set; }

    }
}
