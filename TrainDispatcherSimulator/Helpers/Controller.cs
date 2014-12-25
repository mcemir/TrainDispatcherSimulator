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



        #region INITIALIZATION

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

        private Controller()
        {

        }

        #endregion INITIALIZATION





        #region PUBLIC METHODS

        public void RegisterMouseUp(RailwayBase railway)
        {

        }

        public void RegisterMouseDown(RailwayBase railway)
        {

        }

        #endregion PUBLIC METHODS



    }
}
