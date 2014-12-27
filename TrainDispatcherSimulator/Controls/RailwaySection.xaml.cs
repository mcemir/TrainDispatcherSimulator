﻿using System;
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

        public override bool Reserve(RailwayBase previousRailway, RailwayBase nextRailway)
        {
            if (base.Reserve(previousRailway, nextRailway))
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

            return false;
        }

        public override void Reset()
        {
            base.Reset();
            leftSemaphore.Signal = SemaphoreSignalType.Red;
            rightSemaphore.Signal = SemaphoreSignalType.Red;
        }

        #endregion PUBLIC METHODS



        #region EVENT HANDLERS


        #endregion EVENT HANDLERS






    }
}
