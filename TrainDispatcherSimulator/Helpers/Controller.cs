using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using TrainDispatcherSimulator.Controls;

namespace TrainDispatcherSimulator.Helpers
{
    public enum PathDirection {RightToLeft, LeftToRight, Both};
    public enum LogType { Info, Warning, Error, Critical };
    public class Controller
    {
        //Promijenio sam ovo u public accessibility samo radi testriranja.
        public Logger logger = new Logger();
        public DataGrid LogDataGrid;

        public DataGrid ScheduleDataGrid;
        public List<ScheduleItem> ScheduleList = new List<ScheduleItem>();


        public List<RailwayBase> Railways = new List<RailwayBase>();
        private List<RailwayBase> selectedPath = new List<RailwayBase>();

        private MainWindow mainWindow;

        public AudioSignal AudioSignal;

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
            mainWindow = ((MainWindow)System.Windows.Application.Current.MainWindow);
            AudioSignal = AudioSignal.Instance;
            PathReservation.Instance.Railways = Railways;
            EventManager.RegisterClassHandler(typeof(MainWindow), Mouse.MouseUpEvent, new MouseButtonEventHandler(OnGlobalMouseUp));
            EventManager.RegisterClassHandler(typeof(MainWindow), Keyboard.KeyUpEvent, new KeyEventHandler(OnGlobalKeyUp));
        }



        public void InitSchedule()
        {
            Train p101 = new Train() {
                Name = "P101",
                MaxSpeed = 120,
                Type = TrainType.Passenger,
                WagonCount = 3,
                Orientation = TrainOrientation.Right
            };

            Train p102 = new Train() {
                Name = "P102",
                MaxSpeed = 80,
                Type = TrainType.Passenger,
                WagonCount = 2,
                Orientation = TrainOrientation.Right
            };

            Train p103 = new Train() {
                Name = "P103",
                MaxSpeed = 80,
                Type = TrainType.Passenger,
                WagonCount = 5,
                Orientation = TrainOrientation.Right
            };

            Train p104 = new Train() {
                Name = "P104",
                MaxSpeed = 80,
                Type = TrainType.Passenger,
                WagonCount = 3,
                Orientation = TrainOrientation.Right
            };

            Train p105 = new Train() {
                Name = "P105",
                MaxSpeed = 80,
                Type = TrainType.Passenger,
                WagonCount = 3,
                Orientation = TrainOrientation.Right
            };

            Train f101 = new Train() {
                Name = "F101",
                MaxSpeed = 80,
                Type = TrainType.Freight,
                WagonCount = 20,
                Orientation = TrainOrientation.Right
            };

            Train f102 = new Train() {
                Name = "F102",
                MaxSpeed = 80,
                Type = TrainType.Freight,
                WagonCount = 15,
                Orientation = TrainOrientation.Right
            };



            DateTime date = DateTime.Now;
            date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0);


            ScheduleItem item1 = new ScheduleItem()
            {
                Train = p101,
                ScheduleType = ScheduleType.Arrival,
                Target = "P1",
                Time = date.AddMinutes(1),
                From = "Zagreb",
                To = "Ploče"
            };
            ScheduleList.Add(item1);

            ScheduleItem item2 = new ScheduleItem()
            {
                Train = p102,
                ScheduleType = ScheduleType.Arrival,
                Target = "P3",
                Time = date.AddMinutes(2),
                From = "Beograd",
                To = "Ploče"
            };
            ScheduleList.Add(item2);
            /*
            ScheduleItem item3 = new ScheduleItem()
            {
                Train = f101,
                ScheduleType = ScheduleType.Arrival,
                Target = "",
                Time = date.AddMinutes(3),
                From = "Doboj",
                To = "Sarajevo"
            };
            ScheduleList.Add(item3);

            ScheduleItem item4 = new ScheduleItem()
            {
                Train = p101,
                ScheduleType = ScheduleType.Departure,
                Target = "N1",
                Time = date.AddMinutes(4),
                From = "Zagreb",
                To = "Ploče"
            };
            ScheduleList.Add(item4);

            ScheduleItem item5 = new ScheduleItem()
            {
                Train = f102,
                ScheduleType = ScheduleType.Arrival,
                Target = "",
                Time = date.AddMinutes(5),
                From = "Ploče",
                To = "Sarajevo"
            };
            ScheduleList.Add(item5);

            ScheduleItem item6 = new ScheduleItem()
            {
                Train = p102,
                ScheduleType = ScheduleType.Departure,
                Target = "N1",
                Time = date.AddMinutes(6),
                From = "Beograd",
                To = "Ploče"
            };
            ScheduleList.Add(item6);

            ScheduleItem item7 = new ScheduleItem()
            {
                Train = p103,
                ScheduleType = ScheduleType.Arrival,
                Target = "P2",
                Time = date.AddMinutes(7),
                From = "Zenica",
                To = "Sarajevo"
            };
            ScheduleList.Add(item7);

            ScheduleItem item8 = new ScheduleItem()
            {
                Train = p104,
                ScheduleType = ScheduleType.Arrival,
                Target = "P4",
                Time = date.AddMinutes(8),
                From = "Mostar",
                To = "Sarajevo"
            };
            ScheduleList.Add(item8);

            ScheduleItem item9 = new ScheduleItem()
            {
                Train = p105,
                ScheduleType = ScheduleType.Arrival,
                Target = "P1",
                Time = date.AddMinutes(9),
                From = "Mostar",
                To = "Doboj"
            };
            ScheduleList.Add(item9);

            ScheduleItem item10 = new ScheduleItem()
            {
                Train = p105,
                ScheduleType = ScheduleType.Departure,
                Target = "N1",
                Time = date.AddMinutes(10),
                From = "Mostar",
                To = "Doboj"
            };
            ScheduleList.Add(item10);
            */
            RailwayPrivola ulaznaPrivola = Railways.First(r => (r as RailwayBase).RailwayName == "N2") as RailwayPrivola;

            ulaznaPrivola.DispatchTrain((item1.Time - DateTime.Now).Minutes, (item1.Time - DateTime.Now).Seconds, p101);

            startScheduleTimer();
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
                        Controller.Instance.AudioSignal.RailwayReservedPath();
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

        #region VISIBILITY MANAGEMENT
        public void showGraph()
        {
            collapseAll();
            mainWindow.graphGrid.Visibility = Visibility.Visible;
        }

        public void showRailway()
        {
            collapseAll();
            mainWindow.mainRailwayGrid.Visibility = Visibility.Visible;
            mainWindow.notificationPanelGrid.Visibility = Visibility.Visible;
            mainWindow.controlPanelGrid.Visibility = Visibility.Visible;
        }

        public void showTableGrid()
        {
            collapseAll();
            mainWindow.mainTableGrid.Visibility = Visibility.Visible;
        }
        public void showLogger()
        {
            collapseAll();
            mainWindow.logGrid.Visibility = Visibility.Visible;
        }
        
        private void collapseAll()
        {
            mainWindow.logGrid.Visibility = Visibility.Collapsed;
            mainWindow.controlPanelGrid.Visibility = Visibility.Collapsed;
            mainWindow.notificationPanelGrid.Visibility = Visibility.Collapsed;
            mainWindow.mainRailwayGrid.Visibility = Visibility.Collapsed;
            mainWindow.graphGrid.Visibility = Visibility.Collapsed;
            mainWindow.mainTableGrid.Visibility = Visibility.Collapsed;
        }

        #endregion VISIBILITY MANAGEMENT

        #region MOUSE EVENTS PUBLIC METHODS

        private RailwayBase mouseDownRailway;
        public void RegisterMouseUp(RailwayBase railway)
        {
            if (railway != null)
            {
                if (railway.GetType().BaseType == typeof(RailwaySwitchBase) || railway.GetType() == typeof(RailwayCross))
                {
                    Controller.Instance.AudioSignal.RailwaySwitchToogle();
                    Controller.Instance.Log("Railway switch segment activated: <" + railway.RailwayName + ">", LogType.Info);
                }
                else
                {
                    Controller.Instance.AudioSignal.RailwaySelectedPath();
                }
                
            }
        }

        
        public void RegisterMouseDown(RailwayBase railway)
        {
            ClearSelected();
            mouseDownRailway = railway;
            selectedPath = PathReservation.Instance.Highlight(mouseDownRailway, mouseDownRailway);
        }

        public void RegisterMouseEnter(RailwayBase railway)
        {
            if (Mouse.LeftButton == MouseButtonState.Pressed && mouseDownRailway != null)
            {
                selectedPath = PathReservation.Instance.Highlight(mouseDownRailway, railway);
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





        #region GLOBAL EVENT HANDLERS
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
                    showRailway();
                    break;
                case "D2":
                    showGraph();
                    break;
                case "D3":
                    showTableGrid();
                    break;
                case "D4":
                    showLogger();
                    break;
            }
        }
        #endregion GLOBAL EVENT HANDLERS

        #region DISPATCHER TIMERS
        int currentIndex = 0;   // Trenutni na koji se čeka
        protected DispatcherTimer scheduleTimer = new DispatcherTimer(DispatcherPriority.Render); // Set priority to render
        protected virtual void startScheduleTimer()
        {
            int intervalSec = 10;
            scheduleTimer.Interval = new TimeSpan(0, 0, 0, intervalSec, 0);
            scheduleTimer.Tick += (s, args) =>
            {
                if (currentIndex < ScheduleList.Count - 1)
                {
                    DateTime date = DateTime.Now;
                    date = new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 0);

                    if (ScheduleList[currentIndex].Time < DateTime.Now.AddSeconds(-5))
                    {
                        while ((currentIndex < ScheduleList.Count) && (ScheduleList[currentIndex].Time < DateTime.Now))
                            currentIndex++;

                        if (currentIndex < ScheduleList.Count)
                        {
                            if (ScheduleList[currentIndex].ScheduleType != ScheduleType.Departure)
                            {
                                RailwayPrivola ulaznaPrivola = Railways.First(r => (r as RailwayBase).RailwayName == "N2") as RailwayPrivola;
                                double sec = (ScheduleList[currentIndex].Time - DateTime.Now).TotalSeconds;
                                int min = (int)(sec / 60);
                                ulaznaPrivola.DispatchTrain(min, (int)(sec - 60 * min), ScheduleList[currentIndex].Train);
                            }

                            ScheduleDataGrid.SelectedItem = ScheduleList[currentIndex];
                        }
                    }
                }
                else
                {
                    scheduleTimer.Stop();
                }
            };
            scheduleTimer.Start();
        }
        #endregion DISPATCHER TIMERS
    }
}
