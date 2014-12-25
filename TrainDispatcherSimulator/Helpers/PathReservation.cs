using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using TrainDispatcherSimulator.Controls;

namespace TrainDispatcherSimulator.Helpers
{
    public class PathReservation
    {
        public RailwayBase startingPoint { get; set; }
        public RailwayBase finalPoint { get; set; }


        private HashSet<RailwayBase> visitedSections = new HashSet<RailwayBase>();
        private Dictionary<RailwayBase, RailwayBase> parentSections = new Dictionary<RailwayBase, RailwayBase>();
        private Queue<RailwayBase> sectionStack = new Queue<RailwayBase>();

        public void activate(){
            if (startingPoint == null || finalPoint == null || startingPoint==finalPoint) return;
            findPathBetweenSections();
            iluminatePath();
            parentSections.Clear();
        }

        private void iluminatePath()
        {
            startingPoint.RailwayBrush = new SolidColorBrush(Color.FromArgb(255, 0, 0, 200));
            finalPoint.RailwayBrush = new SolidColorBrush(Color.FromArgb(255, 0, 0, 200));
            RailwayBase parent = parentSections[finalPoint];
            while (parent != startingPoint)
            {
                parent.RailwayBrush = parent.RailwayBrush = new SolidColorBrush(Color.FromArgb(255, 0, 222, 0));
                parent = parentSections[parent];
            }
        }

        private void findPathBetweenSections()
        {
            sectionStack.Enqueue(startingPoint);

            while (sectionStack.Any())
            {
                
                RailwayBase current = sectionStack.Dequeue();

                if (!visitedSections.Add(current))
                    continue;

                if (finalPoint == current)
                {
                    sectionStack.Clear();
                    visitedSections.Clear();
                    return;
                }

                List<RailwayBase> neighbors = current.GetNeighbors().Where(n => !visitedSections.Contains(n)).ToList();

                foreach (RailwayBase child in neighbors)
                {
                    parentSections[child] = current;
                    sectionStack.Enqueue(child);
                }
            }

        }

    }



    
}
