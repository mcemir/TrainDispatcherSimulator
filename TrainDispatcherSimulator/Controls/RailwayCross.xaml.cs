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

namespace TrainDispatcherSimulator.Controls
{
    public enum RailwayCrossState { Straight, SverveFirst, SverveSecond };
    public partial class RailwayCross : RailwayBase
    {
        private bool straightUpperTrainOccupied = false;
        private bool straightLowerTrainOccupied = false;

        #region INITIALIZATION
        public RailwayCross()
        {
            InitializeComponent();

            this.MouseUp += RailwayCross_MouseUp;
            this.Width = (double)App.Current.Resources["BlockDimensionD"];
            this.Height = (double)App.Current.Resources["BlockDoubleDimensionD"];
        }
        #endregion INITIALIZATION




        #region PROPERTIES

        public RailwayCrossState State
        {
            get { return (RailwayCrossState)GetValue(StateProperty); }
            set 
            {
                if (Trains.Count > 0)
                    return;

                SetValue(StateProperty, value);

                if (value == RailwayCrossState.Straight)
                {
                    RailwayCrossStraightVisibility = Visibility.Visible;
                    RailwayCrossSverveFirstVisibility = Visibility.Collapsed;
                    RailwayCrossSverveSecondVisibility = Visibility.Collapsed;
                }
                else if (value == RailwayCrossState.SverveSecond)
                {
                    RailwayCrossStraightVisibility = Visibility.Collapsed;
                    RailwayCrossSverveFirstVisibility = Visibility.Collapsed;
                    RailwayCrossSverveSecondVisibility = Visibility.Visible;
                }
                else
                {
                    RailwayCrossStraightVisibility = Visibility.Collapsed;
                    RailwayCrossSverveFirstVisibility = Visibility.Visible;
                    RailwayCrossSverveSecondVisibility = Visibility.Collapsed;
                }
            }
        }
        public static readonly DependencyProperty StateProperty =
            DependencyProperty.Register("RailwayCrossState", typeof(RailwayCrossState), typeof(RailwayBase), new PropertyMetadata(RailwayCrossState.Straight, stateChanged));



        public Visibility RailwayCrossStraightVisibility
        {
            get { return (Visibility)GetValue(RailwayCrossStraightVisibilityProperty); }
            set { SetValue(RailwayCrossStraightVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RailwayCrossStraightVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RailwayCrossStraightVisibilityProperty =
            DependencyProperty.Register("RailwayCrossStraightVisibility", typeof(Visibility), typeof(RailwayBase), new PropertyMetadata(Visibility.Visible));

        

        public Visibility RailwayCrossSverveFirstVisibility
        {
            get { return (Visibility)GetValue(RailwayCrossSverveFirstVisibilityProperty); }
            set { SetValue(RailwayCrossSverveFirstVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RailwayCrossSverveFirstVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RailwayCrossSverveFirstVisibilityProperty =
            DependencyProperty.Register("RailwayCrossSverveFirstVisibility", typeof(Visibility), typeof(RailwayBase), new PropertyMetadata(Visibility.Collapsed));



        public Visibility RailwayCrossSverveSecondVisibility
        {
            get { return (Visibility)GetValue(RailwayCrossSverveSecondVisibilityProperty); }
            set { SetValue(RailwayCrossSverveSecondVisibilityProperty, value); }
        }

        // Using a DependencyProperty as the backing store for RailwayCrossSverveSecondVisibility.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RailwayCrossSverveSecondVisibilityProperty =
            DependencyProperty.Register("RailwayCrossSverveSecondVisibility", typeof(Visibility), typeof(RailwayBase), new PropertyMetadata(Visibility.Collapsed));
        #endregion PROPERTIES





        #region PUBLIC METHODS

        public void ToggleState()
        {
            if (State == RailwayCrossState.Straight)
            {
                State = RailwayCrossState.SverveFirst;
            }
            else if (State == RailwayCrossState.SverveFirst)
            {
                State = RailwayCrossState.SverveSecond;
            }
            else
            {
                State = RailwayCrossState.Straight;
            }
        }



        public override void EnterRailway(Train train)
        {
            base.EnterRailway(train);

            if (State == RailwayCrossState.Straight)
            {
                if (RightRailways[0].Trains.Contains(train) ||
                    LeftRailways[0].Trains.Contains(train))
                {
                    straightUpperTrainOccupied = true;
                    straightLowerPolygon.Fill = App.Current.Resources["RailwayBaseBrush"] as SolidColorBrush;
                }
                else
                {
                    straightLowerTrainOccupied = true;
                    straightUpperPolygon.Fill = App.Current.Resources["RailwayBaseBrush"] as SolidColorBrush;
                }
            }
        }



        public override bool Reserve(RailwayBase previousRailway, RailwayBase nextRailway, bool highlight = false)
        {
            if (base.Reserve(previousRailway, nextRailway, highlight))
            {
                if (previousRailway == null && nextRailway == null)
                    return true;

                int indexR = 0;
                int indexL = 0;
                bool found = false;

                if (RightRailways.Contains(previousRailway) && LeftRailways.Contains(nextRailway))
                {
                    indexR = RightRailways.IndexOf(previousRailway);
                    indexL = LeftRailways.IndexOf(nextRailway);
                    found = true;
                }
                else if (RightRailways.Contains(nextRailway) && LeftRailways.Contains(previousRailway))
                {
                    indexR = RightRailways.IndexOf(nextRailway);
                    indexL = LeftRailways.IndexOf(previousRailway);
                    found = true;
                }

                if (found)
                {
                    if (indexR == indexL)
                        State = RailwayCrossState.Straight;
                    else if (indexL == 0 && indexR == 1)
                        State = RailwayCrossState.SverveFirst;
                    else
                        State = RailwayCrossState.SverveSecond;
                }
                else
                {
                    Reset();
                }

                return found;

            }

            return false;
        }


        public override void Reset()
        {
            base.Reset();
            State = RailwayCrossState.Straight;
        }



        public override RailwayBase GetRightRailway()
        {
            if (State == RailwayCrossState.Straight)
            {
                if (straightUpperTrainOccupied)
                    return RightRailways[0];
                else
                    return RightRailways[1]; 
            }
            else if (State == RailwayCrossState.SverveFirst)
            {
                return RightRailways[1];
            }
            else
            {
                return RightRailways[0];
            }
        }

        public override RailwayBase GetLeftRailway()
        {
            if (State == RailwayCrossState.Straight) 
            {
                if (straightUpperTrainOccupied)
                    return LeftRailways[0];
                else
                    return LeftRailways[1]; 
            }
            else if (State == RailwayCrossState.SverveFirst)
            {
                return LeftRailways[0];
            }
            else
            {
                return LeftRailways[1];
            }
        }
        #endregion PUBLIC METHODS








        #region PRIVATE METHODS

        protected override void trainLeft(Train train)
        {
            if (straightUpperTrainOccupied)
            {
                straightUpperTrainOccupied = false;
                straightLowerPolygon.SetBinding(Polygon.FillProperty, new Binding("RailwayBrush"));
            }
            else
            {
                straightLowerTrainOccupied = false;
                straightUpperPolygon.SetBinding(Polygon.FillProperty, new Binding("RailwayBrush"));
            }
        }
        #endregion PRIVATE METHODS







        #region EVENT HANDLERS
        public event EventHandler StateChanged;

        private static void stateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((d as RailwayCross).StateChanged != null)
            {
                (d as RailwayCross).StateChanged(d, new EventArgs());  // Raise state changed event
            }
        }


        private void RailwayCross_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ToggleState();
        }
        #endregion EVENT HANDLERS
    }
}
