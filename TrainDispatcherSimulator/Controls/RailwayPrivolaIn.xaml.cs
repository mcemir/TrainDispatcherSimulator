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

namespace TrainDispatcherSimulator.Controls
{
    public enum RailwayPrivolaOrientation { Left, Right };

    public partial class RailwayPrivolaIn : RailwayBase
    {


        #region PROPERTIES



        public RailwayPrivolaOrientation Orientation
        {
            get { return (RailwayPrivolaOrientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Orientation.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(RailwayPrivolaOrientation), typeof(RailwayPrivolaIn), new PropertyMetadata(RailwayPrivolaOrientation.Left));

        

        #endregion PROPERTIES


        #region INITIALIZATION
        public RailwayPrivolaIn()
        {
            InitializeComponent();

        }

        #endregion INITIALIZATION




        #region PUBLIC METHODS


        public void DispatchTrain(int minutes, int seconds, Train train)
        {
            Trains.Add(train);
            RailwayBrush = App.Current.Resources["RailwayVisited"] as SolidColorBrush;
            startTimer(minutes, seconds, train);
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
