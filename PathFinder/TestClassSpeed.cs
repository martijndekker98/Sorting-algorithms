using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder
{
    class TestClassSpeed
    {
        public static void TestSpeed()
        {
            //TestNumberWrapper();   
            //TestIntWrapper_();
            //TestDPV2();

            IntWrapper iw = new IntWrapper(3);
            IntWrapper iw2 = new IntWrapper(-3);
            IntWrapper iw3 = new IntWrapper(46);
            IntWrapper iw4 = new IntWrapper(53);
            UintWrapper uw = new UintWrapper(13);
            IntegralWrapper www = new IntegralWrapper((sbyte)4);
            IntegralWrapper leeg = null;
            DataWrapper dw = new DataWrapper(4);
            Variables.WriteLine($"Compare 3 & 13 {iw.CompareTo(uw)}");
            Variables.WriteLine($"Compare 3 & 13 {iw.CompareTo(iw2)}");
            Variables.WriteLine($"Compare 3 & 13 {iw.CompareTo(leeg)}");

            List<IntegralWrapper> intss = new List<IntegralWrapper>() { iw, iw2, iw3, iw4 };
            NumberSetNewIntegral nsn = new NumberSetNewIntegral(intss, false);
            //foreach (IntegralWrapper i in intss) Variables.WriteLine($"Integralwrapper: {i.GetType()}, val: {i.Value}");\
            foreach (IntegralWrapper i in nsn) Variables.WriteLine($"Integralwrapper: {i.GetType()}, val: {i.Value}");
            nsn.Switch(1, 2);
            nsn.Switch(0, 3);
            foreach (IntegralWrapper i in nsn) Variables.WriteLine($"Integralwrapper: {i.GetType()}, val: {i.Value}");
            Variables.WriteLine($"Val index 1: {nsn[1]}");
            Variables.WriteLine($"Type nsn: {nsn.Lijst.GetType()} <> {nsn.Count}");



            //Variables.WriteLine($"groter? : {iw < intss[2]} {intss.GetType()}");
            //long longVal = long.MaxValue - 1;
            //IntegralWrapper longWrapper = new IntegralWrapper(longVal);
            //IntWrapper iw_ = new IntWrapper(Int32.MaxValue);
            //TestSpeedSwitching();

            //List<IntWrapper> intsN = new List<IntWrapper>() { iw, iw2, iw3, iw4 };
            //TestSpeedComparisons(intss, intsN);

            IntWrapper a1 = new IntWrapper(11);
            IntWrapper a2 = new IntWrapper(25);
            IntWrapper a3 = new IntWrapper(12);
            IntWrapper a4 = new IntWrapper(22);
            IntWrapper a5 = new IntWrapper(64);
            //NumberSetNewIntegral set1 = new NumberSetNewIntegral(new List<IntegralWrapper>() { a1, a2, a3, a4, a5 }, false);
            NumberSetNewInt set1 = new NumberSetNewInt(new List<IntWrapper>() { a5, a2, a4, a3, a1 }, false);
            TestSort(set1);
        }


        private static void TestSort(NumberSetNewInt nsni)
        {
            SortingAlgorithms.BasisSortAlgorithm bsa = new SortingAlgorithms.InsertionSortAlgorithm();
            Variables.WriteLine(nsni.ListToString());
            while (nsni.ResumeIndex < nsni.Count)
            {
                //SortingAlgorithms.InsertionSort.InsertionSortSetStep(nsni);
                bsa.Step(nsni);
                Variables.WriteLine(nsni.ListToString());
            }
        }



        private static void TestSpeedComparisons(List<IntegralWrapper> lijst, List<IntWrapper> lijst2)
        {
            int count = lijst.Count;
            int x = 0;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 1000000; i++)
            {
                if (lijst[i % count] < lijst[0]) x++;
            }
            sw.Stop();
            Variables.WriteLine($"Time: {sw.ElapsedMilliseconds}");
            sw.Reset();

            sw.Start();
            for (int i = 0; i < 1000000; i++)
            {
                //if (((IntWrapper)lijst[i % count]) < ((IntWrapper)lijst[0])) x++;
                if (lijst[i % count] < ((IntWrapper)lijst[0])) x++;
            }
            sw.Stop();
            Variables.WriteLine($"Time: {sw.ElapsedMilliseconds}");
            sw.Reset();

            sw.Start();
            for (int i = 0; i < 1000000; i++)
            {
                //if (((IntWrapper)lijst[i % count]) < ((IntWrapper)lijst[0])) x++;
                if (lijst2[i % count] < lijst2[0]) x++;
            }
            sw.Stop();
            Variables.WriteLine($"Time: {sw.ElapsedMilliseconds}");
        }

        private static void TestSpeedSwitching()
        {
            Stopwatch sw = new Stopwatch();

            IntWrapper iw_ = new IntWrapper(3);
            IntWrapper iw2_ = new IntWrapper(-3);
            IntWrapper iw3_ = new IntWrapper(46);
            IntWrapper iw4_ = new IntWrapper(53);
            List<IntegralWrapper> intss2 = new List<IntegralWrapper>() { iw_, iw2_, iw3_, iw4_ };
            foreach (IntegralWrapper item in intss2) item.SetRectangle(new System.Windows.Shapes.Rectangle());
            NumberSetNewIntegral nsn2 = new NumberSetNewIntegral(intss2, true);
            nsn2.SwitchNew(1, 2);
            nsn2.SwitchNew(1, 2);

            sw.Start();
            for (int i = 0; i < 10000000; i++)
            {
                nsn2.SwitchNew(1, 2);
            }
            sw.Stop();
            Variables.WriteLine($"Time switchnew, {nsn2.HasRectangles}: {sw.ElapsedMilliseconds}");
            sw.Reset();

            sw.Start();
            for (int i = 0; i < 10000000; i++)
            {
                nsn2.Switch(1, 2);
            }
            sw.Stop();
            Variables.WriteLine($"Time switch, {nsn2.HasRectangles}: {sw.ElapsedMilliseconds}");
            sw.Reset();
            

            IntWrapper iw = new IntWrapper(3);
            IntWrapper iw2 = new IntWrapper(-3);
            IntWrapper iw3 = new IntWrapper(46);
            IntWrapper iw4 = new IntWrapper(53);
            List<IntegralWrapper> intss = new List<IntegralWrapper>() { iw, iw2, iw3, iw4 };
            NumberSetNewIntegral nsn = new NumberSetNewIntegral(intss, false);

            sw.Start();
            for (int i = 0; i < 10000000; i++)
            {
                nsn.SwitchNew(1, 2);
            }
            sw.Stop();
            Variables.WriteLine($"Time switchnew, {nsn2.HasRectangles}: {sw.ElapsedMilliseconds}");
            sw.Reset();
            sw.Start();
            for (int i = 0; i < 10000000; i++)
            {
                nsn.Switch(1, 2);
            }
            sw.Stop();
            Variables.WriteLine($"Time switch, {nsn2.HasRectangles}: {sw.ElapsedMilliseconds}");
            sw.Reset();

        }


        private static void TestCastingIntDecimal()
        {
            long x = 1345;
            int count = 0;
            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 0; i < 1000000; i++)
            {
                long h = i;
                if (x < h) count++;
            }
            sw.Stop();
            Variables.WriteLine($"Time: {sw.ElapsedMilliseconds}");

            decimal y = 1345.0M;
            sw.Reset();
            sw.Start();
            for (int i = 0; i < 1000000; i++)
            {
                decimal h = i;
                if (y < h) count++;
            }
            sw.Stop();
            Variables.WriteLine($"Time: {sw.ElapsedMilliseconds}");
        }

        private static void TestNumberWrapper()
        {
            List<NumberWrapper> getallen = GetListOfNumberWrapper(0, 1000);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int smaller = 0;
            for (int k = 0; k < 40; k++)
            {
                for (int i = 0; i < getallen.Count; i++)
                {
                    for (int j = 0; j < getallen.Count; j++)
                    {
                        //if (getallen[i].SmallerThan(getallen[j])) smaller++;
                        if (getallen[i] < getallen[j]) smaller++;
                        //Variables.WriteLine($"Type: {getallen[i].Value.GetType()} in {getallen[i].GetType()}");
                        //Console.ReadLine();
                        //return;
                    }
                }
            }
            sw.Stop();
            Variables.WriteLine($"Test number wrapper: {sw.ElapsedMilliseconds} ms");
        }
        
        private static void TestIntWrapper_()
        {
            List<IntWrapper_> getallen = new List<IntWrapper_>();
            for (int i = 0; i < 1000; i++) { getallen.Add(new IntWrapper_(i)); }
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int smaller = 0;
            for (int k = 0; k < 40; k++)
            {
                for (int i = 0; i < getallen.Count; i++)
                {
                    for (int j = 0; j < getallen.Count; j++)
                    {
                        if (getallen[i].SmallerThan(getallen[j])) smaller++;
                        //if (getallen[i] < getallen[j]) smaller++;
                        //Variables.WriteLine($"Type: {getallen[i].Value.GetType()} in {getallen[i].GetType()}");
                        //Console.ReadLine();
                        //return;
                    }
                }
            }
            sw.Stop();
            Variables.WriteLine($"Test int wrapper: {sw.ElapsedMilliseconds} ms");
        }

        private static void TestDPV2()
        {
            List<DPV2<int>> getallen = new List<DPV2<int>>();
            for (int i = 0; i < 1000; i++) { getallen.Add(new DPV2<int>(i)); }
            Stopwatch sw = new Stopwatch();
            sw.Start();
            int smaller = 0;
            for (int k = 0; k < 40; k++)
            {
                for (int i = 0; i < getallen.Count; i++)
                {
                    for (int j = 0; j < getallen.Count; j++)
                    {
                        //if (getallen[i].SmallerThan(getallen[j])) smaller++;
                        if (getallen[i] < getallen[j]) smaller++;
                        //Variables.WriteLine($"Type: {getallen[i].Value.GetType()} in {getallen[i].GetType()}");
                        //Console.ReadLine();
                        //return;
                    }
                }
            }
            sw.Stop();
            Variables.WriteLine($"Test int wrapper: {sw.ElapsedMilliseconds} ms");
        }

        private static List<NumberWrapper> GetListOfNumberWrapper(int min, int count)
        {
            List<NumberWrapper> ans = new List<NumberWrapper>();
            for (int i = min; i < min+count; i++)
            {
                IntWrapper_ iw = new IntWrapper_(i);
                ans.Add(iw);
            }
            return ans;
        }
    }

    // < operator: 1430 ms (40 * 1.000.000 comparisons)
    // Smaller than: 910 ms (40 * 1.000.000 comparisons)
    // Intwrapper smaller than: 810 ms (40 * 1.000.000 comparisons)
    // DPV2 smaller than: 1900 ms (40 * 1.000.000 comparisons)

    public class NumberWrapper 
    {
        public object Value;
        public NumberWrapper(object val) { Value = val; }

        public static bool operator <(NumberWrapper lhs, NumberWrapper rhs)
        {
            //Variables.WriteLine("Compare numberWrapper");
            return lhs.SmallerThan(rhs);
            //return (int)lhs.Value < (int)rhs.Value;
        }
        public static bool operator >(NumberWrapper lhs, NumberWrapper rhs)
        {
            return (int)lhs.Value > (int)rhs.Value;
        }

        public virtual bool SmallerThan(NumberWrapper rhs)
        {
            //Variables.WriteLine("Number smaller than number");
            return (int)Value < (int)rhs.Value;
        }
    }

    public class IntWrapper_ : NumberWrapper, IComparable<IntWrapper_>
    {
        public new int Value;
        public IntWrapper_(int val) : base(val)
        {
            Value = val;
        }

        public int CompareTo(IntWrapper_ other) { return 1; }

        public static bool operator <(IntWrapper_ lhs, NumberWrapper rhs)
        {
            //Variables.WriteLine("Compare int wrapper");
            return lhs.SmallerThan(rhs);
            //return lhs.Value < rhs.Value;
        }
        public static bool operator >(IntWrapper_ lhs, NumberWrapper rhs)
        {
            return lhs.Value > (int)rhs.Value;
        }

        public override bool SmallerThan(NumberWrapper rhs)
        {
            //Variables.WriteLine("Int smaller than number");
            return Value < (int)rhs.Value;
        }
        public bool SmallerThan(IntWrapper_ rhs)
        {
            //Variables.WriteLine("Int smaller than int");
            return Value < rhs.Value;
        }
    }


    public class DPV2<T> where T : IComparable<T>
    {
        public T Value { get; private set; }

        public DPV2(T val)
        {
            Value = val;
        }

        public int CompareTo(DataPointV2<T> other)
        {
            return 1;
        }


        // sbyte, byte, short, ushort, int, uint, long, uint, ulong, float, double, decimal
        public static bool operator <(DPV2<int> lhs, DPV2<T> rhs)
        {
            if (rhs.Value.GetType() == typeof(int)) return lhs.Value < (int)(object)rhs.Value;
            return false;//return lhs.Value < rhs.Value;
        }
        public static bool operator >(DPV2<int> lhs, DPV2<T> rhs)
        {
            if (rhs.Value.GetType() == typeof(int)) return lhs.Value > (int)(object)rhs.Value;
            return false;//return lhs.Value > rhs.Value;
        }

        public static bool operator ==(DPV2<int> lhs, DPV2<T> rhs)
        {
            return lhs.Value == (int)(object)rhs.Value;
        }
        public static bool operator !=(DPV2<int> lhs, DPV2<T> rhs)
        {
            return lhs.Value != (int)(object)rhs.Value;
        }

        // NEED BETTER IMPLEMENTATION
        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
        public override bool Equals(object obj)
        {
            return false;
        }
    }
}
