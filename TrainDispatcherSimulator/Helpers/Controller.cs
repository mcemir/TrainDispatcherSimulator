using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using TrainDispatcherSimulator.Controls;

namespace TrainDispatcherSimulator.Helpers
{
    public enum PathDirection {RightToLeft, LeftToRight, Both};
    public enum KeyboardEvents {Reserve, Clear, ShowRailway, ShowGraph, ShowLog };
    public class Controller
    {
        private PathReservation pathFinder = new PathReservation();
        private KeyboardInput keyboardEvent = new KeyboardInput();

        public List<RailwayBase> Railways = new List<RailwayBase>();

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

        public void RegisterKeyPressed(KeyEventArgs e)
        {
            keyboardEvent.parseKeyboardInput(e);
        }

        #endregion PUBLIC METHODS



    }
}
