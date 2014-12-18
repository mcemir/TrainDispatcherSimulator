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
    public class RailwayBase : UserControl
    {

        #region PROPERTIES

        public SolidColorBrush RailwayBrush
        {
            get { return (SolidColorBrush)GetValue(RailwayBrushProperty); }
            set { SetValue(RailwayBrushProperty, value); }
        }
        public static readonly DependencyProperty RailwayBrushProperty =
            DependencyProperty.Register("RailwayBrush", typeof(SolidColorBrush), typeof(RailwayBase), new PropertyMetadata((App.Current.Resources["RailwayBaseBrush"] as SolidColorBrush) ));

        #endregion PROPERTIES








        #region INITIALIZATION

        public RailwayBase()
        {
            DataContext = this;
            RailwayBrush = App.Current.Resources["RailwayBaseBrush"] as SolidColorBrush;

            this.MouseEnter += RailwayBase_MouseEnter;
            this.MouseLeave += RailwayBase_MouseLeave;
        }        

        #endregion INITIALIZATION







        #region EVENT HANDLERS

        void RailwayBase_MouseEnter(object sender, MouseEventArgs e)
        {
            RailwayBrush = App.Current.Resources["RailwayMouseOverBrush"] as SolidColorBrush;
        }

        void RailwayBase_MouseLeave(object sender, MouseEventArgs e)
        {
            RailwayBrush = App.Current.Resources["RailwayBaseBrush"] as SolidColorBrush;
        }

        #endregion EVENT HANDLERS
    }
}
