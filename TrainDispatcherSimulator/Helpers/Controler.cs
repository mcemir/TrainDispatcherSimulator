using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainDispatcherSimulator.Controls;

namespace TrainDispatcherSimulator.Helpers
{
    public class Controler
    {
        public RailwayBase startingPoint { get; set; }

        public RailwayBase finalPoint { get; set; }

        public Controler()
        {
            startingPoint = null;
            finalPoint = null;
        }
    }
}
