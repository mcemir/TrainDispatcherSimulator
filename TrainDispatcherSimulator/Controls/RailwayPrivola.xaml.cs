using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using TrainDispatcherSimulator.Helpers;

namespace TrainDispatcherSimulator.Controls
{

    public partial class RailwayPrivola : RailwayBase
    {


        #region PROPERTIES


        

        #endregion PROPERTIES


        #region INITIALIZATION
        public RailwayPrivola()
        {
            InitializeComponent();


            // TEST 
            this.Loaded += RailwayPrivola_Loaded;
            

        }

        // TEST 
        private void RailwayPrivola_Loaded(object sender, RoutedEventArgs e)
        {
        }

        #endregion INITIALIZATION




        #region PUBLIC METHODS


        public void DispatchTrain(int minutes, int seconds, Train train)
        {
            Trains.Add(train);
            RailwayBrush = App.Current.Resources["RailwayVisited"] as SolidColorBrush;
            startTimer(minutes, seconds, train);

            TrainName = train.Name;

            if (train.Orientation == TrainOrientation.Left)
            {
                leftTriangle.Fill = App.Current.Resources["RailwayVisited"] as SolidColorBrush;
                TrainNameLeftPanelVisibility = Visibility.Visible;
            }
            else 
            {
                rightTriangle.Fill = App.Current.Resources["RailwayVisited"] as SolidColorBrush;                
                TrainNameRightPanelVisibility = Visibility.Visible;
            }


            
        }



        public override void EnterRailway(Train train)
        {
            base.EnterRailway(train);
            Controller.Instance.AudioSignal.RailwayPrivolaArrival();
            Controller.Instance.Log("Train arriving to section: <" + this + ">", LogType.Warning);
            if (train.Orientation == TrainOrientation.Left)
                leftTriangle.Fill = App.Current.Resources["RailwayVisited"] as SolidColorBrush;
            else
                rightTriangle.Fill = App.Current.Resources["RailwayVisited"] as SolidColorBrush;
        }


        // Overide-amo leaving jer ne treba provjeravat semafore i ako je null sljedeći treba opet sve resetat
        public override void LeaveRailway(Train train)
        {
            RailwayBase nextRailway;

            if (train.Orientation == TrainOrientation.Left)
                nextRailway = this.GetLeftRailway();
            else
                nextRailway = this.GetRightRailway();

            if (nextRailway != null)        // Ukoliko nema sljedećeg railway-a ne radi dalje ništa jer train izlazi napolje
                nextRailway.EnterRailway(train);


            Trains.RemoveAll(t => t.Name == train.Name);
            this.startTimerLeaving(train);

            // Reset triangle color
            leftTriangle.Fill = App.Current.Resources["RailwayBaseBrush"] as SolidColorBrush;
            rightTriangle.Fill = App.Current.Resources["RailwayBaseBrush"] as SolidColorBrush;

            TrainNameLeftPanelVisibility = Visibility.Collapsed;
            TrainNameRightPanelVisibility = Visibility.Collapsed;
            
        }

        #endregion PUBLIC METHODS





        #region PRIVATE METHODS


        #endregion PRIVATE METHODS




        #region DISPATCHER TIMER
        // Timer koji služi za animaciju sata na GUI-u
        private DispatcherTimer timer;
        private int minutes, seconds;

        private void startTimer(int minutes, int seconds, Train train) 
        {
            this.minutes = minutes;
            this.seconds = seconds;

            timer = new DispatcherTimer(DispatcherPriority.Render); // Set priority to render
            timer.Tick += (s, args) => timer_Tick(train);
            timer.Interval = new TimeSpan(0, 0, 0, 1, 0);           // Tick every second
            timer.Start();
        }

        private void timer_Tick(Train train)
        {
            if (minutes <= 0 && seconds <= 0)    // If offset is in the past
            {
                timerTextBlock.Text = "";
                this.LeaveRailway(train);
                timer.Stop();
            }
            else
            {
                seconds--;
                if (seconds < 0)
                {
                    seconds = 59;
                    minutes--;
                }
                string secStr = seconds < 10 ? "0" + seconds : "" + seconds;
                timerTextBlock.Text = minutes + ":" + secStr;
            }
        }
        

        #endregion DISPATCHER TIMER

    }
}
