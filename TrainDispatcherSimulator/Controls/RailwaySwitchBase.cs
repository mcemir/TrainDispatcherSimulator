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
    public class RailwaySwitchBase : RailwayBase
    {
        public enum RailwaySwitchState { Straight, Sverve };


        #region PROPERTIES

        public RailwaySwitchState State
        {
            get { return (RailwaySwitchState)GetValue(StateProperty); }
            set { SetValue(StateProperty, value); }
        }
        public static readonly DependencyProperty StateProperty =
            DependencyProperty.Register("State", typeof(RailwaySwitchState), typeof(RailwaySwitchBase), new PropertyMetadata(RailwaySwitchState.Straight, stateChanged));





        public Visibility RailwaySwitchStraightVisibility
        {
            get { return (Visibility)GetValue(RailwaySwitchStraightVisibilityProperty); }
            set { SetValue(RailwaySwitchStraightVisibilityProperty, value); }
        }
        public static readonly DependencyProperty RailwaySwitchStraightVisibilityProperty =
            DependencyProperty.Register("RailwaySwitchStraightVisibility", typeof(Visibility), typeof(RailwayBase), new PropertyMetadata(Visibility.Visible));




        public Visibility RailwaySwitchSverveVisibility
        {
            get { return (Visibility)GetValue(RailwaySwitchSverveVisibilityProperty); }
            set { SetValue(RailwaySwitchSverveVisibilityProperty, value); }
        }
        public static readonly DependencyProperty RailwaySwitchSverveVisibilityProperty =
            DependencyProperty.Register("RailwaySwitchSverveVisibility", typeof(Visibility), typeof(RailwayBase), new PropertyMetadata(Visibility.Collapsed));

        

        
               

        #endregion PROPERTIES







        #region INITIALIZATION
        public RailwaySwitchBase()
        {
            this.MouseUp += RailwaySwitchBase_MouseUp;
            this.Width = (double)App.Current.Resources["BlockDimensionD"];
            this.Height = (double)App.Current.Resources["BlockDoubleDimensionD"];
        }

        #endregion INITIALIZATION







        #region EVENT HANDLERS
        public event EventHandler StateChanged;

        private static void stateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((d as RailwaySwitchBase).StateChanged != null) {
                (d as RailwaySwitchBase).StateChanged(d, new EventArgs());  // Raise state changed event
            }
        }


        private void RailwaySwitchBase_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (State == RailwaySwitchState.Straight)
            {
                State = RailwaySwitchState.Sverve;
                RailwaySwitchStraightVisibility = Visibility.Collapsed;
                RailwaySwitchSverveVisibility = Visibility.Visible;
            }
            else
            {
                State = RailwaySwitchState.Straight;
                RailwaySwitchStraightVisibility = Visibility.Visible;
                RailwaySwitchSverveVisibility = Visibility.Collapsed;
            }
        }
        #endregion EVENT HANDLERS
    }
}
