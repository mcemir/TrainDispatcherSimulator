using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrainDispatcherSimulator.Controls;

namespace TrainDispatcherSimulator.Helpers
{
    public class Controller
    {

        private static Controller instance;
        public static Controller Instance
        {
            get
            {
                if (instance == null)
                    instance = new Controller();

                return instance;
            }
        }
        public RailwayBase startingPoint { get; set; }

        public RailwayBase finalPoint { get; set; }

    }
}
