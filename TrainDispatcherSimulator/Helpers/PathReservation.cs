using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using TrainDispatcherSimulator.Controls;

namespace TrainDispatcherSimulator.Helpers
{
    //TO DO
    //Pogresno rezervise path ka ovoj sekciji sa najkracim putem
    //Nalazi se izmedju ovih dugih sa peronima.
    public class PathReservation
    {
        public RailwayBase startingPoint { get; set; }
        public RailwayBase finalPoint { get; set; }
        public PathDirection pathDirection { get; set; }

        private HashSet<RailwayBase> visitedSections = new HashSet<RailwayBase>();
        private Dictionary<RailwayBase, RailwayBase> parentSections = new Dictionary<RailwayBase, RailwayBase>();
        private Queue<RailwayBase> sectionStack = new Queue<RailwayBase>();

        public PathReservation()
        {
            pathDirection = PathDirection.Both;
        }


        public void activate(){
            
            if (startingPoint == null || finalPoint == null || startingPoint==finalPoint) return;
            if (startingPoint.GetType() != typeof(RailwaySection) || finalPoint.GetType() != typeof(RailwaySection)) return;

            int distance = Grid.GetColumn(startingPoint) - Grid.GetColumn(finalPoint);
            pathDirection = distance >= 0 ? PathDirection.RightToLeft : PathDirection.LeftToRight;

            if(findPathBetweenSections())
                iluminatePath();

            //parentSections.Clear();
            sectionStack.Clear();
            visitedSections.Clear();
            }

        public void clearPath()
        {
            RailwayBase current = finalPoint;
            while (current != startingPoint)
            {
                current.Reset();
                current = parentSections[current]; 
            }
            current.Reset();
            parentSections.Clear();

        }

        private void iluminatePath()
        {
            //startingPoint.RailwayBrush =  
            //finalPoint.RailwayBrush = new SolidColorBrush(Color.FromArgb(255, 0, 0, 200));
            RailwayBase current = finalPoint;
            RailwayBase previous = null;
            RailwayBase next = null;

            while (current != startingPoint)
            {
                next = parentSections[current];

                current.Reserve(previous, next);

                previous = current;
                current = next;
            }
            current.Reserve(previous, null);
        }

        private bool findPathBetweenSections()
        {
            sectionStack.Enqueue(startingPoint);

            while (sectionStack.Any())
            {
                
                RailwayBase current = sectionStack.Dequeue();

                if (!visitedSections.Add(current))
                    continue;

                if (finalPoint == current)
                    return true;

                List<RailwayBase> neighbors = new List<RailwayBase>();
                if(pathDirection == PathDirection.RightToLeft)
                    neighbors = current.LeftRailways.Where(n => !visitedSections.Contains(n)).ToList();
                else if(pathDirection == PathDirection.LeftToRight)
                    neighbors = current.RightRailways.Where(n => !visitedSections.Contains(n)).ToList();


                foreach (RailwayBase child in neighbors)
                {
                    parentSections[child] = current;
                    sectionStack.Enqueue(child);
                }
            }
            return false;
        }
    }   
}
