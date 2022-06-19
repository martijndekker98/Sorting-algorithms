using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PathFinder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public delegate void DelegatePrint(string msg);
        private static DelegatePrint WriteLine;
        //Variables vars;

        private int ObjectTypeSelected;
        private Random rand;

        public MainWindow()
        {
            WriteLine = new DelegatePrint(Variables.WriteLine);
            WriteLine("Test");

            InitializeComponent();

            CultureInfo.CurrentCulture = new CultureInfo("en-US", true);

            UpdateWindow.InitialUpdate(this);

            ObjectTypeSelected = Variables.ObjTypeInitialSelected;
            WriteLine($"2.5: {float.Parse("2.5")}");
            rand = new Random();

            WriteLine($"Value: {Variables.NumeralType.Int}");
            WriteLine($"Value: {Variables.NumeralType.Ulong}");
            // TEST
            //GenerateRandomSet.TestFunction();
            //GenerateRandomSet.TestShuffling();
        }

        
        // Check if the input is an Integer
        private void EnsureIntPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            WriteLine($"Text>{((TextBox)sender).Text}<");
            WriteLine(Variables.regexNumbOfPoints.IsMatch(e.Text).ToString());
            e.Handled = !Variables.regexNumbOfPoints.IsMatch(((TextBox)sender).Text);
        }

        // Check if the input is a number (float or integer depends on objType comboBox)
        private void EnsureNumbPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            //return;
            WriteLine("Ensure number");
            e.Handled = CheckInput.EnsureNumbPreviewTextInput(sender, e, objectType);
        }

        // Ensure that the TextBox is not empty
        private void EnsureTextBoxNotEmpty(object sender, TextChangedEventArgs e)
        {
            //return
            CheckInput.EnsureTextBoxNotEmpty(sender, minVal, maxVal, numbOfPoints, objectType);
        }

        // Update text for slider for delay in MS
        private void DelayMS_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            delayMSval.Text = Math.Round(delayMS.Value).ToString();
        }

        // Checks whether unchecking 'Allow duplicates' is possible based on min-max values 
        private void AllowDupUnchecked(object sender, RoutedEventArgs e)
        {
            allowDup.IsChecked = CheckInput.AllowDupUnchecked(objectType, maxVal, minVal, numbOfPoints);
        }


        // Methods needs rewriting! Replace 'ObjectTypeSelected' -> Variables.Numeraltype
        private void ObjectTypeSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender != null && sender.GetType() == typeof(ComboBox)) WriteLine($"New selected {((ComboBox)sender).SelectedItem}");
            if (!HelperFunctions.IntegerValRequired((ComboBoxItem)objectType.Items[ObjectTypeSelected]) &&
                HelperFunctions.IntegerValRequired((ComboBoxItem)objectType.SelectedItem))
            {
                WriteLine("T R U E !");
                // Check min, max & numb of points
                if (allowDup.IsChecked == true)
                {
                    WriteLine("Duplicates are allowed");
                    if (!HelperFunctions.ParsableInt(minVal.Text)) minVal.Text = Math.Round(HelperFunctions.ConvertTextToFloat(minVal.Text)).ToString();
                    if (!HelperFunctions.ParsableInt(maxVal.Text)) maxVal.Text = Math.Round(HelperFunctions.ConvertTextToFloat(maxVal.Text)).ToString();
                }
                else // Duplicates are not allowed
                {
                    int minV = (int)Math.Round(HelperFunctions.ConvertTextToFloat(minVal.Text));
                    int maxV = (int)Math.Round(HelperFunctions.ConvertTextToFloat(maxVal.Text));
                    int points = HelperFunctions.ConvertTextToInt(numbOfPoints.Text);
                    minVal.Text = minV.ToString();
                    maxVal.Text = maxV - minV + 1 < points ? $"{minV + points - 1}" : maxV.ToString();
                }
            }
            ObjectTypeSelected = objectType.SelectedIndex;
        }

        //private object numberSet;
        //private NumberSet3<int> numberSetInt;
        //private NumberSet3<float> numberSetFloat;
        //private NumberSet3<double> numberSetDouble;
        //private NumberSet4 numberSet;

        private Variables.NumeralType CurrentNumeralType;
        private NumberSetNew CurrentNumberSet;

        private SortingAlgorithms.BasisSortAlgorithm CurrAlgorithm;
        private Rectangle indicator;

        private bool SortingStarted = false;


        // Generate a set of random numbers
        private void RandomiseClick(object sender, RoutedEventArgs e)
        {
            // Delete the set previously used
            DeletePreviousStuff();
            WriteLine("RandomiseClick");

            CurrentNumeralType = (Variables.NumeralType)((ComboBoxItem)objectType.SelectedItem).Tag;
            NumberSetNew nsn = GenerateSet.GenerateNumberSet(this, CurrentNumeralType, rand);
            if (nsn == null) WriteLine($"ERROR! RandomiseClick: nsn is null");
            else
            {
                CurrentNumberSet = nsn;
                nsn.ResetCounters();
            }
            WriteLine($"Type of number seet: {nsn.GetType()}");
        }
        // Delete the information from the previous set
        private void DeletePreviousStuff()
        {
            CurrentNumberSet = null;
            indicator = null;
            VisualisationField.Children.Clear();
            SortingStarted = false;
        }


        // Start sorting the set of numbers
        private void SortSet(object sender, RoutedEventArgs e)
        {
            if (SortingStarted || CurrentNumberSet == null) return;
            // Initialise before the sorting can start
            if (!SortingStarted) InitialiseSorting();

            //if (CurrentNumeralType == Variables.NumeralType.Int) CurrAlgorithm.Step((NumberSetNewIntegral)CurrentNumberSet);
            //if (indicator != null) indicator.Visibility = Visibility.Visible;
            WriteLine("Make step");
            //if (CurrentNumberSet.GetType() == typeof(NumberSetNewInt))
            //{
            //    CurrAlgorithm.Step((NumberSetNewInt)CurrentNumberSet);
            //    CurrAlgorithm.AdjustIndicator((NumberSetNewInt)CurrentNumberSet, indicator);
            //}
            SortNumberSet();
        }
        // Initialise the sorting
        private void InitialiseSorting()
        {
            SortingStarted = true;
            Type sel = ((ComboBoxItem)this.algorithmToUse.SelectedItem).Tag as Type;
            CurrAlgorithm = (SortingAlgorithms.BasisSortAlgorithm)(Activator.CreateInstance(sel));
            WriteLine($"Current algorithm = {CurrAlgorithm.GetName()}");
            //CurrAlgorithm = new SortingAlgorithms.SelectionSortAlgorithm();
            //if (CurrentNumberSet.GetType() == typeof(NumberSetNewInt)) CurrAlgorithm.Prepare((NumberSetNewInt)CurrentNumberSet);
            CurrAlgorithm.Prepare(CurrentNumberSet);
            indicator = UpdateWindow.AddSortingIndicator(this, CurrentNumberSet, Variables.indicatorFill);
        }

        private async Task SortNumberSet()
        {
            WriteLine("Sort the number set");
            while (!CurrentNumberSet.IsSorted)
            {
                CurrAlgorithm.Step(CurrentNumberSet);
                CurrAlgorithm.AdjustIndicator(CurrentNumberSet, indicator);

                //WriteLine("Finished step");
                int ms = (int)delayMS.Value;
                await Task.Delay(ms);
            }
            SortingStarted = false;
        }
        


        private void ForTesting_Click(object sender, RoutedEventArgs e)
        {
            WriteLine("For testing click");
            WriteLine($"Testje: {((IntegralWrapper)((NumberSetNewInt)CurrentNumberSet)[0]).Value}");
            //return;

            WriteLine(((NumberSetNewInt)CurrentNumberSet).ListToString());
            /*
            WriteLine($"{numberSet.type}");
            numberSet.Switch(0, 1);

            //NewDataPoint<int> a = new NewDataPoint<int>(2);
            //NewDataPoint<int> b = new NewDataPoint<int>(3);
            //WriteLine($"a < b? {a < b}");
            DataPointV2<int> a = new DataPointV2<int>(2);
            DataPointV2<int> b = new DataPointV2<int>(3);
            //DataPointV3 a = new DataPointV3(2);
            //DataPointV3 b = new DataPointV3(3);
            WriteLine($"a< b? {a < b}");
            // sbyte, byte, short, ushort, int, uint, long, uint, ulong, float, double, decimal
            */
            if (!SortingStarted) InitialiseSorting();

            WriteLine($"Algo name: {CurrAlgorithm.GetName()} and {CurrentNumberSet.Count}");
            DataWrapper d0 = CurrentNumberSet.GetItem(0);
            DataWrapper d1 = CurrentNumberSet.GetItem(1);
            WriteLine($"Type: {d0.GetType()} <> {d1.GetType()}");
            SortingAlgorithms.QuickSortLomuto qs = new SortingAlgorithms.QuickSortLomuto();
            qs.Prepare(CurrentNumberSet);
            // qs.Partition(CurrentNumberSet, 0, CurrentNumberSet.Count - 1);
            qs.Sort(CurrentNumberSet);

            WriteLine("~~~~~");
            string nspace = "PathFinder.SortingAlgorithms";
            var q = from t in Assembly.GetExecutingAssembly().GetTypes() where t.IsClass && !t.IsAbstract 
                    && t.IsSubclassOf(typeof(SortingAlgorithms.BasisSortAlgorithm)) select t;
            q.ToList().ForEach(t => Console.WriteLine($"N: {t.Name} <> {t.Namespace == nspace}"));
            var ql = q.ToList();
            WriteLine($"type {ql.GetType()} {ql[0].GetType()}");
            ql.ForEach(t => WriteLine($"Test {((SortingAlgorithms.BasisSortAlgorithm)(Activator.CreateInstance(t))).GetName()}"));
        }

        private void TestWrappers(object sender, RoutedEventArgs e)
        {
            //TestClassSpeed.TestSpeed();
            //TestRandomlyPicking();
            //TestGeneratingInts();
            //TestGeneratingLongs();
            //int max = int.MaxValue;
            //int min = int.MinValue;
            //uint range = (uint)max + (uint)(min * -1);
            //WriteLine($"ranhe: {range}");
            //int o1 = (int)(range * 0.0) + min;
            //int o2 = (int)((range * 1.0) + min);
            //WriteLine($"o1: {o1}");
            //WriteLine($"o2: {o2}");
            //double prob = (double)1000000000000000000 / ulong.MaxValue;
            //WriteLine($"prob: {prob}");
            //List<int> ans = GenerateRandomSet.GenWithoutDuplicates(4, 7, 4, Variables.GenOption.Sorted, rand);
            //List<int> ans2 = GenerateRandomSet.GenWithoutDuplicates(2, 4, 3, Variables.GenOption.Reversed, rand);
            //for (int i = 0; i < ans.Count; i++) WriteLine(ans[i].ToString());
            //for (int i = 0; i < ans2.Count; i++) WriteLine(ans2[i].ToString());
            //List<int> ans3 = GenerateRandomSet.GenWithDuplicates(0, 4, 10, Variables.GenOption.Sorted, rand);
            //WriteLine("Duplicates");
            //for (int i = 0; i < ans3.Count; i++) WriteLine(ans3[i].ToString());
            //TestUniqueDoubles();

            //ulong range1 = (ulong)long.MaxValue + (ulong)((long.MinValue+1) * -1) + 1;
            //WriteLine($"r: {range1} <> {ulong.MaxValue}");
            //ulong a = 1000000000071L;
            //double ad = a;
            //WriteLine($"a: {a} <> ad: {ad}");

            ulong m = ulong.MaxValue;
            byte[] ma = BitConverter.GetBytes(m);
            string ma_ = "";
            foreach (byte b in ma) ma_ += $"{b}, ";
            WriteLine(ma_);
            //TestRandomByte();

            //NextTest();
            //GenerateRandomSet.TestCreating(rand);
            Stopwatch sw = new Stopwatch();
            WriteLine($"Type: {CurrentNumberSet.GetType()}");
            WriteLine($"Accessed: {CurrentNumberSet.TimesAccessed}, switched: {CurrentNumberSet.TimesSwitched}");
            DataWrapper dww = ((NumberSetNewInt)CurrentNumberSet)[0];
            WriteLine($"Accessed: {CurrentNumberSet.TimesAccessed}, switched: {CurrentNumberSet.TimesSwitched}");
            ((NumberSetNewInt)CurrentNumberSet).Switch(0, 1);
            WriteLine($"Accessed: {CurrentNumberSet.TimesAccessed}, switched: {CurrentNumberSet.TimesSwitched}");
        }





        //sw.Start();
        //TestRandomUshort();
        //sw.Stop();
        //WriteLine($"TIME: {sw.ElapsedMilliseconds}");
        //sw.Reset();
        //Min: 1925 with 9578
        //Max: 49665 with 10437
        //TIME: 41674

        //sw.Start();
        //TestRandomUshort2();
        //sw.Stop();
        //WriteLine($"TIME 2: {sw.ElapsedMilliseconds}");
        // Min: 55108 with 9549
        // Max: 4978 with 10395
        //TIME 2: 21638
        private void TestRandomUshort()
        {

            int[] generated = new int[65536];
            for (int i = 0; i < 65536; i++) generated[i] = 0;
            Random ra;
            for (int j = 0; j < 10; j++)
            {
                ra = new Random();
                for (int i = 0; i < 65536000; i++)
                {
                    ushort r = ra.NextUshort(ushort.MinValue, ushort.MaxValue);
                    generated[r]++;
                }
            }
            int minV = 0;
            int maxV = 0;
            for (int i = 1; i < 65536; i++)
            {
                if (generated[i] > generated[maxV]) maxV = i;
                else if (generated[i] < generated[minV]) minV = i;
            }
            WriteLine($"Min: {minV} with {generated[minV]}");
            WriteLine($"Max: {maxV} with {generated[maxV]}");
        }
        private void TestRandomUshort2()
        {

            int[] generated = new int[65536];
            for (int i = 0; i < 65536; i++) generated[i] = 0;
            Random ra;
            for (int j = 0; j < 10; j++)
            {
                ra = new Random();
                for (int i = 0; i < 65536000; i++)
                {
                    int r = ra.Next(0, 65536);
                    generated[r]++;
                }
            }
            int minV = 0;
            int maxV = 0;
            for (int i = 1; i < 65536; i++)
            {
                if (generated[i] > generated[maxV]) maxV = i;
                else if (generated[i] < generated[minV]) minV = i;
            }
            WriteLine($"Min: {minV} with {generated[minV]}");
            WriteLine($"Max: {maxV} with {generated[maxV]}");
        }


        private const long ONE_TENTH = 922337203685477581;

        public static void NextTest()
        {
            Random rnd = new Random();

            int[] count = new int[10];

            // Generate 20 million long integers.
            for (int ctr = 1; ctr <= 20000000; ctr++)
            {
                long number = NextLong(rnd, true);
                // Categorize random numbers.
                count[(int)(number / ONE_TENTH)]++;
            }

            // Display breakdown by range.
            Console.WriteLine("{0,28} {1,32}   {2,7}\n", "Range", "Count", "Pct.");
            for (int ctr = 0; ctr <= 9; ctr++)
            {
                Console.WriteLine("{0,25:N0}-{1,25:N0}  {2,8:N0}   {3,7:P2}",
                        ctr * ONE_TENTH,
                                        ctr < 9 ? ctr * ONE_TENTH + ONE_TENTH - 1 : Int64.MaxValue,
                                            count[ctr],
                        count[ctr] / 20000000.0);
            }
        }

        public static long NextLong(Random random, bool includeLongMaxValue = false)
        {
            byte[] longBuf = new byte[sizeof(long)];
            long longValue;

            // Loop runs once if includeLongMaxValue is true.
            // Loop will run more than once with a chance of 1 / 2^31 otherwise.
            do
            {
                random.NextBytes(longBuf);
                // Make sure that the retrieved value is zero or positive.
                longValue = BitConverter.ToInt64(longBuf, 0) & long.MaxValue;
            }
            while (!includeLongMaxValue && longValue == long.MaxValue);

            return longValue;
        }


        private void TestRandomByte()
        {
            int[] opties = new int[256];
            for (int i = 0; i < 256; i++) opties[i] = 0;

            byte range = 10;
            for (int i = 0; i < 25500000; i++)
            {
                byte randB;
                do
                {
                    byte[] buf = new byte[1];
                    rand.NextBytes(buf);
                    randB = buf[0];
                } while (randB > byte.MaxValue - ((byte.MaxValue % range) + 1) % range) ;
                // while (randB > 255 - {((255 % range) + 1) % range} )  // Modulo before minus
                int a = (randB % range) + 0;
                opties[a] += 1;
            }
            int minV = 0;
            int maxV = 0;
            for (int i = 1; i < 255; i++)
            {
                if (opties[i] > opties[maxV]) maxV = i;
                else if (opties[i] < opties[minV]) minV = i;
            }
            WriteLine($"Min: {minV} with {opties[minV]}");
            WriteLine($"Max: {maxV} with {opties[maxV]}");
            for (int i = 0; i < 256; i++)
            {
                WriteLine($"Val: {i} with {opties[i]}");
                if (opties[i] == 0) break;
            }
        }

        private void TestUniqueDoubles()
        {
            ulong een = 1;
            double max = uint.MaxValue;
            for (uint i = 1; i < uint.MaxValue; i++)
            {
                if (i % 100000000 == 0) Variables.WriteLine($"Done: {((double)i)/max}");
                double c = i;
                double c1 = (i - een);
                double c2 = (i + een);
                if (c == c1) { WriteLine($"THE SAME: {c} <> {c1}"); return; }
                if (c == c2) { WriteLine($"THE SAME: {c} <> {c2}"); return; }
            }         
        }

        private void TestGeneratingLongs()
        {
            const long ONE_TENTH = 922337203685477581;
            
            long number;
            int[] count = new int[10];
            int[] countNeg = new int[10];

            // Generate 20 million long integers.
            for (int ctr = 1; ctr <= 20000000; ctr++)
            {
                double r = rand.NextDouble();
                if (r < 0.5)
                {
                    number = (long)(r * 2 * long.MinValue);
                    countNeg[(int)(number*-1 / ONE_TENTH)]++; // Categorize random numbers.
                }
                else
                {
                    number = (long)((r-0.5) * 2 * long.MaxValue);
                    count[(int)(number / ONE_TENTH)]++; // Categorize random numbers.
                }
            }
            // Display breakdown by range.
            Console.WriteLine("{0,28} {1,32}   {2,7}\n", "Range", "Count", "Pct.");
            for (int ctr = 0; ctr <= 9; ctr++)
                Console.WriteLine("{0,25:N0}-{1,25:N0}  {2,8:N0}   {3,7:P2}", ctr * ONE_TENTH,
                                   ctr < 9 ? ctr * ONE_TENTH + ONE_TENTH - 1 : Int64.MaxValue,
                                   count[ctr], count[ctr] / 20000000.0);
            Variables.WriteLine("NOW FOR THE NEGATIVE NUMBERS");
            for (int ctr = 0; ctr <= 9; ctr++)
                Console.WriteLine("{0,25:N0}-{1,25:N0}  {2,8:N0}   {3,7:P2}", ctr * ONE_TENTH,
                                   ctr < 9 ? ctr * ONE_TENTH + ONE_TENTH - 1 : Int64.MaxValue,
                                   countNeg[ctr], countNeg[ctr] / 20000000.0);
        }

        private void TestGeneratingInts()
        {
            double minVal = 0;
            double maxVal = int.MinValue + 10;
            for (int i = 0; i < 100000; i++)
            {
                double h = rand.NextDouble();
                if (h < 0.5)
                {
                    double h2 = h * 2 * int.MinValue;
                    if (h2 < minVal) minVal = h2;
                    else if (h2 > maxVal) maxVal = h;
                }
                else
                {
                    double h2 = (h-0.5) * 2 * int.MinValue;
                    if (h2 < minVal) minVal = h2;
                    else if (h2 > maxVal) maxVal = h;
                }
            }
            WriteLine($"Min: {minVal} -> {(int)minVal} ({int.MinValue})");
            WriteLine($"Max: {maxVal} -> {(int)maxVal} (0)");
        }

        private void TestRandomlyPicking()
        {
            int[] opties = new int[200];
            for (int i = 0; i < opties.Length; i++) opties[i] = 0;
            for (int i = 0; i < 10000000; i++)
            {
                int added = 0;
                for (int j = 0; j < opties.Length; j++)
                {
                    if (added == 100) break;
                    else if (100 - added == opties.Length - j)
                    {
                        for (int k = j; k < opties.Length; k++) opties[k] += 1;
                        break;
                    }
                    double toAdd = rand.NextDouble();
                    if (toAdd < 0.5)
                    {
                        added += 1;
                        opties[j] += 1;
                    }
                }
            }

            int minIndex = 0;
            int maxIndex = 0;
            for (int i = 1; i < opties.Length; i++)
            {
                if (opties[i] < opties[minIndex]) minIndex = i;
                else if (opties[i] > opties[maxIndex]) maxIndex = i;
            }
            WriteLine($"Min: {minIndex} with {opties[minIndex]} times");
            WriteLine($"Max: {maxIndex} with {opties[maxIndex]} times");
        }
    }
    
}
