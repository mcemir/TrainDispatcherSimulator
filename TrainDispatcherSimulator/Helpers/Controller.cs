using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using TrainDispatcherSimulator.Controls;

namespace TrainDispatcherSimulator.Helpers
{
    public enum PathDirection {RightToLeft, LeftToRight, Both};
    public class Controller
    {
        private PathReservation pathFinder = new PathReservation();



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
            pathFinder.finalPoint = railway;
            pathFinder.activate();
        }

        public void RegisterMouseDown(RailwayBase railway)
        {
            pathFinder.startingPoint = railway;
            
        }

        #endregion PUBLIC METHODS



    }
}
