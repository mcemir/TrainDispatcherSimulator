using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Collections.ObjectModel;
using System.Windows.Threading;

namespace TrainDispatcherSimulator.Controls
{
    public class RailwayBase : UserControl
    {
        private List<RailwayBase> leftRailways = new List<RailwayBase>();
        private List<RailwayBase> rightRailways = new List<RailwayBase>();




        #region PROPERTIES


        // Railway name (used to display the name of the railway, e.g. "Switch1")
        public string RailwayName
        {
            get { return (string)GetValue(RailwayNameProperty); }
            set { SetValue(RailwayNameProperty, value); }
        }
        public static readonly DependencyProperty RailwayNameProperty =
            DependencyProperty.Register("RailwayName", typeof(string), typeof(RailwayBase), new PropertyMetadata(""));

        

        
        // The color of the railway
        public SolidColorBrush RailwayBrush
        {
            get { return (SolidColorBrush)GetValue(RailwayBrushProperty); }
            set { SetValue(RailwayBrushProperty, value); }
        }
        public static readonly DependencyProperty RailwayBrushProperty =
            DependencyProperty.Register("RailwayBrush", typeof(SolidColorBrush), typeof(RailwayBase), new PropertyMetadata((App.Current.Resources["RailwayBaseBrush"] as SolidColorBrush) ));



        // The platform that is bound to the railway
        public Platform Platform
        {
            get { return (Platform)GetValue(PlatformProperty); }
            set { SetValue(PlatformProperty, value); }
        }
        public static readonly DependencyProperty PlatformProperty =
            DependencyProperty.Register("Platform", typeof(Platform), typeof(RailwayBase), new PropertyMetadata(null));


        public List<Train> Trains { get; set; }
        public int Length  { get; set; }            // Dužina sekcije u metrima
        #endregion PROPERTIES








        #region INITIALIZATION

        public RailwayBase()
        {
            DataContext = this;
            RailwayBrush = App.Current.Resources["RailwayBaseBrush"] as SolidColorBrush;
            Trains = new List<Train>();

            this.MouseEnter += RailwayBase_MouseEnter;
            this.MouseLeave += RailwayBase_MouseLeave;
            this.Loaded += RailwayBase_Loaded;

        }

        void RailwayBase_Loaded(object sender, RoutedEventArgs e)
        {
            populateRailwayJoints();
        }


        // Get left and right railways
        private void populateRailwayJoints()
        {
            // Get position
            int row = Grid.GetRow(this);
            int col = Grid.GetColumn(this);
            int rowSpan = Grid.GetRowSpan(this);
            int colSpan = Grid.GetColumnSpan(this);

            Grid parentGrid = this.Parent as Grid;



            leftRailways = parentGrid.Children.Cast<UserControl>().Where(c => c != this && c is RailwayBase && (
                (Grid.GetRow(c) == row - 1 && Grid.GetColumn(c) + Grid.GetColumnSpan(c) == col && Grid.GetRowSpan(c) == 2 && !(c is RailwaySwitch4)) ||     
                (Grid.GetRow(c) == row && Grid.GetColumn(c) + Grid.GetColumnSpan(c) == col) ||
                (rowSpan == 2 && Grid.GetRow(c) == row + 1 && Grid.GetColumn(c) + Grid.GetColumnSpan(c) == col && !(c is RailwaySwitch3))))
                .OrderBy(c => Grid.GetRow(c)).Cast<RailwayBase>().ToList();

            leftRailways = parentGrid.Children.Cast<UserControl>().Where(c => c != this && c is RailwayBase && (
                (Grid.GetRow(c) == row - 1 && Grid.GetColumn(c) == col + colSpan && Grid.GetRowSpan(c) == 2 && !(c is RailwaySwitch1)) ||    
                (Grid.GetRow(c) == row && Grid.GetColumn(c) == col + colSpan) ||
                (rowSpan == 2 && Grid.GetRow(c) == row + 1 && Grid.GetColumn(c) == col + colSpan && !(c is RailwaySwitch2))))
                .OrderBy(c => Grid.GetRow(c)).Cast<RailwayBase>().ToList();

            

            // Ako je rowSpan 2 imaju specijalni slučajevi, ali ih nema na našem čvoru
            if (rowSpan == 2 && leftRailways.Count == 1 && 
                Grid.GetRowSpan(leftRailways[0]) == 2 &&
                Grid.GetRow(leftRailways[0]) == row)
            {
                leftRailways.Add(leftRailways[0]); // Dodaj još jednu instancu
            }
            if (rowSpan == 2 && rightRailways.Count == 1 &&
                Grid.GetRowSpan(rightRailways[0]) == 2 &&
                Grid.GetRow(rightRailways[0]) == row)
            {
                leftRailways.Add(rightRailways[0]); // Dodaj još jednu instancu
            }

        }

        #endregion INITIALIZATION






        #region PUBLIC METHODS

        public virtual RailwayBase GetLeftRailway()
        {
            if (leftRailways.Count > 0)
                return leftRailways[0];
            return null;
        }

        public virtual RailwayBase GetRightRailway()
        {
            if (rightRailways.Count > 0)
                return rightRailways[0];
            return null;
        }




        public virtual void EnterRailway(Train train)
        {
            Trains.Add(train);
            RailwayBrush = App.Current.Resources["RailwayVisited"] as SolidColorBrush;
            startTimerDriving(train);
        }

        public virtual void LeaveRailway(Train train)
        {
            RailwayBase nextRailway;


            if (train.Orientation == TrainOrientation.Left)
            {
                // Provjeri lijevi semafor
                nextRailway = this.GetLeftRailway();
            }
            else
            {
                // Provjeri desni semafor semafor
                nextRailway = this.GetRightRailway();
            }

            if (nextRailway != null)
            {
                Trains.Remove(train);
                nextRailway.EnterRailway(train);
                startTimerLeaving(train);
            }
        }

        #endregion PUBLIC METHODS





        #region PRIVATE METHODS

        #endregion PRIVATE METHODS





        #region EVENT HANDLERS

        

        #endregion EVENT HANDLERS






        #region DISPATCHER TIMERS
        private void startTimerDriving(Train train)
        {
            int drivingTime = (this.Length / train.MaxSpeed * 1000)*3600;

            DispatcherTimer timerDriving = new DispatcherTimer(DispatcherPriority.Render); // Set priority to render
            timerDriving.Interval = new TimeSpan(0, 0, 0, drivingTime, 0);


            timerDriving.Tick += (s, args) =>
            {
                LeaveRailway(train);
                timerDriving.Stop();
            };

            timerDriving.Start();
        }


        private void startTimerLeaving(Train train)
        {
            int leavingTime = (train.Length / train.MaxSpeed * 1000) * 3600;

            DispatcherTimer timerDriving = new DispatcherTimer(DispatcherPriority.Render); // Set priority to render
            timerDriving.Interval = new TimeSpan(0, 0, 0, leavingTime, 0);


            timerDriving.Tick += (s, args) =>
            {
                RailwayBrush = App.Current.Resources["RailwayBaseBrush"] as SolidColorBrush;
                timerDriving.Stop();
            };

            timerDriving.Start();
        }



        #endregion DISPATCHER TIMERS







        #region MOUSE OVER
        private SolidColorBrush tmpRailwayBrush;
        private int brightnesIntensity = 40;
        void RailwayBase_MouseEnter(object sender, MouseEventArgs e)
        {
            tmpRailwayBrush = RailwayBrush;

            byte R = (byte)(RailwayBrush.Color.R + brightnesIntensity);
            R = RailwayBrush.Color.R < R ? R : (byte)255;

            byte G = (byte)(RailwayBrush.Color.G + brightnesIntensity);
            G = RailwayBrush.Color.G < G ? G : (byte)255;

            byte B = (byte)(RailwayBrush.Color.B + brightnesIntensity);
            B = RailwayBrush.Color.B < B ? B : (byte)255;

            RailwayBrush = new SolidColorBrush(Color.FromArgb(255, R, G, B));
        }

        void RailwayBase_MouseLeave(object sender, MouseEventArgs e)
        {
            RailwayBrush = tmpRailwayBrush;
        }
        #endregion MOUSE OVER

    }


}
