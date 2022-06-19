using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;

namespace PathFinder
{
    class HelperFunctions
    {
        public delegate void DelegatePrint(string msg);
        private static DelegatePrint WriteLine;

        // Initialise WriteLine
        public static void InitialiseWriteLine()
        {
            WriteLine = new DelegatePrint(Variables.WriteLine);
        }

        // Determine whether an integer value is required
        public static bool IntegerValRequired(ComboBoxItem cbi)
        {
            return !FloatingPointAcceptable(cbi);
            //return cbi.Content.ToString() == Variables.objTypeCombo[1].Item1;
        }

        // Determine whether a floating point value is acceptable
        public static bool FloatingPointAcceptable(ComboBoxItem cbi)
        {
            return FloatingPointAcceptable((Variables.NumeralType)cbi.Tag);
        }
        public static bool FloatingPointAcceptable(Variables.NumeralType nt)
        {
            return nt == Variables.NumeralType.Float || nt == Variables.NumeralType.Double;  //|| nt == Variables.NumeralType.Decimal;
        }

        // Determine whether the number has to be positive
        public static bool PositiveNumberRequired(ComboBoxItem cbi)
        {
            return PositiveNumberRequired((Variables.NumeralType)cbi.Tag);
        }
        public static bool PositiveNumberRequired(Variables.NumeralType nt)
        {
            return (int)nt < 1;
        }

        // Determines whether the text could be parsed to an integer
        public static bool ParsableInt(string text)
        {
            return Variables.regexInteger.IsMatch(text);
        }

        // Determines whether the text could be parsed to a real number (float or double)
        public static bool ParsableRealNumber(string text)
        {
            return Variables.regexRealNumber.IsMatch(text);
        }

        // Convert a string to a float
        public static float ConvertTextToFloat(string text)
        {
            WriteLine(text);
            WriteLine(float.Parse(text).ToString());
            return float.Parse(text);
        }

        // Convert a string to an int
        public static int ConvertTextToInt(string text)
        {
            return int.Parse(text);
        }

        // Convert a string to a decimal
        public static decimal ConvertTextToDecimal(string text)
        {
            return decimal.Parse(text);
        }

        
        // Find the smallest element
        public static int FindIndexSmallest(NumberSetNewInt nsni)
        {
            int indexSmallest = nsni.ResumeIndex;
            for (int i = nsni.ResumeIndex+1; i < nsni.Count; i++)
            {
                if (nsni[i] < nsni[indexSmallest]) indexSmallest = i;
            }
            return indexSmallest;
        }
        // Find the smallest element
        public static int FindIndexSmallest(NumberSetNew nsn)
        {
            int indexSmallest = nsn.ResumeIndex;
            for (int i = nsn.ResumeIndex + 1; i < nsn.Count; i++)
            {
                if (nsn.ASmallerThanB(i, indexSmallest)) indexSmallest = i;
            }
            return indexSmallest;
        }


        // Adjust the location of the indicator
        //public static void AdjustIndicatorPosition(NumberSetNew nsn, Rectangle indicator)
        //{
        //    double left = 0;
        //    if (nsn.GetType() == typeof(NumberSetNewIntegral)) left =
        //            Canvas.GetLeft(((NumberSetNewIntegral)nsn)[nsn.ResumeIndex - 1].CorrespondingRectangle);
        //    Canvas.SetLeft(indicator, left);
        //}



        public static double ConvertTextToFloatingPoint(string text, Variables.NumeralType nt, bool min)
        {
            if (text.Length < 1)
            {
                if (nt == Variables.NumeralType.Float) return min ? float.MinValue: float.MaxValue;
                else if (nt == Variables.NumeralType.Double) return min ? double.MinValue : double.MaxValue;
                //else if (nt == Variables.NumeralType.Decimal) return min ? decimal.ToDouble(decimal.MinValue) : decimal.ToDouble(decimal.MaxValue);
            }
            double a = double.Parse(text);
            if (nt == Variables.NumeralType.Float) return min ? (float)Math.Max(a, float.MinValue) : (float)Math.Min(a, float.MaxValue);
            //else if (nt == Variables.NumeralType.Decimal) return min ? (float)Math.Max(a, (double)decimal.MinValue) : 
            //        (float)Math.Min(a, (double)decimal.MaxValue);
            return a;
        }

        public static ulong ConvertTextToIntegralPos(string text, Variables.NumeralType nt, bool min)
        {
            if (text.Length < 1)
            {
                if (nt == Variables.NumeralType.Ulong) return min ? ulong.MinValue : ulong.MaxValue;
                else if (nt == Variables.NumeralType.Uint) return min ? uint.MinValue : uint.MaxValue;
                else if (nt == Variables.NumeralType.Ushort) return min ? ushort.MinValue : ushort.MaxValue;
                else if (nt == Variables.NumeralType.Byte) return min ? byte.MinValue : byte.MaxValue;
            }
            ulong a = ulong.Parse(text);
            if (nt == Variables.NumeralType.Uint) return min ? Math.Max(a, uint.MinValue) : Math.Min(a, uint.MaxValue);
            else if (nt == Variables.NumeralType.Ushort) return min ? Math.Max(a, ushort.MinValue) : Math.Min(a, ushort.MaxValue);
            else if (nt == Variables.NumeralType.Byte) return min ? Math.Max(a, byte.MinValue) : Math.Min(a, byte.MaxValue);
            return a;
        }

        public static long ConvertTextToIntegral(string text, Variables.NumeralType nt, bool min)
        {
            if (text.Length < 1)
            {
                if (nt == Variables.NumeralType.Long) return min ? long.MinValue : long.MaxValue;
                else if (nt == Variables.NumeralType.Int) return min ? int.MinValue : int.MaxValue;
                else if (nt == Variables.NumeralType.Short) return min ? short.MinValue : short.MaxValue;
                else if (nt == Variables.NumeralType.Sbyte) return min ? sbyte.MinValue : sbyte.MaxValue;
            }
            long a = long.Parse(text);
            if (nt == Variables.NumeralType.Int) return min ? Math.Max(a, int.MinValue) : Math.Min(a, int.MaxValue);
            else if (nt == Variables.NumeralType.Short) return min ? Math.Max(a, short.MinValue) : Math.Min(a, short.MaxValue);
            else if (nt == Variables.NumeralType.Sbyte) return min ? Math.Max(a, sbyte.MinValue) : Math.Min(a, sbyte.MaxValue);
            return a;
        }
    }
}
