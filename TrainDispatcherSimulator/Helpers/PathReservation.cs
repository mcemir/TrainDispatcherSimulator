using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TrainDispatcherSimulator.Controls;

namespace TrainDispatcherSimulator.Helpers
{
    public class PathReservation : Controler
    {
        private Stack<RailwayBase> path = new Stack<RailwayBase>();
        public void activate(){
            findPathBetweenSections();
        }

        private void findPathBetweenSections()
        {
            path.Push(startingPoint);
            while (path.Any())
            {
                RailwayBase current = path.Pop();
                
                if(finalPoint == current){
                    finalPoint.RailwayBrush = new SolidColorBrush(Color.FromArgb(255, 0, 222, 0));
                    return;
                }
                foreach (RailwayBase child in current.GetNeighbors())
                {
                    path.Push(child);
                }
            }

        }

    }



    
}
