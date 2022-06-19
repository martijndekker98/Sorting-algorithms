using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace PathFinder
{
    class NumberSet<T>
    {
        //private List<int> _intList;
        //private List<float> _floatList;
        //private List<double> _doubleList;
        public List<T> numberList;

        public bool IsSorted { get; private set; }
        
        public NumberSet(List<T> lijst, bool sorted = false)
        {
            numberList = lijst;
            IsSorted = sorted;
        }

        public void HasBeenSorted()
        {
            IsSorted = true;
        }
        //public NumberSet(List<int> list, bool isSorted)
        //{
        //    _intList = list;
        //    IsSorted = isSorted;
        //}
        //public NumberSet(List<float> list, bool isSorted)
        //{
        //    _floatList = list;
        //    IsSorted = isSorted;
        //}
        //public NumberSet(List<double> list, bool isSorted)
        //{
        //    _doubleList = list;
        //    IsSorted = isSorted;
        //}
    }

    class NumberSet2<T>
    {
        public List<Variables.DataPoint<T>> numberList;

        public bool IsSorted { get; private set; }

        public NumberSet2(List<Variables.DataPoint<T>> lijst, bool sorted = false)
        {
            numberList = lijst;
            IsSorted = sorted;
        }

        public void HasBeenSorted()
        {
            IsSorted = true;
        }
    }


    class NumberSet3<T>
    {
        public List<NewDataPoint<T>> numberList { get; private set; }

        public bool IsSorted { get; private set; }

        public NumberSet3(List<NewDataPoint<T>> lijst, bool sorted = false)
        {
            numberList = lijst;
            IsSorted = sorted;
        }

        public void HasBeenSorted()
        {
            IsSorted = true;
        }
    }


    enum NDPType
    {
        Int,
        Float,
        Double
    }
    class NumberSet4
    {
        private List<NewDataPoint<int>> numberListInt;
        private List<NewDataPoint<float>> numberListFloat;
        private List<NewDataPoint<double>> numberListDouble;
        public NDPType type { get; private set; }

        public bool IsSorted { get; private set; }

        public NumberSet4(List<NewDataPoint<int>> lijst, bool sorted = false)
        {
            numberListInt   = lijst;
            IsSorted        = sorted;
            type = NDPType.Int;
        }
        public NumberSet4(List<NewDataPoint<float>> lijst, bool sorted = false)
        {
            numberListFloat = lijst;
            IsSorted = sorted;
            type = NDPType.Float;
        }
        public NumberSet4(List<NewDataPoint<double>> lijst, bool sorted = false)
        {
            numberListDouble = lijst;
            IsSorted = sorted;
            type = NDPType.Double;
        }

        public void HasBeenSorted()
        {
            IsSorted = true;
        }

        // Switch the indices a and b
        public void Switch(int a, int b)
        {
            if (type == NDPType.Int) SwitchInt(a, b);
            else if (type == NDPType.Float) SwitchInt(a, b);
            else if (type == NDPType.Double) SwitchInt(a, b);
        }
        private void SwitchInt(int a, int b)
        {
            if (a < 0 || b < 0 || a >= numberListInt.Count || b >= numberListInt.Count)
            {
                Variables.WriteLine($"ERROR, switching not possible with indices {a}, {b}. Count: {numberListInt.Count}");
            }
            NewDataPoint<int> temp = numberListInt[a];
            double leftb = Canvas.GetLeft(numberListInt[b].CorrespondingRectangle);
            numberListInt[a] = numberListInt[b];
            Canvas.SetLeft(numberListInt[a].CorrespondingRectangle, Canvas.GetLeft(temp.CorrespondingRectangle));
            numberListInt[b] = temp;
            Canvas.SetLeft(temp.CorrespondingRectangle, leftb);
        }
        private void SwitchFloat(int a, int b)
        {
            if (a < 0 || b < 0 || a >= numberListFloat.Count || b >= numberListFloat.Count)
            {
                Variables.WriteLine($"ERROR, switching not possible with indices {a}, {b}. Count: {numberListFloat.Count}");
            }
            NewDataPoint<float> temp = numberListFloat[a];
            double leftb = Canvas.GetLeft(numberListFloat[b].CorrespondingRectangle);
            numberListFloat[a] = numberListFloat[b];
            Canvas.SetLeft(numberListFloat[a].CorrespondingRectangle, Canvas.GetLeft(temp.CorrespondingRectangle));
            numberListFloat[b] = temp;
            Canvas.SetLeft(temp.CorrespondingRectangle, leftb);
        }
        private void SwitchDouble(int a, int b)
        {
            if (a < 0 || b < 0 || a >= numberListDouble.Count || b >= numberListDouble.Count)
            {
                Variables.WriteLine($"ERROR, switching not possible with indices {a}, {b}. Count: {numberListDouble.Count}");
            }
            NewDataPoint<double> temp = numberListDouble[a];
            double leftb = Canvas.GetLeft(numberListDouble[b].CorrespondingRectangle);
            numberListDouble[a] = numberListDouble[b];
            Canvas.SetLeft(numberListDouble[a].CorrespondingRectangle, Canvas.GetLeft(temp.CorrespondingRectangle));
            numberListDouble[b] = temp;
            Canvas.SetLeft(temp.CorrespondingRectangle, leftb);
        }
    }


    



    public class NewDataPoint<T> // where T : IComparable<T> 
    {
        public Rectangle CorrespondingRectangle { get; private set; }
        public T Value { get; private set; }

        public NewDataPoint(T val)
        {
            Value = val;
            CorrespondingRectangle = null;
        }
        public NewDataPoint(T val, Rectangle rect)
        {
            Value = val;
            CorrespondingRectangle = rect;
        }

        public void SetRectangle(Rectangle rect)
        {
            CorrespondingRectangle = rect;
        }

        //public int CompareTo(NewDataPoint<T> other) 
        //{
        //    if (other == null) return 1;

        //    return Value.CompareTo(other.Value);
        //}

       

        //public static bool operator<(NewDataPoint<int> a, NewDataPoint<int> b) { return (a.Value < b.Value); }
        //public static bool operator>(NewDataPoint<int> a, NewDataPoint<int> b) { return (a.Value > b.Value); }
    }

    public class DataPointV2<T> where T : IComparable<T>
    {
        public T Value { get; private set; }

        public DataPointV2(T val)
        {
            Value = val;
        }

        public int CompareTo(DataPointV2<T> other)
        {
            return 1;
        }


        // sbyte, byte, short, ushort, int, uint, long, uint, ulong, float, double, decimal
        public static bool operator <(DataPointV2<int> lhs, DataPointV2<T> rhs)
        {
            if (rhs.Value.GetType() == typeof(int)) return lhs.Value < (int)(object)rhs.Value;
            return false;//return lhs.Value < rhs.Value;
        }
        public static bool operator >(DataPointV2<int> lhs, DataPointV2<T> rhs)
        {
            if (rhs.Value.GetType() == typeof(int)) return lhs.Value > (int)(object)rhs.Value;
            return false;//return lhs.Value > rhs.Value;
        }
    }

    /*
    public class DataPointV3 : IComparable<DataPointV3>
    {
        public int Value { get; private set; }

        public DataPointV3(int val)
        {
            Value = val;
        }

        public int CompareTo(DataPointV3 other)
        {
            return Value.CompareTo(other.Value);
        }
        
        public static bool operator <(DataPointV3 lhs, DataPointV3 rhs)
        {
            return lhs.Value < rhs.Value;
        }
        public static bool operator >(DataPointV3 lhs, DataPointV3 rhs)
        {
            return lhs.Value > rhs.Value;
        }
    }
    */
}
