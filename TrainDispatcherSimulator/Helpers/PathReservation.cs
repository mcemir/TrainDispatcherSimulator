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
        private static PathReservation instance;
        public static PathReservation Instance
        {
            get
            {
                if (instance == null)
                    instance = new PathReservation();

                return instance;
            }
        }

        public List<RailwayBase> Railways = new List<RailwayBase>();


        public RailwayBase firstPoint { get; set; }
        public RailwayBase secondPoint { get; set; }

        private RailwayBase startingPoint;
        private RailwayBase finalPoint;
        public PathDirection pathDirection { get; set; }
        public Boolean railwayClear { get; set; }

        private HashSet<RailwayBase> visitedSections = new HashSet<RailwayBase>();
        private Dictionary<RailwayBase, RailwayBase> parentSections = new Dictionary<RailwayBase, RailwayBase>();
        private Queue<RailwayBase> sectionQueue = new Queue<RailwayBase>();

        public PathReservation()
        {
            pathDirection = PathDirection.Both;
            railwayClear = true;
        }


        public void activate(){

            if (firstPoint == null || secondPoint == null || firstPoint == secondPoint) 
                return;

            if (firstPoint.GetType() != typeof(RailwaySection) || secondPoint.GetType() != typeof(RailwaySection)) 
                return;

            if (!railwayClear) 
                return;

            startingPoint = firstPoint;
            finalPoint = secondPoint;
            int distance = Grid.GetColumn(startingPoint) - Grid.GetColumn(finalPoint);
            pathDirection = distance >= 0 ? PathDirection.RightToLeft : PathDirection.LeftToRight;

            if (findPathBetweenSections())
            {
                Controller.Instance.Log("New railway path reserved!", LogType.Info);
                iluminatePath();
                railwayClear = false;
            }
            
            sectionQueue.Clear();
            visitedSections.Clear();
        }

        public void clearPath()
        {
            
            if (!parentSections.Any()) return;
            //Log
            Controller.Instance.Log("Reserved railway path cleared!", LogType.Info);
            RailwayBase current = finalPoint;
            while (current != startingPoint)
            {
                current.Reset();
                current = parentSections[current]; 
            }
            current.Reset();
            parentSections.Clear();
            railwayClear = true;

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

                current.Reserve(previous, next, true);

                previous = current;
                current = next;
            }
            current.Reserve(previous, null, true);
        }

        private bool findPathBetweenSections()
        {
            sectionQueue.Enqueue(startingPoint);

            while (sectionQueue.Any())
            {
                
                RailwayBase current = sectionQueue.Dequeue();

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
                    sectionQueue.Enqueue(child);
                }
            }
            return false;
        }






        // ==============================================================



        private List<RailwayBase> getPathBetween(RailwayBase first, RailwayBase last, bool highlight = false)
        {
            List<RailwayBase> path = new List<RailwayBase>();
            Queue<RailwayBase> queue = new Queue<RailwayBase>();
            HashSet<RailwayBase> visited = new HashSet<RailwayBase>();
            Dictionary<RailwayBase, RailwayBase> parent = new Dictionary<RailwayBase, RailwayBase>();

            int distance = Grid.GetColumn(first) - Grid.GetColumn(last);
            string direction = distance >= 0 ? "RightToLeft" : "LeftToRight";

            queue.Enqueue(first);

            while (queue.Any())
            {
                RailwayBase railway = queue.Dequeue();

                if (!visited.Add(railway))
                    continue;

                if (last == railway)
                    break;

                List<RailwayBase> neighbors = new List<RailwayBase>();
                if (direction == "RightToLeft")
                    neighbors = railway.LeftRailways.Where(n => !visitedSections.Contains(n)).ToList();
                else
                    neighbors = railway.RightRailways.Where(n => !visitedSections.Contains(n)).ToList();


                foreach (RailwayBase child in neighbors)
                {
                    parent[child] = railway;
                    queue.Enqueue(child);
                }                
            }


            // Create path
            RailwayBase current = last;
            RailwayBase previous = null;
            RailwayBase next = null;

            try
            {
                while (current != first)
                {
                    next = parent[current];

                    path.Add(current);
                    if (highlight)
                    {
                        bool canReserve = current.Reserve(previous, next, true);
                        if (!canReserve)
                        {
                            Reset(path);
                            return new List<RailwayBase>();
                        }

                    }

                    previous = current;
                    current = next;
                }
                path.Add(current);
                if (highlight)
                    current.Reserve(previous, null, true);
            }
            catch (Exception)
            {
                // Nije se pronašla putanja
            }
            

            return path;
        }


        public List<RailwayBase> Highligh(RailwayBase first, RailwayBase last)
        {
            List<RailwayBase> path = new List<RailwayBase>();

            // Ukoliko je sekcija ili privola
            if (first != null && last != null &&
                first != last &&
                (first is RailwaySection ||  first is RailwayPrivola) &&
                (last is RailwaySection || last is RailwayPrivola))
            {
                path = getPathBetween(first, last, true);
            }


            return path;
        }

        public void Reset()
        {
            Reset(Railways);
        }

        public void Reset(List<RailwayBase> railways)
        {
            if (railways != null)
            {
                foreach (RailwayBase railway in railways)
                    railway.Reset();
            }
        }
    }   
}
