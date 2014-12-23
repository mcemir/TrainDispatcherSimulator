using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainDispatcherSimulator.Controls
{
    public enum TrainType { Passenger, Freight };
    public enum TrainOrientation { Left, Right };



    public class Train
    {
        public static int WagonLength = 20;      // Duzina vagona je 20 metara


        #region PROPERTIES

        public TrainType Type { get; set; }
        public TrainOrientation Orientation { get; set; }
        public string Name { get; set; }
        public int MaxSpeed { get; set; }       // Koristi se za proračun koliko dugo će se voziti kroz railway (km/h)
        public int WagonCount { get; set; }     // Duzina voza, ukoliko se bude animirao ovo će biti referentan dužina (jedan vagon je fiksne dužine)        
        public int Length { get { return WagonCount * WagonLength; } }
        
        #endregion PROPERTIES


        #region INITIALIZATION

        public Train()
        {

        }
        #endregion INITIALIZATION

    }
}
