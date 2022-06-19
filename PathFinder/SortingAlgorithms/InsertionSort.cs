using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace PathFinder.SortingAlgorithms
{
    class InsertionSortAlgorithm : BasisSortAlgorithm
    {
        public override string GetName() { return "Insertion sort"; }

        public override void Prepare(NumberSetNew nsni) { nsni.UpdateResumeIndex(); }

        public override void Sort(NumberSetNew nsni)
        {
            while (nsni.ResumeIndex < nsni.Count)
            {
                Step(nsni);
            }
        }

        public override void Step(NumberSetNew nsni)
        {
            //Variables.WriteLine($"Insertion sort step! {nsni.ResumeIndex}");
            if (nsni.ResumeIndex == 0)
            {
                nsni.UpdateResumeIndex();
                Variables.WriteLine($"{nsni.ResumeIndex}");
                return;
            }
            else if (nsni.ResumeIndex == nsni.Count) return;
            // Find the (index of) the smallest element in the unsorted part of the set
            int minIndex = FindIndexForSwitch(nsni);

            // Places the smallest element at the beginning of the unsorted part of the set
            SwitchBefore(nsni, minIndex);

            // Update the ResumeIndex, to indicate thet the unsorted part has shrunk
            nsni.UpdateResumeIndex();

            // If it has reached the end then the set is sorted
            if (nsni.ResumeIndex == nsni.Count) nsni.HasBeenSorted();
        }

        private static int FindIndexForSwitch(NumberSetNew nsni)
        {
            for (int i = nsni.ResumeIndex - 1; i > -1; i--)
            {
                //if (!(nsni[nsni.ResumeIndex] < nsni[i])) return i + 1;
                if (!(nsni.ASmallerThanB(nsni.ResumeIndex, i))) return i + 1;
            }
            return 0;
        }
        private static void SwitchBefore(NumberSetNew nsni, int stop)
        {
            for (int i = nsni.ResumeIndex; i > stop; i--)
            {
                nsni.Switch(i, i - 1);
            }
        }


        public override void AdjustIndicator(NumberSetNew nsn, Rectangle indicator)
        {
            Canvas.SetLeft(indicator, Canvas.GetLeft(nsn.GetCorrespondingRectangle(nsn.ResumeIndex - 1)));
        }
    }










    class InsertionSort
    {
        public static void InsertionSortSetPrepare(NumberSetNewIntegral nsni) { nsni.UpdateResumeIndex(); }

        public static void InsertionSortSet(NumberSetNewIntegral nsni)
        {
            while (nsni.ResumeIndex < nsni.Count)
            {
                InsertionSortSetStep(nsni);
            }
        }

        public static void InsertionSortSetStep(NumberSetNewIntegral nsni)
        {
            if (nsni.ResumeIndex == 0)
            {
                nsni.UpdateResumeIndex();
                return;
            }
            // Find the (index of) the smallest element in the unsorted part of the set
            int minIndex = FindIndexForSwitch(nsni);

            // Places the smallest element at the beginning of the unsorted part of the set
            SwitchBefore(nsni, minIndex);

            // Update the ResumeIndex, to indicate thet the unsorted part has shrunk
            nsni.UpdateResumeIndex();
        }

        private static int FindIndexForSwitch(NumberSetNewIntegral nsni)
        {
            for (int i = nsni.ResumeIndex - 1; i > -1; i--)
            {
                if (!(nsni[nsni.ResumeIndex] < nsni[i])) return i + 1;
            }
            return 0;
        }
        private static void SwitchBefore(NumberSetNewIntegral nsni, int stop)
        {
            for (int i = nsni.ResumeIndex; i > stop; i--)
            {
                nsni.SwitchNew(i, i - 1);
            }
        }

        // 6, 7 | 3
        // 
    }
}
