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

namespace TrainDispatcherSimulator.Controls
{
    public partial class RailwaySection : RailwayBase
    {


        #region PROPERTIES

        public Visibility LeftSemaphoreVisibility
        {
            get { return (Visibility)GetValue(LeftSemaphoreVisibilityProperty); }
            set { SetValue(LeftSemaphoreVisibilityProperty, value); }
        }
        public static readonly DependencyProperty LeftSemaphoreVisibilityProperty =
            DependencyProperty.Register("LeftSemaphoreVisibility", typeof(Visibility), typeof(RailwaySection), new PropertyMetadata(Visibility.Visible));


        

        public Visibility RightSemaphoreVisibility
        {
            get { return (Visibility)GetValue(RightSemaphoreVisibilityProperty); }
            set { SetValue(RightSemaphoreVisibilityProperty, value); }
        }
        public static readonly DependencyProperty RightSemaphoreVisibilityProperty =
            DependencyProperty.Register("RightSemaphoreVisibility", typeof(Visibility), typeof(RailwaySection), new PropertyMetadata(Visibility.Visible));







        public Visibility ManuverSemaphoreVisibility
        {
            get { return (Visibility)GetValue(ManuverSemaphoreVisibilityProperty); }
            set { SetValue(ManuverSemaphoreVisibilityProperty, value); }
        }
        public static readonly DependencyProperty ManuverSemaphoreVisibilityProperty =
            DependencyProperty.Register("ManuverSemaphoreVisibility", typeof(Visibility), typeof(RailwaySection), new PropertyMetadata(Visibility.Collapsed));




        #endregion PROPERTIES

        public RailwaySection()
        {
            InitializeComponent();
        }




        #region PUBLIC METHODS  

        public override bool Reserve(RailwayBase previousRailway, RailwayBase nextRailway, bool highlight = false)
        {
            if (base.Reserve(previousRailway, nextRailway, highlight) && !highlight)
            {
                if (previousRailway != null && (nextRailway == null || RightRailways.Contains(nextRailway)) && LeftRailways.Contains(previousRailway) && leftSemaphore.Visibility == Visibility.Visible)
                {
                    leftSemaphore.Signal = SemaphoreSignalType.Green;
                }
                else if (previousRailway != null && RightRailways.Contains(previousRailway) && (nextRailway == null || LeftRailways.Contains(nextRailway)) && rightSemaphore.Visibility == Visibility.Visible)
                {
                    rightSemaphore.Signal = SemaphoreSignalType.Green;
                }
            }

            return true;
        }

        public override void Reset()
        {
            base.Reset();
            leftSemaphore.Signal = SemaphoreSignalType.Red;
            rightSemaphore.Signal = SemaphoreSignalType.Red;
        }


        public override void EnterRailway(Train train)
        {
            base.EnterRailway(train);
            changeOrientationButton.Visibility = Visibility.Visible;
        }


        public override void LeaveRailway(Train train)
        {
            RailwayBase nextRailway = getNextRailway(train);

            if (nextRailway != null && validateSemaphoreSignal(train))
            {
                nextRailway.EnterRailway(train);
                startTimerLeaving(train);
                Trains.Remove(train);

                TrainNameLeftPanelVisibility = Visibility.Collapsed;
                TrainNameRightPanelVisibility = Visibility.Collapsed;
                changeOrientationButton.Visibility = Visibility.Collapsed;
            }

            
        }
        #endregion PUBLIC METHODS




        #region PRIVATE METHODS

        private bool validateSemaphoreSignal(Train train)
        {
            if (train.Orientation == TrainOrientation.Left && leftSemaphore.Visibility == Visibility.Visible && leftSemaphore.Signal == SemaphoreSignalType.Red)
                return false;

            if (train.Orientation == TrainOrientation.Right && rightSemaphore.Visibility == Visibility.Visible && rightSemaphore.Signal == SemaphoreSignalType.Red)
                return false;

            return true;
        }

        private void leaveRailway()
        {
            if ((leftSemaphore.Signal != SemaphoreSignalType.Red && Trains.First().Orientation == TrainOrientation.Left) ||
                (rightSemaphore.Signal != SemaphoreSignalType.Red && Trains.First().Orientation == TrainOrientation.Right) &&
                !drivingTimer.IsEnabled)
            {
                LeaveRailway(Trains.First());
            }
        }

        #endregion PRIVATE METHODS

        



        #region EVENT HANDLERS
        private void semaphore_StateChanged(object sender, EventArgs e)
        {
            if (Trains.Count > 0 && (sender as SemaphoreSignal).Signal != SemaphoreSignalType.Red)
            {
                leaveRailway();
            }
        }

        private void changeOrientationButton_Click(object sender, RoutedEventArgs e)
        {
            if (Trains.Count > 0)
            {
                TrainNameLeftPanelVisibility = Visibility.Collapsed;
                TrainNameRightPanelVisibility = Visibility.Collapsed;

                if (Trains.First().Orientation == TrainOrientation.Left)
                {
                    Trains.First().Orientation = TrainOrientation.Right;
                    TrainNameRightPanelVisibility = Visibility.Visible;
                }
                else
                {
                    Trains.First().Orientation = TrainOrientation.Left;
                    TrainNameLeftPanelVisibility = Visibility.Visible;
                }

                leaveRailway();
            }
        }

        #endregion EVENT HANDLERS

        






    }
}
