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
using TrainDispatcherSimulator.Helpers;

namespace TrainDispatcherSimulator.Controls
{
    public class RailwayBase : UserControl
    {
        public List<RailwayBase> LeftRailways = new List<RailwayBase>();
        public List<RailwayBase> RightRailways = new List<RailwayBase>();
        



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
        public bool Reserved { get; set; }
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
            this.MouseDown += RailwayBase_MouseDown;
            this.MouseUp += RailwayBase_MouseUp;

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


            if (parentGrid != null)
            {
                LeftRailways = parentGrid.Children.Cast<UserControl>().Where(c => c != this && c is RailwayBase && (
                    (Grid.GetRow(c) == row - 1 && Grid.GetColumn(c) + Grid.GetColumnSpan(c) == col && Grid.GetRowSpan(c) == 2 && !(c is RailwaySwitch4)) && !(c is RailwayCurve2) ||
                    (Grid.GetRow(c) == row && Grid.GetColumn(c) + Grid.GetColumnSpan(c) == col && !(c is RailwayCurve1) && (!(this is RailwayCurve2) && !(c is RailwaySwitch3))) ||
                    (rowSpan == 2 && Grid.GetRow(c) == row + 1 && Grid.GetColumn(c) + Grid.GetColumnSpan(c) == col && !(c is RailwaySwitch3) && !(c is RailwayCurve1))))
                    .OrderBy(c => Grid.GetRow(c)).Cast<RailwayBase>().ToList();

                RightRailways = parentGrid.Children.Cast<UserControl>().Where(c => c != this && c is RailwayBase && (
                    (Grid.GetRow(c) == row - 1 && Grid.GetColumn(c) == col + colSpan && Grid.GetRowSpan(c) == 2 && !(c is RailwaySwitch1)) && !(c is RailwayCurve1) ||
                    (Grid.GetRow(c) == row && Grid.GetColumn(c) == col + colSpan && !(c is RailwayCurve2) && (!(this is RailwayCurve1) && !(c is RailwaySwitch2))) ||
                    (rowSpan == 2 && Grid.GetRow(c) == row + 1 && Grid.GetColumn(c) == col + colSpan && !(c is RailwaySwitch2) && !(c is RailwayCurve2))))
                    .OrderBy(c => Grid.GetRow(c)).Cast<RailwayBase>().ToList();
            }
            

            // Ako je rowSpan 2 imaju specijalni slučajevi, ali ih nema na našem čvoru
            if (rowSpan == 2 && LeftRailways.Count == 1 && 
                Grid.GetRowSpan(LeftRailways[0]) == 2 &&
                Grid.GetRow(LeftRailways[0]) == row)
            {
                LeftRailways.Add(LeftRailways[0]); // Dodaj još jednu instancu
            }
            if (rowSpan == 2 && RightRailways.Count == 1 &&
                Grid.GetRowSpan(RightRailways[0]) == 2 &&
                Grid.GetRow(RightRailways[0]) == row)
            {
                RightRailways.Add(RightRailways[0]); // Dodaj još jednu instancu
            }

        }

        #endregion INITIALIZATION






        #region PUBLIC METHODS

        // Parametar "referent" je referentni railway nad kojim se racuna sljedeći (prethodni). Ovaj parametar se koristi samo kod Cross-a i Switch-a 5 i 6 
        public virtual RailwayBase GetLeftRailway()
        {
            if (LeftRailways.Count > 0)
                return LeftRailways[0];
            return null;
        }

        public virtual RailwayBase GetRightRailway()
        {
            if (RightRailways.Count > 0)
                return RightRailways[0];
            return null;
        }

        public List<RailwayBase> GetNeighbors()
        {
            List<RailwayBase> sectionNeighbors = new List<RailwayBase>(LeftRailways.Count + RightRailways.Count);
            sectionNeighbors.AddRange(RightRailways);
            sectionNeighbors.AddRange(LeftRailways);
           
            return sectionNeighbors;
        }



        public virtual void EnterRailway(Train train)
        {
            Trains.Add(train);
            RailwayBrush = App.Current.Resources["RailwayVisited"] as SolidColorBrush;
            startTimerDriving(train);
        }

        public virtual void LeaveRailway(Train train)
        {
            RailwayBase nextRailway = getNextRailway(train);

            if (nextRailway != null)
            {
                nextRailway.EnterRailway(train);
                startTimerLeaving(train);
                Trains.Remove(train);
            }
        }


        // Vraća true ukoliko je rezervacija uspešna
        // previousRailway je prethodnik trenutnom
        // nextRailway je sljedbenik trenutnom
        public virtual bool Reserve(RailwayBase previousRailway, RailwayBase nextRailway)
        {
            Reserved = true;
            if (Trains.Count == 0)
                RailwayBrush = App.Current.Resources["RailwayReservedBrush"] as SolidColorBrush;

            return true;
        }


        public virtual void Reset()
        {
            // Moze se resetovat samo ukoliko je prazan railway
            if (Trains.Count == 0)
            {
                Reserved = false;
                RailwayBrush = App.Current.Resources["RailwayBaseBrush"] as SolidColorBrush;
            }
        }

        #endregion PUBLIC METHODS





        #region PRIVATE METHODS

        protected RailwayBase getNextRailway(Train train)
        {
            RailwayBase nextRailway;

            if (train.Orientation == TrainOrientation.Left)
            {
                nextRailway = this.GetLeftRailway();
            }
            else
            {
                nextRailway = this.GetRightRailway();
            }

            return nextRailway;
        }

        #endregion PRIVATE METHODS





        #region EVENT HANDLERS

        void RailwayBase_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Controller.Instance.RegisterMouseUp(this);
        }

        void RailwayBase_MouseDown(object sender, MouseButtonEventArgs e)
        {
            Controller.Instance.RegisterMouseDown(this);
        }
        

        #endregion EVENT HANDLERS






        #region DISPATCHER TIMERS
        protected DispatcherTimer drivingTimer = new DispatcherTimer(DispatcherPriority.Render); // Set priority to render
        protected virtual void startTimerDriving(Train train)
        {
            int drivingTimeSec = 2;

            
            drivingTimer.Interval = new TimeSpan(0, 0, 0, drivingTimeSec, 0);


            drivingTimer.Tick += (s, args) =>
            {
                LeaveRailway(train);
                drivingTimer.Stop();
            };

            drivingTimer.Start();
        }


        protected virtual void startTimerLeaving(Train train)
        {
            int leavingTimeSec = 1;

            DispatcherTimer timer = new DispatcherTimer(DispatcherPriority.Render); // Set priority to render
            timer.Interval = new TimeSpan(0, 0, 0, leavingTimeSec, 0);


            timer.Tick += (s, args) =>
            {
                RailwayBrush = App.Current.Resources["RailwayBaseBrush"] as SolidColorBrush;
                timer.Stop();
                Reset();
                trainLeft(train);
            };

            timer.Start();
        }

        protected virtual void trainLeft(Train train)
        {
        }


        #endregion DISPATCHER TIMERS







        #region MOUSE OVER
        private SolidColorBrush tmpRailwayBrush;
        private int brightnesIntensity = 40;


        private SolidColorBrush brightBrush(SolidColorBrush brush)
        {
            byte R = (byte)(brush.Color.R + brightnesIntensity);
            R = brush.Color.R < R ? R : (byte)255;

            byte G = (byte)(brush.Color.G + brightnesIntensity);
            G = brush.Color.G < G ? G : (byte)255;

            byte B = (byte)(brush.Color.B + brightnesIntensity);
            B = brush.Color.B < B ? B : (byte)255;

            return new SolidColorBrush(Color.FromArgb(255, R, G, B));
        }

        private void RailwayBase_MouseEnter(object sender, MouseEventArgs e)
        {
            tmpRailwayBrush = RailwayBrush;
            RailwayBrush = brightBrush(RailwayBrush);
        }

        private void RailwayBase_MouseLeave(object sender, MouseEventArgs e)
        {
            if (RailwayBrush.Color == brightBrush(tmpRailwayBrush).Color)
                RailwayBrush = tmpRailwayBrush;
        }
        #endregion MOUSE OVER

    }


}
