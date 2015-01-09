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
    /// <summary>
    /// Interaction logic for RailwaySwitch5.xaml
    /// </summary>
    public partial class RailwaySwitch5 : RailwaySwitchBase
    {
        private bool straightUpperTrainOccupied = false;
        private bool straightLowerTrainOccupied = false;


        public RailwaySwitch5()
        {
            InitializeComponent();
        }



        #region PUBLIC METHODS


        public override void EnterRailway(Train train)
        {
            base.EnterRailway(train);

            if (State == RailwaySwitchState.Straight)
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

                if (found && !(indexR == 0 && indexL == 1))
                {
                    if (indexR == indexL)
                    {
                        State = RailwaySwitchState.Straight;
                        if (indexR == 0)
                            straightLowerPolygon.Fill = App.Current.Resources["RailwayBaseBrush"] as SolidColorBrush;
                        else
                            straightUpperPolygon.Fill = App.Current.Resources["RailwayBaseBrush"] as SolidColorBrush;
                    }
                    else
                        State = RailwaySwitchState.Sverve;

                    return true;
                }
                else
                {
                    Reset();
                    return false;
                }

            }

            return false;
        }

        public override void Reset()
        {
            base.Reset();
            straightLowerPolygon.SetBinding(Polygon.FillProperty, new Binding("RailwayBrush"));
            straightUpperPolygon.SetBinding(Polygon.FillProperty, new Binding("RailwayBrush"));
        }



        // Ovo nije dobro treba mijenjat
        public override RailwayBase GetRightRailway()
        {
            if (State == RailwaySwitchState.Straight)
            {
                if (straightUpperTrainOccupied)
                    return RightRailways[0];
                else
                    return RightRailways[1];
            }
            else
            {
                return RightRailways[1];
            }
        }

        // Ovo nije dobro treba mijenjat
        public override RailwayBase GetLeftRailway()
        {
            if (State == RailwaySwitchState.Straight)
            {
                if (straightUpperTrainOccupied)
                    return LeftRailways[0];
                else
                    return LeftRailways[1];
            }
            else
            {
                return LeftRailways[0];
            }
        }
        #endregion PUBLIC METHODS
    }
}
