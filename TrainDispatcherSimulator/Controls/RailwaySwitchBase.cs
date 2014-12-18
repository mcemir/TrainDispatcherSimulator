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
               

        #endregion PROPERTIES







        #region INITIALIZATION
        public RailwaySwitchBase()
        {
            this.MouseUp += RailwaySwitchBase_MouseUp;
        }

        #endregion INITIALIZATION







        #region EVENT HANDLERS
        public event EventHandler StateChanged;

        private static void stateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if ((d as RailwaySwitchBase).StateChanged != null) {
                (d as RailwaySwitchBase).StateChanged(d, new EventArgs());
            }
        }


        private void RailwaySwitchBase_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (State == RailwaySwitchState.Straight)
                State = RailwaySwitchState.Sverve;
            else
                State = RailwaySwitchState.Straight;
        }
        #endregion EVENT HANDLERS
    }
}
