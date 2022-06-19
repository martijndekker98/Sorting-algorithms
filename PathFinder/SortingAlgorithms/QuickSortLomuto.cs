using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace PathFinder.SortingAlgorithms
{
    class QuickSortLomuto : BasisSortAlgorithm
    {
        private Stack<Tuple<int, int>> Boundaries = new Stack<Tuple<int, int>>();
        private int High = 0;

        public override string GetName() { return "Quick sort (Lomuto)"; }

        public override void Prepare(NumberSetNew nsni)
        {
            Boundaries.Push(new Tuple<int, int>(0, nsni.Count-1));
            //Variables.WriteLine($"Start order: {((NumberSetNewInt)nsni).ListToString()}");
        }

        public override void Sort(NumberSetNew nsni)
        {
            while (!nsni.IsSorted)
            {
                Step(nsni);
            }
        }

        public override void Step(NumberSetNew nsni)
        {
            if (Boundaries.Count == 0) return;

            Tuple<int, int> bounds = Boundaries.Pop();
            int low = bounds.Item1;
            int high = bounds.Item2;
            //Variables.WriteLine($"Step, low {low}, h: {high}");
            int p = Partition(nsni, low, high);
            //Variables.WriteLine($"Step, p = {p}");

            if (p + 1 < high && p < nsni.Count) Boundaries.Push(new Tuple<int, int>(p + 1, high));
            if (p - 1 > low && p > -1) Boundaries.Push(new Tuple<int, int>(low, p-1));
            //Variables.WriteLine($"Current order: {((NumberSetNewInt)nsni).ListToString()}");
            
            if (Boundaries.Count == 0) nsni.HasBeenSorted();
            High = p;
        }

        // Returns an Int, h, that indicates the final position of the pivot (the last element/nsn[high])
        // where all numbers in front of h are <= nsn[h] and numbers after h are larger
        public int Partition(NumberSetNew nsn, int low, int high)
        {
            // The final position of the pivot element
            int h = low - 1;
            for (int i = low; i < high; i++)
            {
                if (nsn.ASmallerOrEqualB(i, high))
                {
                    h++;
                    // Switch the elements
                    nsn.Switch(h, i);
                }
            }
            h++;
            // Switch the pivot element (pos. high) to its final position
            nsn.Switch(h, high);
            return h;
        }


        public override void AdjustIndicator(NumberSetNew nsn, Rectangle indicator)
        {
            //Variables.WriteLine($"Adjust indicator {nsn.ResumeIndex}");
            Canvas.SetLeft(indicator, Canvas.GetLeft(nsn.GetCorrespondingRectangle(High)));
        }
    }
}
