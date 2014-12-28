using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainDispatcherSimulator.Helpers
{
    public class Logger
    {
        public Queue<Log> logs = new Queue<Log>();

        public void Add(Log e){
            logs.Enqueue(e);
        }
    }
}
