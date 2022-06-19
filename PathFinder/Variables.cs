using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PathFinder
{
    class Variables
    {
        public static void WriteLine(string msg)
        {
            if (AllowPrinting) Console.WriteLine(msg);
        }


        // Strings used in the application
        public static readonly string NameWindow = "Sorting";
        public static readonly string randomiseContent = "Generate number set";
        public static readonly string performAction = "Sort numbers";

        public static readonly string numbOfPointsText = "Number of points:";
        public static readonly string minValText = "Minimum value:";
        public static readonly string maxValText = "Maximum value:";


        // Settings for buttons
        public static readonly HorizontalAlignment controlHorizAlign = HorizontalAlignment.Left;
        public static readonly VerticalAlignment controlVerticAlign = VerticalAlignment.Top;
        //public static readonly float controlWidth = 160f;
        public static readonly float controlHeight = 25f;
        public static readonly Thickness controlMargin = new Thickness(10, 0, 0, 10);


        // Settings for TextBlocks
        public static readonly HorizontalAlignment textBlockHorizAlign = HorizontalAlignment.Left;
        public static readonly HorizontalAlignment textBoxHorizAlign = HorizontalAlignment.Right;

        // Brushes
        public static Brush indicatorFill = new SolidColorBrush(Color.FromRgb(0, 0, 255));
        public static Brush fill = new SolidColorBrush(Color.FromRgb(255, 255, 255));
        public static Brush border = new SolidColorBrush(Color.FromRgb(255, 0, 0));


        // Enums for randomising data
        public enum GenOption
        {
            Random,
            Sorted,
            Reversed,
            Testing // NEEDS TO BE REMOVED LATER
        }

        // sbyte, byte, short, ushort, int, uint, long, uint, ulong, float, double
        public enum NumeralType
        {
            Float = 1,
            Double = 3,
            Int = 5,
            Uint = 0,
            Long = 7,
            Ulong = 2,
            Short = 9,
            Ushort = 4,
            Byte = 6, 
            Sbyte = 11
        }
        // ulong, uint, ushort, byte
        

        public struct DataPoint<T>
        {
            public Rectangle CorrespondingRectangle { get; private set; }
            public T Value { get; private set; }

            public DataPoint(T val) {
                Value = val;
                CorrespondingRectangle = null;
            }
            public DataPoint(T val, Rectangle rect)
            {
                Value = val;
                CorrespondingRectangle = rect;
            }

            public void SetRectangle(Rectangle rect)
            {
                CorrespondingRectangle = rect;
            }
        }

        // Content for comboboxes
        public static readonly Tuple<GenOption>[] genOptsCombo = new Tuple<GenOption>[] { new Tuple<GenOption>(GenOption.Random),
            new Tuple<GenOption>(GenOption.Sorted), new Tuple<GenOption>(GenOption.Reversed), new Tuple<GenOption>(GenOption.Testing) };
        public static readonly Tuple<string, NumeralType>[] objTypeCombo = new Tuple<string, NumeralType>[] {
            //new Tuple<string, NumeralType>("Float", NumeralType.Float),
            new Tuple<string, NumeralType>("Int", NumeralType.Int),
            //new Tuple<string, NumeralType>("Double", NumeralType.Double)
        };
        //public static readonly Tuple<SortingAlgorithms.BasisSortAlgorithm>[] SortingAlgorithms = new Tuple<SortingAlgorithms.BasisSortAlgorithm>[] {

        //};

        public static int ObjTypeInitialSelected = 0;


        // Regex expressions used
        public static readonly Regex regexNumbOfPoints      = new Regex("^[1-9][0-9]*$"); //regex that matches disallowed text
        public static readonly Regex regexInteger           = new Regex("^[-]?[0-9]+$"); //regex that matches disallowed text
        public static readonly Regex regexRealNumber        = new Regex("^[-]?[0-9]*[.]?[0-9]*$"); //regex that matches disallowed text
        // Regex for chars
        public static readonly Regex chRegexRealNumberPos   = new Regex("^([0-9][.])$");
        public static readonly Regex chRegexRealNumber      = new Regex("^([-][0-9][.])$");
        public static readonly Regex chRegexIntegerPos      = new Regex("^[0-9]$");
        public static readonly Regex chRegexInteger         = new Regex("^([-0-9])$");

        // Settings for the rectangles
        public static int RectangleMarginTop = 10;
        public static int RectangleMinHeight = 10;
        public static int SpaceBetween = 2;


        // Whether to allow printing/debugging
        public static bool AllowPrinting = true;


        // Testing numberz
        public static readonly List<int> TestingNumbers = new List<int>() { 3, 7, 8, 5, 2, 1, 9, 5, 4 };//{ 11, 25, 12, 22, 64 };
    }
}
