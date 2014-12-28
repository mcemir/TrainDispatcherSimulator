using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TrainDispatcherSimulator.Controls;

namespace TrainDispatcherSimulator.Helpers
{
    public enum PathDirection {RightToLeft, LeftToRight, Both};
    public enum KeyboardEvents {Reserve, Clear, ShowRailway, ShowGraph, ShowLog };
    public enum LogType { Info, Warrning, Error, Critical };
    public class Controller
    {
        //Promijenio sam ovo u public accessibility samo radi testriranja.
        public PathReservation pathFinder = new PathReservation();
        public KeyboardInput keyboardEvent = new KeyboardInput();
        public Logger logger = new Logger();

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
            pathFinder.secondPoint = railway;
            pathFinder.activate();
        }

        public void RegisterMouseDown(RailwayBase railway)
        {
            pathFinder.firstPoint = railway;
            
        }

        public void RegisterKeyPressed(KeyEventArgs e)
        {
            keyboardEvent.captureKeyboardInput(e);
        }

        #endregion PUBLIC METHODS



    }
}
