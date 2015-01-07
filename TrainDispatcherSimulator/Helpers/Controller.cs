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
    public enum LogType { Info, Warning, Error, Critical };
    public class Controller
    {
        //Promijenio sam ovo u public accessibility samo radi testriranja.
        public KeyboardInput keyboardEvent = new KeyboardInput();
        public Logger logger = new Logger();

        public List<RailwayBase> Railways = new List<RailwayBase>();
        public DataGrid LogDataGrid = new DataGrid();


        // Čisto radi čuvanja referenci
        private RailwayBase mouseDownRailway;
        private List<RailwayBase> selectedPath = new List<RailwayBase>();


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
            PathReservation.Instance.Railways = Railways;
            EventManager.RegisterClassHandler(typeof(MainWindow), Mouse.MouseUpEvent, new MouseButtonEventHandler(OnGlobaMouseUp));
        }


        #endregion INITIALIZATION




        #region PUBLIC METHODS

        public void RegisterMouseUp(RailwayBase railway)
        {
            //pathFinder.secondPoint = railway;
            //pathFinder.activate();
        }

        public void Log(String content, LogType type)
        {
            Log e = new Log(content, type, DateTime.Now);
            logger.Add(e);
            LogDataGrid.Items.Refresh();
        }
        public void RegisterMouseDown(RailwayBase railway)
        {
            mouseDownRailway = railway;
            //pathFinder.firstPoint = railway;            
        }

        public void RegisterMouseEnter(RailwayBase railway)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed && mouseDownRailway != null)
            {
                selectedPath = PathReservation.Instance.Highligh(mouseDownRailway, railway);
            }
        }

        public void RegisterMouseLeave(RailwayBase railway)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed)
            {
                PathReservation.Instance.Reset(selectedPath);
            }
        }

        public void RegisterKeyPressed(KeyEventArgs e)
        {
            keyboardEvent.captureKeyboardInput(e);
            Controller.Instance.Log("Key shortcut pressed!", LogType.Info);
        }

        #endregion PUBLIC METHODS





        private void OnGlobaMouseUp(object sender, MouseButtonEventArgs e)
        {
            mouseDownRailway = null;
        }

    }
}
