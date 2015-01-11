using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Axes;


namespace TrainDispatcherSimulator.Data
{

    public class MainViewModel
    {

        public MainViewModel()
        {
            initializeData();
        }

        private void initializePlot(){
            
            var places = new LinearAxis();
            places.MajorGridlineStyle = LineStyle.Solid;
            places.MinorGridlineStyle = LineStyle.Dot;
            places.IsPanEnabled = false;
            places.IsZoomEnabled = false;

            var time = new LinearAxis();
            time.MajorGridlineStyle = LineStyle.Solid;
            time.MinorGridlineStyle = LineStyle.Dot;
            time.IsPanEnabled = false;
            time.IsZoomEnabled = false;
            time.Position = AxisPosition.Bottom;

            Model.Axes.Add(places);
            Model.Axes.Add(time);

        }

        private void initializeData(){
            this.Linija1 = new List<DataPoint>
                    {
                        new DataPoint(7.20, 8),
                        new DataPoint(7.22, 7),
                        new DataPoint(7.25, 7),
                        new DataPoint(7.34, 6),
                        new DataPoint(7.45, 5),
                        new DataPoint(7.53, 4),
                        new DataPoint(7.57, 4),
                        new DataPoint(7.65, 3),
                        new DataPoint(7.76, 2),
                        new DataPoint(7.87, 1),
                        new DataPoint(7.94, 0),
                    };

            this.Linija2 = new List<DataPoint>
                    {
                        new DataPoint(18.87, 8),
                        new DataPoint(19.35, 3),
                        new DataPoint(19.46, 3),
                        new DataPoint(19.54, 1),
                        new DataPoint(19.55, 0),
           
                    };

            this.Linija3 = new List<DataPoint>
                    {
                        new DataPoint(9.76, 8),
                        new DataPoint(9.38, 3),
                        new DataPoint(9.32, 3),
                        new DataPoint(9.20, 1),
                        new DataPoint(9.17, 0),
                    };

            this.Stations = new List<String>
                    {
                        "Tarčin",
                        "Pazarići",
                        "Zovik",
                        "Hadžići",
                        "Binježevo",
                        "Blažuj",
                        "Ilidža",
                        "Alipašin Most",
                        "Sarajevo"
                    };
        }
        public string Title { get; private set; }
        public PlotModel Model { get; private set; }
        public List<String> Stations { get; private set; }

        public IList<DataPoint> Linija1 { get; private set; }
        public IList<DataPoint> Linija2 { get; private set; }
        public IList<DataPoint> Linija3 { get; private set; }

    }
}
