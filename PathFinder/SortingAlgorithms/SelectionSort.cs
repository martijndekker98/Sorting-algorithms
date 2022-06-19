using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace PathFinder.SortingAlgorithms
{
    class SelectionSortAlgorithm : BasisSortAlgorithm
    {
        public override string GetName() { return "Selection sort"; }

        public override void Prepare(NumberSetNew nsni) {  }

        public override void Sort(NumberSetNew nsni)
        {
            while (nsni.ResumeIndex < nsni.Count)
            {
                Step(nsni);
            }
        }

        public override void Step(NumberSetNew nsni)
        {
            if (nsni.ResumeIndex == nsni.Count) return;
            // Find the (index of) the smallest element in the unsorted part of the set
            int minIndex = HelperFunctions.FindIndexSmallest(nsni);

            Variables.WriteLine($"Minindex: {minIndex}, {nsni.ResumeIndex}");
            // Places the smallest element at the beginning of the unsorted part of the set
            nsni.Switch(minIndex, nsni.ResumeIndex);

            // Update the ResumeIndex, to indicate thet the unsorted part has shrunk
            nsni.UpdateResumeIndex();

            // If it has reached the end then the set is sorted
            if (nsni.ResumeIndex == nsni.Count) nsni.HasBeenSorted();
        }
        
        public override void AdjustIndicator(NumberSetNew nsn, Rectangle indicator)
        {
            Canvas.SetLeft(indicator, Canvas.GetLeft(nsn.GetCorrespondingRectangle(nsn.ResumeIndex-1)));
        }
    }





    class SelectionSort
    {
        public static void SelectionSortSet(NumberSetNewInt nsni)
        {
            while (nsni.ResumeIndex < nsni.Count)
            {
                SelectionSortSetStep(nsni);
            }
        }
        public static void SelectionSortSetStep(NumberSetNewInt nsni)
        {
            // Find the (index of) the smallest element in the unsorted part of the set
            int minIndex = HelperFunctions.FindIndexSmallest(nsni);

            // Places the smallest element at the beginning of the unsorted part of the set
            nsni.SwitchNew(minIndex, nsni.ResumeIndex);

            // Update the ResumeIndex, to indicate thet the unsorted part has shrunk
            nsni.UpdateResumeIndex();
        }











        private static void TestThingy()
        {
            List<int> lijsti = Enumerable.Range(0, 1000).ToList();
            List<float> lijst = (from number in lijsti select (float)number).ToList();
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 1000000; i++)
            {
                float b = lijst[i%1000] + i;
            }
            sw.Stop();
            Variables.WriteLine($"Time m1: {sw.ElapsedMilliseconds}");
            sw.Reset();

            sw.Start();

        }

        public static void SelectionSortList(NumberSet4 numberSet)
        {
            MyGenClass<int> a = new MyGenClass<int>(3);
            MyGenClass<int> b = new MyGenClass<int>(4);
            //bool ans = a < b;
        }

        //private static int FindSmallest<T>(List<NewDataPoint<T>> inp) where T : IComparable
        //{
        //    int ans = 0;
        //    for (int i = 1; i < inp.Count; i++)
        //    {
        //        if (inp[i].Value)
        //    }
        //    return ans;
        //}


    }

    class MyGenClass<T> where T : IComparable<T>
    {
        T obj; // internal field - the instance of type T
        
        public MyGenClass(T _obj)
        {
            obj = _obj;
        }

        // Properties to access to the internal field
        public T Obj
        {
            get { return obj; }
            set { obj = value; }
        }

        // Demo method that outputs the result of comparing
        // the current value of obj with other.obj
        public void GreaterThan(T other)
        {
            if (obj.CompareTo(other) > 0)
                Console.WriteLine("obj > other.obj");
            else
            if (obj.CompareTo(other) < 0)
                Console.WriteLine("obj < other.obj");
            else
                Console.WriteLine("obj == other.obj");
        }
    }

    // A class suitable for comparing instances in generic classes.
    // The class defines a certain number. The class implements the IComparable<T> interface
    class Number : IComparable<Number>
    {
        // Internal field
        double num = 0;

        // Constructor
        public Number(double num)
        {
            this.num = num;
        }

        // Inner field access property
        public double Num
        {
            get { return num; }
            set { num = value; }
        }

        // Implementation of the CompareTo() method from the IComparable<Number> interface
        public int CompareTo(Number other)
        {
            if (this.num > other.num)
                return 1;
            if (this.num < other.num)
                return -1;
            return 0;
        }
    }
}
