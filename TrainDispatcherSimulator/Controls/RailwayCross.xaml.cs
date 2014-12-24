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

        #endregion PUBLIC METHODS






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
