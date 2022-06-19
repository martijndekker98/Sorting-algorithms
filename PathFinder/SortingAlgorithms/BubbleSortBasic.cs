using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace PathFinder.SortingAlgorithms
{
    class BubbleSortBasic : BasisSortAlgorithm
    {
        public override string GetName() { return "Bubble sort"; }

        public override void Prepare(NumberSetNew nsni)
        {
            nsni.SetResumeIndexTo(nsni.Count);
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
            if (nsni.IsSorted) return;

            bool switchedNumbers = false;
            for (int i = 1; i < nsni.ResumeIndex; i++)
            {
                if (nsni.ASmallerThanB(i, i-1))
                {
                    nsni.Switch(i, i - 1);
                    switchedNumbers = true;
                }
            }
            nsni.UpdateResumeIndex(-1);
            if (!switchedNumbers)  nsni.HasBeenSorted(); 
        }
        

        public override void AdjustIndicator(NumberSetNew nsn, Rectangle indicator)
        {
            //Variables.WriteLine($"Adjust indicator {nsn.ResumeIndex}");
            Canvas.SetLeft(indicator, Canvas.GetLeft(nsn.GetCorrespondingRectangle(nsn.ResumeIndex-1)));
        }
    }
}
