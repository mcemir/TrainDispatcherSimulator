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


        // Ovo nije dobro treba mijenjat
        public override RailwayBase GetRightRailway(RailwayBase referent = null)
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
        public override RailwayBase GetLeftRailway(RailwayBase referent = null)
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
