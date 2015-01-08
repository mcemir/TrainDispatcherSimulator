using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainDispatcherSimulator.Helpers
{
    public class Logger
    {
        public List<Log> logs = new List<Log>();

        public void Add(Log e){
            logs.Insert(0,e);
        }
    }
}
