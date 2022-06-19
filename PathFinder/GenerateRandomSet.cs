using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PathFinder
{
    class GenerateRandomSet
    {
        //public NumberSet GenerateRandomSet(Variables.GenOption genOption, int min, int max, int numberOf, bool allowDuplicates)
        //{
        //    return null;
        //}
        // OLD FUNCTION
        public static NumberSetNew GenRandomList(int min, int max, int numberOf, bool allowDuplicates, Variables.NumeralType NumbType, 
            Random rand, bool usesRect, Variables.GenOption go)
        {
            Variables.WriteLine($"Use duplicates? {allowDuplicates}");
            if (NumbType == Variables.NumeralType.Int) return GenRandomListInt(min, max, numberOf, allowDuplicates, rand, usesRect, go);
            Variables.WriteLine("Not an integral numeral type");
            return null;
        }


        // long, int, short, sbyte
        public static NumberSetNew GenRandomListIntegral(long min, long max, int numberOf, bool allowDuplicates, Variables.NumeralType NumbType,
           Random rand, bool usesRect, Variables.GenOption go)
        {
            if (NumbType == Variables.NumeralType.Int) return GenRandomListInt((int)min, (int)max, numberOf, allowDuplicates, rand, usesRect, go);
            return null;
        }
        // ulong, uint, ushort, byte
        public static NumberSetNew GenRandomListIntegralPos(ulong min, ulong max, int numberOf, bool allowDuplicates, Variables.NumeralType NumbType,
           Random rand, bool usesRect, Variables.GenOption go)
        {
            if (NumbType == Variables.NumeralType.Uint) return GenRandomListInt((int)min, (int)max, numberOf, allowDuplicates, rand, usesRect, go);
            return null;
        }
        // float, double, decimal
        public static NumberSetNew GenRandomListFloating(double min, double max, int numberOf, bool allowDuplicates, Variables.NumeralType NumbType,
            Random rand, bool usesRect, Variables.GenOption go)
        {
            if (NumbType == Variables.NumeralType.Float) return GenRandomListInt((int)min, (int)max, numberOf, allowDuplicates, rand, usesRect, go);
            return null;
        }



        //
        // Integrals
        //
        private static NumberSetNewInt GenRandomListInt(int min, int max, int numberOf, bool allowDuplicates, 
            Random rand, bool usesRect, Variables.GenOption go)
        {
            Variables.WriteLine($"GenRandomListInt: {min}, {max}, numb: {numberOf}");
            List<int> numberz = new List<int>();
            if (go == Variables.GenOption.Testing)  numberz = Variables.TestingNumbers;
            else if (allowDuplicates)               numberz = GenWithDuplicates(min, max, numberOf, go, rand);
            else                                    numberz = GenWithoutDuplicates(min, max, numberOf, go, rand);

            Variables.WriteLine($"Length of the random list: {numberz.Count}");
            List<IntWrapper> NumberList = new List<IntWrapper>();
            for (int i = 0; i < numberz.Count; i++)
            {
                NumberList.Add(new IntWrapper(numberz[i]));
            }
            return new NumberSetNewInt(NumberList, usesRect);
        }
        //private static NumberSetNewIntegral GenRandomListLong(long min, long max, int numberOf, bool allowDuplicates,
        //    Random rand, bool usesRect, Variables.GenOption go)
        //{
        //    List<long> numberz = new List<int>();
        //    if (go == Variables.GenOption.Testing) numberz = new List<int>() { 11, 25, 12, 22, 64 };
        //    else numberz = allowDuplicates ? GenWithDuplicates(min, max, numberOf, go, rand) :
        //        GenWithoutDuplicates(min, max, numberOf, go, rand);
        //    Variables.WriteLine($"Length of the random list: {numberz.Count}");

        //    List<IntegralWrapper> NumberList = new List<IntegralWrapper>();
        //    for (int i = 0; i < numberz.Count; i++)
        //    {
        //        NumberList.Add(new IntegralWrapper(numberz[i]));
        //    }
        //    return new NumberSetNewIntegral(NumberList, usesRect);
        //}


        // NEEDS TO BE UPDATED ! NOT USE DOUBLE AS THE UNIQUENESS IS LOWER THAN LONG/ULONG
        // https://stackoverflow.com/questions/6651554/random-number-in-long-range-is-this-the-way/11640700#11640700

        //
        // Generate a list of (numberOf) integers with duplicates allowed
        //
        public static List<int> GenWithDuplicates(int min, int max, int numberOf, Variables.GenOption go, Random rand)
        {
            List<int> ans = new List<int>(numberOf);
            ulong range = (ulong)max + (ulong)(min * -1) + 1;
            for (int i = 0; i < numberOf; i++)
            {
                int r = rand.NextInt(min, max);
                ans.Add(r);
            }
            if (go == Variables.GenOption.Random) FisherYatesShuffle.Shuffle(ans, rand);
            else if (go == Variables.GenOption.Sorted) ans.Sort();
            else if (go == Variables.GenOption.Reversed) ans.Sort(new Comparison<int>((i1, i2) => i2.CompareTo(i1)));
            return ans;
        }



        /// <summary>
        /// PROBLEMS!
        /// METHOD 1: for (int i = min; i leq max;) if (rand leq prob) add number to ANS
        /// METHOD 2: random val, if (val NOT in ANS) add to ANS, else sample new random
        /// If you loop over all options (for loop) while the max - min is very large, it will take forever!
        /// If you randomly choose numbers then you need to be able to determine if already in array...
        /// This would probably be fine normally, BUT: if min = 0, max = 1,000,001, and you pick 999,999 numbers... a lot of doubles, which causes issues...
        /// In that case, the Method 1 would be faster/better
        /// 
        /// ExpTime: R = Gen Random number, N = total numbers, M = range, F = time to look through array
        /// M1: MAX R*M, R*N -- R*M, AVG: R*((M+N)/2)
        /// 
        /// 1, 1+1/10
        /// M2: R*N -- R*INF, AVG: sum 1 + (sum (i-1)/10^j, j=1 to infinity), i=1 to 10 = 15
        /// M2: AVG: N + (N / 2M)
        /// M2: 0 => R + 0
        /// M2: 1 => R + 1F + 1/M * (R + 1/M * (R + ...))
        /// M2: N-1 => R + (N-1)F + (N-1)/M * (R + ...)
        /// 
        /// (M+N)/2 <> N + N*(N / 2M)
        /// N=50, M=50
        /// 50 <> 75
        /// N=50, M=60
        /// 55 <> 70.8
        /// N=50, M=70
        /// 60 <> 67.85
        /// N=50, M=80
        /// 75 <> 65
        /// N=50, M=75
        /// 67.5 <> 66.5
        /// 
        /// </summary>

        //
        // Generate a list of (numberOf) unique integers, duplicates not allowed
        // Versions: sbyte, short, int, long
        //
        public static List<int> GenWithoutDuplicates(int min, int max, int numberOf, Variables.GenOption go, Random rand)
        {
            //Variables.WriteLine($"GenRandomListInt: go: {go}");
            // Split up based on the GenOption 
            if (go == Variables.GenOption.Random)        return GenWithoutDuplicatesRandom(min, max, numberOf, rand);
            else if (go == Variables.GenOption.Sorted)   return GenWithoutDuplicatesSorted(min, max, numberOf, rand);
            else if (go == Variables.GenOption.Reversed) return GenWithoutDuplicatesReversed(min, max, numberOf, rand);
            return new List<int>();
        }
        public static List<int> GenWithoutDuplicatesRandom(int min, int max, int numberOf, Random rand)
        {
            List<int> ans = new List<int>(numberOf);
            ulong range = (ulong)max + (ulong)(min * -1) + 1;
            double ratio = range / (double)numberOf;
            //Variables.WriteLine($"GenWithoutDuplicatesRandom: range {range}, ratio: {ratio}");

            // If the ration is >= 7, the fastest method to create the list of unique (non sorted) numbers differs
            if (ratio >= 7) ans = RandomlyGenerateSet(min, max, numberOf, rand);
            else ans = RandomlyPickSet(min, max, numberOf, rand, range);

            // Shuffle to ensure randomness
            FisherYatesShuffle.Shuffle(ans, rand);
            return ans;
        }
        public static List<int> GenWithoutDuplicatesSorted(int min, int max, int numberOf, Random rand)
        {
            List<int> ans = new List<int>(numberOf);
            ulong range = (ulong)max + (ulong)(min * -1) + 1;
            double ratio = range / (double)numberOf;

            // If the ration is >= 11, the fastest method to create the list of unique (sorted) numbers differs
            if (ratio >= 11)
            {
                ans = RandomlyGenerateSet(min, max, numberOf, rand);
                ans.Sort();
            }
            else ans = RandomlyPickSet(min, max, numberOf, rand, range);
            return ans;
        }
        public static List<int> GenWithoutDuplicatesReversed(int min, int max, int numberOf, Random rand)
        {
            List<int> ans = new List<int>(numberOf);
            ulong range = (ulong)max + (ulong)(min * -1) + 1;
            double ratio = range / (double)numberOf;
            //Variables.WriteLine($"GenWithoutDuplicatesReversed: range {range}, ratio: {ratio}");

            // If the ration is >= 11, the fastest method to create the list of unique (sorted) numbers differs
            if (ratio >= 11)
            {
                ans = RandomlyGenerateSet(min, max, numberOf, rand).ToList();
                ans.Sort(new Comparison<int>((i1, i2) => i2.CompareTo(i1)));
            }
            else
            {   // Perhaps RandomlyPickSet can be updated
                double probToSelect = ((double)numberOf / range);
                for (int i = max; i >= min && ans.Count <= numberOf; i--)
                {
                    if (numberOf - ans.Count == i - min + 1)
                    {
                        for (int k = i; k >= min; k--) ans.Add(k);
                        break;
                    }
                    else if (rand.NextDouble() < probToSelect) ans.Add(i);
                }
            }
            return ans;
        }
        private static List<int> RandomlyGenerateSet(int min, int max, int numberOf, Random rand)
        {
            //Variables.WriteLine($"RandomlyGenerateSet: num: {numberOf}");
            HashSet<int> lijst = new HashSet<int>();
            while (lijst.Count < numberOf)
            {
                int r = rand.NextInt(min, max); 
                lijst.Add(r);
            }
            return lijst.ToList();
        }
        private static List<int> RandomlyPickSet(int min, int max, int numberOf, Random rand, ulong range)
        {
            double probToSelect = ((double)numberOf / range);
            //Variables.WriteLine($"RandomlyPickSet: range {range}, prob {probToSelect}, num: {numberOf},");
            List<int> ans = new List<int>();
            for (int i = min; i <= max && ans.Count <= numberOf; i++)
            {
                double toSel = rand.NextDouble();
                //Variables.WriteLine($"i: {i}, co: {ans.Count}, toSel: {toSel}");
                if (numberOf - ans.Count == max - i + 1)
                {
                    //Variables.WriteLine($"Must select all: i: {i}, num: {numberOf}, co: {ans.Count}, max: {max}");
                    for (int k = i; k <= max; k++) ans.Add(k);
                    break;
                }
                else if (toSel < probToSelect) ans.Add(i);
            }
            return ans;
        }


        




        public static List<int> GenWithoutDuplicatesOLD(int min, int max, int numberOf, Variables.GenOption go, Random rand)
        {
            //if (go == Variables.GenOption.Random) return GenWithoutDuplicatesIntRandom(min, max, numberOf, rand);
            List<int> ans = new List<int>(numberOf);
            ulong range = (ulong)max + (ulong)(min * -1);
            double probToSelect = (ulong)numberOf / range;
            if (go == Variables.GenOption.Reversed)
            {
                for (int i = max; i >= min; i--)
                {
                    if (rand.NextDouble() < probToSelect) ans.Add(i);
                }
            }
            else
            {
                for (int i = min; i <= max; i++)
                {
                    if (rand.NextDouble() < probToSelect) ans.Add(i);
                }
                if (go == Variables.GenOption.Random) FisherYatesShuffle.Shuffle(ans, rand);
            }
            return ans;
        }


        public static void TestCreating(Random rand)
        {
            /* RESULTS min =0, numberof = 100000, 100 rounds
            Max: 110000, 125000, 150000, 175000, 200000, 
            M1: 108,    127,    192,    226,    282,
            M2: 45385,  42612,  42071,  41540,  41034, 
            M3: 3121,   2740,   2554,   2350,   2275, 
            M4: 3079,   2788,   2518,   2424,   2344, 
            M3 uns: 2507, 2024, 1683,   1461,   1385,
            Max: 250000, 300000, 400000, 500000, 
            M1: 390,    498,    676,    884,
            M3: 2206,   2172,   2116,   2060, 
            M4: 2261,   2202,   2148,   2119, 
            M3 uns: 1304, 1255, 1196,   1196,
            Max: 600000, 750000, 1000000, 
            M1: 1062,   1375,   1854,  
            M2: 
            M3: 2086,   1994,   1997, 
            M4: 2088,   2071,   2071, 
            M3 uns: 1151, 1144, 1117,
            Max: 600000, 650000, 700000, 
            M1: 972, 2222, 2309, 
            M2: 
            M3: 2031, 2026, 2004, 
            M4: 
            M3 uns: 1214, 1204, 1211, 
            M1 new: 1119, 1133, 1245,
            
            Max: 1200000, 1500000, 2000000, 
            M1:         2843,   3825, 
            M2: 
            M3: 1998,   1990,   1987, 
            M4: 2070,   2072,   2063, 
            M3 uns: 1141, 1108, 1099,
            Max: 1200000, 1300000, 1400000, 
            M1: 2262,   2459,   2664,
            M2: 
            M3: 1997,   1974,   1992, 
            M4: 2028,   2047,   2039, 
            M3 uns: 1107, 1111, 1102, 
            */
            int min = 0;
            int numberOf = 100000;
            //int[] maxes = new int[] { 110000, 125000, 150000, 175000, 200000 };
            //int[] maxes = new int[] { 1200000, 1300000, 1400000 };
            int[] maxes = new int[] { 110000, 125000, 150000, 175000, 200000, 250000, 300000, 400000, 500000, 600000, 750000, 1000000, 1200000,
                1300000, 1400000, 1500000, 2000000, };
            maxes = new int[] { 1100000 };
            Stopwatch sw = new Stopwatch();
            string[] options = new string[] { "M1: ", "M2: ", "M3: ", "M4: ", "M3 uns: ", "M1 new: ", };
            string maxess = "Max: ";
            foreach (int max in maxes)
            {
                Variables.WriteLine($"Max {max}");
                maxess += max + ", ";
                sw.Start();
                for (int i = 0; i < 100; i++)
                {
                    GenWithoutDuplicatesM1(min, max, numberOf, rand);
                }
                sw.Stop();
                options[0] += sw.ElapsedMilliseconds + ", ";
                sw.Reset();

                // M1 NEW
                sw.Start();
                for (int i = 0; i < 100; i++)
                {
                    GenWithoutDuplicatesM1New(min, max, numberOf, rand);
                }
                sw.Stop();
                options[5] += sw.ElapsedMilliseconds + ", ";
                sw.Reset();

                //sw.Start();
                //for (int i = 0; i < 100; i++)
                //{
                //    GenWithoutDuplicatesM2(min, max, numberOf, rand);
                //}
                //sw.Stop();
                //options[1] += sw.ElapsedMilliseconds + ", ";
                //sw.Reset();

                sw.Start();
                for (int i = 0; i < 100; i++)
                {
                    GenWithoutDuplicatesM3(min, max, numberOf, rand);
                }
                sw.Stop();
                options[2] += sw.ElapsedMilliseconds + ", ";
                sw.Reset();

                //sw.Start();
                //for (int i = 0; i < 100; i++)
                //{
                //    GenWithoutDuplicatesM4(min, max, numberOf, rand);
                //}
                //sw.Stop();
                //options[3] += sw.ElapsedMilliseconds + ", ";
                //sw.Reset();

                sw.Start();
                for (int i = 0; i < 100; i++)
                {
                    GenWithoutDuplicatesM3Unsorted(min, max, numberOf, rand);
                }
                sw.Stop();
                options[4] += sw.ElapsedMilliseconds + ", ";
                //sw.Reset();
            }
            Variables.WriteLine(maxess);
            foreach (string s in options) Variables.WriteLine(s);
        }
        /* 
                */
        public static List<int> GenWithoutDuplicatesM1(int min, int max, int numberOf, Random rand)
        {
            List<int> ans = new List<int>(numberOf);
            ulong range = (ulong)max + (ulong)(min * -1);
            double probToSelect = (ulong)numberOf / range;
            for (int i = min; i <= max; i++)
            {
                if (rand.NextDouble() < probToSelect) ans.Add(i);
            }
            return ans;
        }
        public static List<int> GenWithoutDuplicatesM1New(int min, int max, int numberOf, Random rand)
        {
            List<int> ans = new List<int>(numberOf);
            ulong range = (ulong)max + (ulong)(min * -1);
            double probToSelect = (ulong)numberOf / range;
            for (int i = min; i <= max && ans.Count <= numberOf; i++)
            {
                if (numberOf - ans.Count == max - i + 1)
                {
                    for (int k = i; k <= max; k++) ans.Add(k);
                    break;
                }
                else if (rand.NextDouble() < probToSelect) ans.Add(i);
            }
            return ans;
        }
        public static List<int> GenWithoutDuplicatesM1NewNew(int min, int max, int numberOf, Random rand)
        {
            List<int> ans = new List<int>(numberOf);
            ulong range = (ulong)max + (ulong)(min * -1);
            double probToSelect = (ulong)numberOf / range;
            for (int i = min; i <= max && ans.Count <= numberOf; i++)
            {
                if (numberOf - ans.Count == max - i + 1)
                {
                    for (int k = i; k <= max; k++) ans.Add(k);
                    break;
                }
                else if (rand.NextDouble() < probToSelect) ans.Add(i);
            }
            return ans;
        }
        public static List<int> GenWithoutDuplicatesM2(int min, int max, int numberOf, Random rand)
        {
            List<int> ans = new List<int>(numberOf);
            while (ans.Count < numberOf)
            {
                int r = rand.Next(min, max + 1);
                int index = BinarySearch.FindIndex(ans, r);
                if (index < 0) ans.Insert(index * -1 - 1, r);
            }
            return ans;
        }
        public static List<int> GenWithoutDuplicatesM3Unsorted(int min, int max, int numberOf, Random rand)
        {
            List<int> ans = new List<int>(numberOf);
            HashSet<int> lijst = new HashSet<int>();
            while (lijst.Count < numberOf)
            {
                int r = rand.Next(min, max + 1);
                lijst.Add(r);
            }
            ans = lijst.ToList();
            return ans;
        }
        public static List<int> GenWithoutDuplicatesM3(int min, int max, int numberOf, Random rand)
        {
            List<int> ans = new List<int>(numberOf);
            HashSet<int> lijst = new HashSet<int>();
            while (lijst.Count < numberOf)
            {
                int r = rand.Next(min, max + 1);
                lijst.Add(r);
            }
            ans = lijst.ToList();
            ans.Sort();
            return ans;
        }
        public static List<int> GenWithoutDuplicatesM4(int min, int max, int numberOf, Random rand)
        {
            List<int> ans = new List<int>(numberOf);
            HashSet<int> lijst = new HashSet<int>();
            while (lijst.Count < numberOf)
            {
                int r = rand.Next(min, max + 1);
                lijst.Add(r);
            }
            foreach (int a in lijst) ans.Add(a);
            ans.Sort();
            return ans;
        }




        // NOT SUSTAINABLE TO USE RANGE! if you need 100 number between 0 and 18.000.000.000.000 would take too much space
        //private static List<int> GenWithoutDuplicatesIntRandom(int min, int max, int numberOf, Random rand)
        //{
        //    List<int> temp = Enumerable.Range(min, max + 1).ToList();
        //    FisherYatesShuffle.Shuffle(temp, rand);
        //    List<int> ans = temp.GetRange(0, numberOf);
        //    return ans;
        //}














        // 
        // OLD FUNCTION
        //
        // Generate a list of integers
        //public static List<int> GenRandomList(int min, int max, int numberOf, bool allowDuplicates)
        //{
        //    if (allowDuplicates) return GenWithDuplicatesInt(min, max, numberOf);
        //    return GenWithoutDuplicatesInt(min, max, numberOf);
        //}



        //public struct TestDing<T>
        //{
        //    public T value;
        //    public TestDing(T inp)
        //    {
        //        value = inp;
        //    }
        //}

        //public static IList<T> TestGenerateList<T>(T a)
        //{
        //    List<int> ans = new List<int>() { 1, 2, 3};
        //    return (IList<T>)Convert.ChangeType(ans, typeof(IList<T>));
        //}

        public static List<Variables.DataPoint<int>> GetListOfDataPoints(List<int> listOfVals)
        {
            List<Variables.DataPoint<int>> ans = new List<Variables.DataPoint<int>>();
            for (int i = 0; i < listOfVals.Count; i++)
            {
                ans.Add(new Variables.DataPoint<int>(listOfVals[i]));
            }
            return ans;
        }
        public static List<Variables.DataPoint<float>> GetListOfDataPoints(List<float> listOfVals)
        {
            List<Variables.DataPoint<float>> ans = new List<Variables.DataPoint<float>>();
            for (int i = 0; i < listOfVals.Count; i++)
            {
                ans.Add(new Variables.DataPoint<float>(listOfVals[i]));
            }
            return ans;
        }
        public static List<Variables.DataPoint<double>> GetListOfDataPoints(List<double> listOfVals)
        {
            List<Variables.DataPoint<double>> ans = new List<Variables.DataPoint<double>>();
            for (int i = 0; i < listOfVals.Count; i++)
            {
                ans.Add(new Variables.DataPoint<double>(listOfVals[i]));
            }
            return ans;
        }


        public static List<NewDataPoint<int>> GetNewDataPoints(List<int> listOfVals)
        {
            List<NewDataPoint<int>> ans = new List<NewDataPoint<int>>();
            for (int i = 0; i < listOfVals.Count; i++)
            {
                ans.Add(new NewDataPoint<int>(listOfVals[i]));
            }
            return ans;
        }

        public static void TestFunction()
        {
            int min = 0;
            int max = 110000;
            int numbOf = 100000;
            Random rand = new Random();
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            List<int> temp = Enumerable.Range(min, max+1).ToList();

            stopwatch.Stop();
            Variables.WriteLine($"Time passed in ms: {stopwatch.ElapsedMilliseconds} for creating the list, with count {temp.Count}");
            stopwatch.Reset();
            stopwatch.Start();

            List<int> ans1 = Enumerable.Range(min, max+1).ToList();
            for (int i = 0; i < ans1.Count-numbOf; i++)
            {
                ans1.RemoveAt(rand.Next(0, ans1.Count));
            }

            stopwatch.Stop();
            Variables.WriteLine($"Time passed in ms: {stopwatch.ElapsedMilliseconds} for creating the list");

            Variables.WriteLine("The next method will be especially slow when max-min and numbOf are close");
            stopwatch.Reset();
            stopwatch.Start();

            HashSet<int> uniques = new HashSet<int>();
            List<int> ans2 = new List<int>();
            while (ans2.Count < numbOf)
            {
                int option;
                do { option = rand.Next(min, max + 1); }
                while (uniques.Contains(option));
                uniques.Add(option);
                ans2.Add(option);
            }

            stopwatch.Stop();
            Variables.WriteLine($"Time passed in ms: {stopwatch.ElapsedMilliseconds} for creating the list");
            stopwatch.Reset();
            stopwatch.Start();

            List<int> options = Enumerable.Range(min, max + 1).ToList();
            // Shuffle the options
            for (int i = 0; i < options.Count; i++)
            {
                int randIndex = rand.Next(0, options.Count);
                int temp1 = options[randIndex];
                options[randIndex] = options[i];
                options[i] = temp1;
            }
            List<int> ans = options.Take(numbOf).ToList();

            stopwatch.Stop();
            Variables.WriteLine($"Time passed in ms: {stopwatch.ElapsedMilliseconds} for creating the list");
        }

        public static void TestShuffling()
        {
            Random r = new Random();
            Dictionary<int, int> times = new Dictionary<int, int>();
            for (int i = 0; i < 2000; i++)
            {
                times[i] = 0;
            }
            for (int i = 0; i < 20000; i++)
            {
                if (i % 100 == 0) Variables.WriteLine($"Round: {i}");
                List<int> temp = Enumerable.Range(0, 1999 + 1).ToList();
                FisherYatesShuffle.Shuffle(temp, r);
                for (int j = 0; j < 1000; j++)
                {
                    times[temp[j]]++;
                }
            }
            Tuple<int, int> min = new Tuple<int, int>(0, times[0]);
            Tuple<int, int> max = new Tuple<int, int>(0, times[0]);
            long timesFirstHalf = 0;
            long timesSecondHalf = 0;
            for (int i = 0; i < 2000; i++)
            {
                Variables.WriteLine($"Int {i} occured {times[i]} times");
                if (times[i] > max.Item2) max = new Tuple<int, int>(i, times[i]);
                if (times[i] < min.Item2) min = new Tuple<int, int>(i, times[i]);
                if (i < 1000) timesFirstHalf += times[i];
                else timesSecondHalf += times[i];
            }
            Variables.WriteLine($"The min is {min.Item1} with {min.Item2} times");
            Variables.WriteLine($"The max is {max.Item1} with {max.Item2} times");
            Variables.WriteLine($"The first half is {timesFirstHalf} and the second is {timesSecondHalf}");
            // Min 4834, Max 5150 for 10000 rounds
            // Min 9744, 10237 for 20000 rounds
        }
    }
}
