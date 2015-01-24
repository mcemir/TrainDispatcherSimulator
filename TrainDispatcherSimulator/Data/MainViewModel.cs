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
                        new DataPoint(18.00, 8),
                        new DataPoint(19.10, 3),
                        new DataPoint(19.46, 3),
                        new DataPoint(20.10, 0),
           
                    };

            this.Linija3 = new List<DataPoint>
                    {
                        new DataPoint(9.76, 8),
                        new DataPoint(9.38, 3),
                        new DataPoint(9.32, 3),
                        new DataPoint(9, 0),
                    };

            this.Linija4 = new List<DataPoint>
                    {
                        new DataPoint(8, 8),
                        new DataPoint(8.53, 3),
                        new DataPoint(9.50, 3),
                        new DataPoint(10.50, 0),
                    };

            this.Linija5 = new List<DataPoint>
                    {
                        new DataPoint(21, 8),
                        new DataPoint(20.20, 1),
                        new DataPoint(19.20, 1),
                        new DataPoint(19, 0),
                    };

            this.Linija6 = new List<DataPoint>
                    {
                        new DataPoint(12, 0),
                        new DataPoint(13, 3),
                        new DataPoint(14, 3),
                        new DataPoint(15, 8),
                    };

            this.Linija7 = new List<DataPoint>
                    {
                        new DataPoint(14, 8),
                        new DataPoint(14.50, 7),
                        new DataPoint(15, 7),
                        new DataPoint(16.30, 2),
                        new DataPoint(17, 2),
                        new DataPoint(17.50, 2),
                        new DataPoint(18.17, 0)
                    };

            this.Linija8 = new List<DataPoint>
                    {
                        new DataPoint(12.76, 8),
                        new DataPoint(11.38, 3),
                        new DataPoint(11.05, 3),
                        new DataPoint(10.50, 1),
                        new DataPoint(10.17, 0),
                    };

            this.Linija9 = new List<DataPoint>
                    {
                        new DataPoint(11, 8),
                        new DataPoint(11.10, 7),
                        new DataPoint(12.05, 7),
                        new DataPoint(13.50, 0),
                    };

            this.Linija10 = new List<DataPoint>
                    {
                        new DataPoint(17.50, 8),
                        new DataPoint(16.50, 3),
                        new DataPoint(15.50, 3),
                        new DataPoint(14.50, 0),
                    };

            this.pomocnaLinija1 = new List<DataPoint>
                    {
                        new DataPoint(6, 7.1),
                        new DataPoint(22, 7.1)

                    };
            this.pomocnaLinija2 = new List<DataPoint>
                    {
                        new DataPoint(6, 6.9),
                        new DataPoint(22, 6.9)
                    };

            this.pomocnaLinija3 = new List<DataPoint>
                    {
                        new DataPoint(6, 3.1),
                        new DataPoint(22, 3.1)
                    };

            this.pomocnaLinija4 = new List<DataPoint>
                    {
                       new DataPoint(6, 2.9),
                        new DataPoint(22, 2.9)
                    };

            this.pomocnaLinija5 = new List<DataPoint>
                    {
                        new DataPoint(6, 6.8),
                        new DataPoint(22, 6.8)
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


        public List<DataPoint> Linija4 { get; set; }

        public List<DataPoint> Linija5 { get; set; }

        public List<DataPoint> Linija6 { get; set; }

        public List<DataPoint> Linija7 { get; set; }

        public List<DataPoint> Linija8 { get; set; }

        public List<DataPoint> Linija9 { get; set; }

        public List<DataPoint> Linija10 { get; set; }

        public List<DataPoint> pomocnaLinija1 { get; set; }

        public List<DataPoint> pomocnaLinija2 { get; set; }

        public List<DataPoint> pomocnaLinija3 { get; set; }

        public List<DataPoint> pomocnaLinija4 { get; set; }

        public List<DataPoint> pomocnaLinija5 { get; set; }
    }
}
