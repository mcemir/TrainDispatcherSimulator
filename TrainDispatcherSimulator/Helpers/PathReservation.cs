﻿using System;
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
        private HashSet<RailwayBase> visitedSections = new HashSet<RailwayBase>();
        private Dictionary<RailwayBase, RailwayBase> parentSections = new Dictionary<RailwayBase, RailwayBase>();
        private Stack<RailwayBase> sectionStack = new Stack<RailwayBase>();

        public void activate(){

            findPathBetweenSections();
            iluminatePath();
            parentSections.Clear();
        }

        private void iluminatePath()
        {
            RailwayBase parent = parentSections[finalPoint];
            while (parent != startingPoint)
            {
                parent.RailwayBrush = parent.RailwayBrush = new SolidColorBrush(Color.FromArgb(255, 0, 222, 0));
                parent = parentSections[parent];
            }
        }

        private void findPathBetweenSections()
        {
            sectionStack.Push(startingPoint);

            while (sectionStack.Any())
            {
                
                RailwayBase current = sectionStack.Pop();

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
                    sectionStack.Push(child);
                }
            }

        }

    }



    
}
