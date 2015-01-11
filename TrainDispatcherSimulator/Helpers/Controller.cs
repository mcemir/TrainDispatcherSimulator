﻿using System;
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
    public enum LogType { Info, Warning, Error, Critical };
    public class Controller
    {
        //Promijenio sam ovo u public accessibility samo radi testriranja.
        public Logger logger = new Logger();

        public List<RailwayBase> Railways = new List<RailwayBase>();
        public DataGrid LogDataGrid = new DataGrid();




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
            EventManager.RegisterClassHandler(typeof(MainWindow), Mouse.MouseUpEvent, new MouseButtonEventHandler(OnGlobalMouseUp));
            EventManager.RegisterClassHandler(typeof(MainWindow), Keyboard.KeyUpEvent, new KeyEventHandler(OnGlobalKeyUp));
        }

        

        


        #endregion INITIALIZATION








        #region PUBLIC METHODS

        public void ReserveSelected()
        {
            if (selectedPath != null) 
            {
                RailwayBase next = null;
                RailwayBase previous = null;
                if (selectedPath.Count > 1)
                {
                    for (int i = 0; i < selectedPath.Count; i++)
                    {
                        if (i < selectedPath.Count - 1)
                            next = selectedPath[i + 1];
                        else
                            next = null;

                        bool result = selectedPath[i].Reserve(previous, next);

                        // Ukoliko se ne može rezervisati određeni dio selektovanog puta, cijeli put se resetuje (ne može se rezervisat)
                        if (!result)
                        {
                            Log("Railway can not be reserved", LogType.Warning);
                            ClearSelected();
                            return;
                        }

                        previous = selectedPath[i];
                    }
                }
            }
            selectedPath = null;
        }

        public void ClearSelected()
        {
            PathReservation.Instance.Reset(selectedPath);
        }


        public void Log(String content, LogType type)
        {
            Log e = new Log(content, type, DateTime.Now);
            logger.Add(e);
            LogDataGrid.Items.Refresh();
        }

        #endregion PUBLIC METHODS








        #region MOUSE EVENTS PUBLIC METHODS

        private RailwayBase mouseDownRailway;
        public void RegisterMouseUp(RailwayBase railway)
        {
        }

        
        public void RegisterMouseDown(RailwayBase railway)
        {
            ClearSelected();
            mouseDownRailway = railway;
            selectedPath = PathReservation.Instance.Highligh(mouseDownRailway, mouseDownRailway);
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

        #endregion MOUSE EVENTS PUBLIC METHODS





        private void OnGlobalMouseUp(object sender, MouseButtonEventArgs e)
        {
            mouseDownRailway = null;
        }


        private void OnGlobalKeyUp(object sender, KeyEventArgs e)
        {
            string pressedKey = e.Key.ToString();
            Controller.Instance.Log("Keyboard key pressed: <" + pressedKey + ">", LogType.Info);
            switch (pressedKey)
            {
                case "R":
                    ReserveSelected();
                    break;
                case "C":
                    PathReservation.Instance.Reset(selectedPath);
                    break;
                case "D1":
                    break;
                case "D2":
                    break;
                case "D3":
                    break;
            }
        }


    }
}
