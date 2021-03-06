﻿using System;
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
using TrainDispatcherSimulator.Helpers;

namespace TrainDispatcherSimulator.Controls
{
    public enum SemaphoreSignalType { Red, Yellow, YellowYellow, Green };        // Vrste signalizacije na semaforu

    public partial class SemaphoreSignal : UserControl
    {
        

        //Signal type on the manoeuvre semaphore has only two states.
        //Active - manoeuvre action in progress
        //NotActive - traffic is controled by regular semaphores
        public enum ManueuvreSignalStyle { Active, NotActive};

        public enum SemaphoreOrientation { Left, Right };                   // Ulazni ili izlazni semafor


        // When the state changes this event is fired
        public event EventHandler StateChanged;




        #region PROPERTIES
        private SemaphoreSignalType signal = SemaphoreSignalType.Red;     // Inicijalno crvena
        private ManueuvreSignalStyle manueuvreSignal = ManueuvreSignalStyle.NotActive; // ManueuvareSignal is initaly disabled

        public SemaphoreSignalType Signal
        {
            get {return signal;}
            set
            {
                signal = value;

                // Promijeni boje
                if (value == SemaphoreSignalType.Red)
                {
                    upperSignal.Visibility = Visibility.Hidden;
                    upperSignal.Fill = new SolidColorBrush(Colors.LightGray);
                    lowerSignal.Fill = new SolidColorBrush(Colors.Red);
                }
                else if (value == SemaphoreSignalType.Yellow)
                {
                    upperSignal.Visibility = Visibility.Hidden;
                    upperSignal.Fill = new SolidColorBrush(Colors.LightGray);
                    lowerSignal.Fill = new SolidColorBrush(Colors.Yellow);
                }
                else if (value == SemaphoreSignalType.YellowYellow)
                {
                    upperSignal.Visibility = Visibility.Visible;
                    upperSignal.Fill = new SolidColorBrush(Colors.Yellow);
                    lowerSignal.Fill = new SolidColorBrush(Colors.Yellow);
                }
                else
                {
                    upperSignal.Visibility = Visibility.Hidden;
                    upperSignal.Fill = new SolidColorBrush(Colors.LightGray);
                    lowerSignal.Fill = new SolidColorBrush(Colors.LightGreen);
                }

                if (StateChanged != null)
                {
                    StateChanged(this, new EventArgs());  // Raise state changed event3
                }
            }
        }

        public ManueuvreSignalStyle ManueuvreSignal
        {
            get { return  manueuvreSignal; }
            set
            {
                manueuvreSignal = value;

                if (value == ManueuvreSignalStyle.Active)
                {
                    manoeuvreSignal.Fill = new SolidColorBrush(Colors.LightGreen);
                }
                else
                {
                    manoeuvreSignal.Fill = new SolidColorBrush(Colors.DarkGray);
                }
            }
        }        



        // Definiše da li je semafor ulazni ili izlazni
        public SemaphoreOrientation Orientation
        {
            get { return (SemaphoreOrientation)GetValue(OrientationProperty); }
            set { SetValue(OrientationProperty, value); }
        }
        public static readonly DependencyProperty OrientationProperty =
            DependencyProperty.Register("Orientation", typeof(SemaphoreOrientation), typeof(SemaphoreSignal), new PropertyMetadata(SemaphoreOrientation.Left, SemaphoreOrientation_Changed));

        private static void SemaphoreOrientation_Changed(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            (d as SemaphoreSignal).ApplyOrientation();
        }






        public Visibility ManuverSemaphoreVisibility
        {
            get { return (Visibility)GetValue(ManuverSemaphoreVisibilityProperty); }
            set { SetValue(ManuverSemaphoreVisibilityProperty, value); }
        }
        public static readonly DependencyProperty ManuverSemaphoreVisibilityProperty =
            DependencyProperty.Register("ManuverSemaphoreVisibility", typeof(Visibility), typeof(SemaphoreSignal), new PropertyMetadata(Visibility.Collapsed));

        

        
        #endregion PROPERTIES


        public SemaphoreSignal()
        {
            InitializeComponent();
        }




        #region PUBLIC METHODS
        public void ApplyOrientation()
        {
            if (Orientation == SemaphoreOrientation.Right)
            {
                this.LayoutTransform = new RotateTransform(180);
            }
            else
            {
                this.LayoutTransform = new RotateTransform(0);
            }
        }
        #endregion PUBLIC METHODS




        #region EVENT HANDLERS



        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            semaphorePopup.IsOpen = !semaphorePopup.IsOpen;
        }

        private void manueuvrePopup_MouseUp(object sender, MouseButtonEventArgs e)
        {
            //manueuvreSemaphorePopup.IsOpen = !manueuvreSemaphorePopup.IsOpen;

            if (ManueuvreSignal == ManueuvreSignalStyle.NotActive)
                ManueuvreSignal = ManueuvreSignalStyle.Active;
            else
                ManueuvreSignal = ManueuvreSignalStyle.NotActive;
        }




        private void redSignal_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Signal = SemaphoreSignalType.Red;
            Controller.Instance.Log("Semaphore <" + this.Name + "> manually changed to RED" , LogType.Info);
        }

        private void yellowSignal_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Signal = SemaphoreSignalType.Yellow;
            Controller.Instance.Log("Semaphore <" + this.Name + "> manually changed to YELLOW", LogType.Info);
        }
        private void yellowYellowSignal_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Signal = SemaphoreSignalType.YellowYellow;
            Controller.Instance.Log("Semaphore <" + this.Name + "> manually changed to YELLOW_YELLOW", LogType.Info);
        }
        private void greenSignal_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Signal = SemaphoreSignalType.Green;
            Controller.Instance.Log("Semaphore <" + this.Name + "> manually changed to GREEN", LogType.Info);
        }

        private void notActiveSignal_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ManueuvreSignal = ManueuvreSignalStyle.NotActive;
            Controller.Instance.Log("Semaphore <" + this.Name + "> manually changed to NotActive", LogType.Info);
        }

        private void activeSignal_MouseUp(object sender, MouseButtonEventArgs e)
        {
            ManueuvreSignal = ManueuvreSignalStyle.Active;
            Controller.Instance.Log("Semaphore <" + this.Name + "> manually changed to Active", LogType.Info);
        }





        
        #endregion EVENT HANDLERS

        
    }
}
