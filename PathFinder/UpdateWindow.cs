using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace PathFinder
{
    class UpdateWindow
    {
        // The initial update to change window name
        public static void InitialUpdate(MainWindow mw)
        {
            Initialising.InitialiseControls(mw);
        }



        public static void AddVisualisationNSNIntegral(MainWindow mw, NumberSetNew nsn, int spaceBetween, long min, long max, 
            Variables.NumeralType numberType)
        {
            //Variables.WriteLine($"Type nsn: {nsn.GetType()}");
            //Variables.WriteLine($"Type nsn: {((NumberSetNewIntegral)nsn)[0].GetType()}");
            //return;
            Canvas visField = mw.VisualisationField;
            int availableHeight = (int)(visField.ActualHeight - Variables.RectangleMarginTop - Variables.RectangleMinHeight);

            if (nsn.GetType() == typeof(NumberSetNewIntegral)) AddRectanglesNSN_integral(visField, Variables.fill, Variables.border, 
                spaceBetween, min, max, (NumberSetNewIntegral)nsn, availableHeight);

            else if (nsn.GetType() == typeof(NumberSetNewInt)) AddRectanglesNSN_int(visField, Variables.fill, Variables.border, spaceBetween, 
                (int)min, (int)max, max - min, (NumberSetNewInt)nsn, availableHeight);
            else if (nsn.GetType() == typeof(NumberSetNewLong)) AddRectanglesNSN_long(visField, Variables.fill, Variables.border, 
                spaceBetween, min, max, max - min, (NumberSetNewLong)nsn, availableHeight);
            else if (nsn.GetType() == typeof(NumberSetNewShort)) AddRectanglesNSN_short(visField, Variables.fill, Variables.border, 
                spaceBetween, (short)min, (short)max, max - min, (NumberSetNewShort)nsn, availableHeight);
            else if (nsn.GetType() == typeof(NumberSetNewSbyte)) AddRectanglesNSN_sbyte(visField, Variables.fill, Variables.border,
                spaceBetween, (sbyte)min, (sbyte)max, max - min, (NumberSetNewSbyte)nsn, availableHeight);
            //AddRectanglesNSN_slim(mw.VisualisationField, new SolidColorBrush(Color.FromRgb(255, 255, 255)),
            //    new SolidColorBrush(Color.FromRgb(255, 0, 0)), spaceBetween, min, max, nsn, numberType);
        }
        
        public static void AddVisualisationNSNIntegralPos(MainWindow mw, NumberSetNew nsn, int spaceBetween, ulong min, ulong max,
            Variables.NumeralType numberType)
        {
            Canvas visField = mw.VisualisationField;
            int availableHeight = (int)(visField.ActualHeight - Variables.RectangleMarginTop - Variables.RectangleMinHeight);
            
            if (nsn.GetType() == typeof(NumberSetNewUint)) AddRectanglesNSN_uint(visField, Variables.fill, Variables.border, spaceBetween,
                (uint)min, (uint)max, max - min, (NumberSetNewUint)nsn, availableHeight);
            else if (nsn.GetType() == typeof(NumberSetNewUlong)) AddRectanglesNSN_ulong(visField, Variables.fill, Variables.border,
                spaceBetween, min, max, max - min, (NumberSetNewUlong)nsn, availableHeight);
            else if (nsn.GetType() == typeof(NumberSetNewUshort)) AddRectanglesNSN_ushort(visField, Variables.fill, Variables.border,
                spaceBetween, (ushort)min, (ushort)max, max - min, (NumberSetNewUshort)nsn, availableHeight);
            else if (nsn.GetType() == typeof(NumberSetNewByte)) AddRectanglesNSN_byte(visField, Variables.fill, Variables.border,
                spaceBetween, (byte)min, (byte)max, max - min, (NumberSetNewByte)nsn, availableHeight);
            //AddRectanglesNSN_slim(mw.VisualisationField, new SolidColorBrush(Color.FromRgb(255, 255, 255)),
            //    new SolidColorBrush(Color.FromRgb(255, 0, 0)), spaceBetween, min, max, nsn, numberType);
        }

        public static void AddVisualisationNSNFloating(MainWindow mw, NumberSetNew nsn, int spaceBetween, double min, double max,
            Variables.NumeralType numberType)
        {
            Canvas visField = mw.VisualisationField;
            int availableHeight = (int)(visField.ActualHeight - Variables.RectangleMarginTop - Variables.RectangleMinHeight);

            if (nsn.GetType() == typeof(NumberSetNewDouble)) AddRectanglesNSN_double(visField, Variables.fill, Variables.border, spaceBetween,
                min, max, max - min, (NumberSetNewDouble)nsn, availableHeight);
            else if (nsn.GetType() == typeof(NumberSetNewFloat)) AddRectanglesNSN_float(visField, Variables.fill, Variables.border,
                spaceBetween, min, max, max - min, (NumberSetNewFloat)nsn, availableHeight);
        }




        // INT
        private static void AddRectanglesNSN_int(Canvas visField, Brush fill, Brush border, int spaceBetween, int min, int max, 
            double range, NumberSetNewInt nsn, int availableHeight)
        {
            int rectWidth = (int)((visField.ActualWidth - (nsn.Count - 1) * spaceBetween) / nsn.Count);

            for (int i = 0; i < nsn.Count; i++)
            {
                double h = Variables.RectangleMinHeight + ((nsn[i].Value - min) * availableHeight) / range;
                AddRectangleSingular(h, rectWidth, fill, border, i, spaceBetween, visField, nsn[i]);
            }
        }
        // Uint
        private static void AddRectanglesNSN_uint(Canvas visField, Brush fill, Brush border, int spaceBetween, uint min, uint max, 
            double range, NumberSetNewUint nsn, int availableHeight)
        {
            int rectWidth = (int)((visField.ActualWidth - (nsn.Count - 1) * spaceBetween) / nsn.Count);
            for (int i = 0; i < nsn.Count; i++)
            {
                double h = Variables.RectangleMinHeight + ((nsn[i].Value - min) * availableHeight) / range;
                AddRectangleSingular(h, rectWidth, fill, border, i, spaceBetween, visField, nsn[i]);
            }
        }
        // Long
        private static void AddRectanglesNSN_long(Canvas visField, Brush fill, Brush border, int spaceBetween, long min, long max, 
            double range, NumberSetNewLong nsn, int availableHeight)
        {
            int rectWidth = (int)((visField.ActualWidth - (nsn.Count - 1) * spaceBetween) / nsn.Count);
            for (int i = 0; i < nsn.Count; i++)
            {
                double h = Variables.RectangleMinHeight + ((nsn[i].Value - min) * availableHeight) / range;
                AddRectangleSingular(h, rectWidth, fill, border, i, spaceBetween, visField, nsn[i]);
            }
        }
        // Ulong
        private static void AddRectanglesNSN_ulong(Canvas visField, Brush fill, Brush border, int spaceBetween, ulong min, ulong max, 
            double range, NumberSetNewUlong nsn, int availableHeight)
        {
            int rectWidth = (int)((visField.ActualWidth - (nsn.Count - 1) * spaceBetween) / nsn.Count);
            for (int i = 0; i < nsn.Count; i++)
            {
                double h = Variables.RectangleMinHeight + ((double)(nsn[i].Value - min) * availableHeight) / range;
                AddRectangleSingular(h, rectWidth, fill, border, i, spaceBetween, visField, nsn[i]);
            }
        }
        // Short
        private static void AddRectanglesNSN_short(Canvas visField, Brush fill, Brush border, int spaceBetween, short min, short max, 
            double range, NumberSetNewShort nsn, int availableHeight)
        {
            int rectWidth = (int)((visField.ActualWidth - (nsn.Count - 1) * spaceBetween) / nsn.Count);
            for (int i = 0; i < nsn.Count; i++)
            {
                double h = Variables.RectangleMinHeight + ((nsn[i].Value - min) * availableHeight) / range;
                AddRectangleSingular(h, rectWidth, fill, border, i, spaceBetween, visField, nsn[i]);
            }
        }
        // Ushort
        private static void AddRectanglesNSN_ushort(Canvas visField, Brush fill, Brush border, int spaceBetween, ushort min, ushort max, 
            double range, NumberSetNewUshort nsn, int availableHeight)
        {
            int rectWidth = (int)((visField.ActualWidth - (nsn.Count - 1) * spaceBetween) / nsn.Count);
            for (int i = 0; i < nsn.Count; i++)
            {
                double h = Variables.RectangleMinHeight + ((nsn[i].Value - min) * availableHeight) / range;
                AddRectangleSingular(h, rectWidth, fill, border, i, spaceBetween, visField, nsn[i]);
            }
        }
        // Byte
        private static void AddRectanglesNSN_byte(Canvas visField, Brush fill, Brush border, int spaceBetween, byte min, byte max,
            double range, NumberSetNewByte nsn, int availableHeight)
        {
            int rectWidth = (int)((visField.ActualWidth - (nsn.Count - 1) * spaceBetween) / nsn.Count);
            for (int i = 0; i < nsn.Count; i++)
            {
                double h = Variables.RectangleMinHeight + ((nsn[i].Value - min) * availableHeight) / range;
                AddRectangleSingular(h, rectWidth, fill, border, i, spaceBetween, visField, nsn[i]);
            }
        }
        // Sbyte
        private static void AddRectanglesNSN_sbyte(Canvas visField, Brush fill, Brush border, int spaceBetween, sbyte min, sbyte max,
            double range, NumberSetNewSbyte nsn, int availableHeight)
        {
            int rectWidth = (int)((visField.ActualWidth - (nsn.Count - 1) * spaceBetween) / nsn.Count);
            for (int i = 0; i < nsn.Count; i++)
            {
                double h = Variables.RectangleMinHeight + ((nsn[i].Value - min) * availableHeight) / range;
                AddRectangleSingular(h, rectWidth, fill, border, i, spaceBetween, visField, nsn[i]);
            }
        }




        // Double
        private static void AddRectanglesNSN_double(Canvas visField, Brush fill, Brush border, int spaceBetween, double min, double max,
            double range, NumberSetNewDouble nsn, int availableHeight)
        {
            int rectWidth = (int)((visField.ActualWidth - (nsn.Count - 1) * spaceBetween) / nsn.Count);
            for (int i = 0; i < nsn.Count; i++)
            {
                double h = Variables.RectangleMinHeight + ((nsn[i].Value - min) * availableHeight) / range;
                AddRectangleSingular(h, rectWidth, fill, border, i, spaceBetween, visField, nsn[i]);
            }
        }
        // Float
        private static void AddRectanglesNSN_float(Canvas visField, Brush fill, Brush border, int spaceBetween, double min, double max,
            double range, NumberSetNewFloat nsn, int availableHeight)
        {
            int rectWidth = (int)((visField.ActualWidth - (nsn.Count - 1) * spaceBetween) / nsn.Count);
            for (int i = 0; i < nsn.Count; i++)
            {
                double h = Variables.RectangleMinHeight + ((nsn[i].Value - min) * availableHeight) / range;
                AddRectangleSingular(h, rectWidth, fill, border, i, spaceBetween, visField, nsn[i]);
            }
        }




        // Add a rectangle to the DataWrapper, where the height = DataWrapper.Value, and the position 
        // is based on the element's index in the NumberSetNew list
        private static void AddRectangleSingular(double h, double rectWidth, Brush fill, Brush border, int i, int spaceBetween,
            Canvas visField, DataWrapper dw)
        {
            // Instantiate a new rectangle
            Rectangle r = new Rectangle
            {
                Width = rectWidth,
                Height = h,
                //Fill = fill,
                Stroke = border,
                StrokeThickness = 4,
            };

            // Set the location of the rectangles
            Canvas.SetLeft(r, (i * rectWidth) + (i * spaceBetween));
            Canvas.SetBottom(r, 0);
            visField.Children.Add(r);
            dw.SetRectangle(r);
        }




        //
        // OLD
        //

        // OLD
        private static void AddRectanglesNSN_slimIntegral(Canvas visField, Brush fill, Brush border, int spaceBetween, long min, long max,
            NumberSetNew nsn, Variables.NumeralType numberType)
        {
            int availableHeight = (int)(visField.ActualHeight - Variables.RectangleMarginTop - Variables.RectangleMinHeight);

            if (nsn.GetType() == typeof(NumberSetNewIntegral)) AddRectanglesNSN_integral(visField, fill, border, spaceBetween, min, max,
                (NumberSetNewIntegral)nsn, availableHeight);
            else if (nsn.GetType() == typeof(NumberSetNewInt)) AddRectanglesNSN_int(visField, fill, border, spaceBetween, (int)min, (int)max,
                max - min, (NumberSetNewInt)nsn, availableHeight);
            else if (nsn.GetType() == typeof(NumberSetNewLong)) AddRectanglesNSN_long(visField, fill, border, spaceBetween, min, max,
                max - min, (NumberSetNewLong)nsn, availableHeight);
            else if (nsn.GetType() == typeof(NumberSetNewShort)) AddRectanglesNSN_short(visField, fill, border, spaceBetween, (short)min,
                (short)max, max - min, (NumberSetNewShort)nsn, availableHeight);
        }


        // OLD
        private static void AddRectanglesNSN_integral(Canvas visField, Brush fill, Brush border, int spaceBetween, double min, double max,
            NumberSetNewIntegral nsn, int availableHeight)
        {
            int rectWidth = (int)((visField.ActualWidth - (nsn.Count - 1) * spaceBetween) / nsn.Count);
            for (int i = 0; i < nsn.Count; i++)
            {
                double h = Variables.RectangleMinHeight + ((int)nsn[i].Value - min) / (max - min) * availableHeight;
                AddRectangleSingular(h, rectWidth, fill, border, i, spaceBetween, visField, nsn[i]);
            }
        }



        private static void AddRectanglesNSN(Canvas visField, Brush fill, Brush border, int spaceBetween, float min, float max,
            NumberSetNew nsn, Variables.NumeralType numberType)
        {
            double width = visField.ActualWidth;
            double height = visField.ActualHeight;

            int rectWidth = (int)((width - (nsn.Count - 1) * spaceBetween) / nsn.Count);
            int availableHeight = (int)(height - Variables.RectangleMarginTop - Variables.RectangleMinHeight);
            double bottomY = height - Variables.RectangleMarginTop;

            for (int i = 0; i < nsn.Count; i++)
            {
                double h = Variables.RectangleMinHeight + GetDataWrapperRectHeight(numberType, nsn[i], availableHeight, min, max - min);

                // Instantiate a new rectangle
                Rectangle r = new Rectangle
                {
                    Width = rectWidth,
                    Height = h,
                    Fill = fill,
                    Stroke = border,
                };

                // Set the location of the rectangles
                Canvas.SetLeft(r, (i * rectWidth) + (i * spaceBetween));
                Canvas.SetBottom(r, 0);
                visField.Children.Add(r);
                nsn[i].SetRectangle(r);
            }
        }

        private static double GetDataWrapperRectHeight(Variables.NumeralType numberType, DataWrapper dw, int availableHeight, float min, 
            float diff)
        {
            //((x - min) / diff) * availableHeigth;
            if (numberType == Variables.NumeralType.Int) return (((int)((IntegralWrapper)dw).Value - min) / diff) * availableHeight;
            return 10.0d;
        }



        public static Rectangle AddSortingIndicator(MainWindow mw, NumberSetNew nsn, Brush indicatorFill)
        {
            Rectangle r0 = null;
            if (nsn.GetType() == typeof(NumberSetNewInt)) r0 = ((NumberSetNewInt)nsn)[0].CorrespondingRectangle;
            if (r0 == null)
            {
                Variables.WriteLine("Rectangle is null! @AddSortingIndicator()");
                return null;
            }
            Rectangle indicator = new Rectangle
            {
                Width = r0.Width,
                Height = mw.VisualisationField.ActualHeight - Variables.RectangleMarginTop,
                Fill = indicatorFill,
                Stroke = indicatorFill,
            };
            Canvas.SetLeft(indicator, 0);
            Canvas.SetBottom(indicator, 0);
            mw.VisualisationField.Children.Add(indicator);
            //indicator.Visibility = System.Windows.Visibility.Hidden;
            Canvas.SetZIndex(indicator, Canvas.GetZIndex(r0)-1);
            return indicator;
        }






        // Add the visualisation
        public static void AddVisualisation(MainWindow mw)
        {
            Variables.WriteLine($"Width: {mw.VisualisationField.ActualWidth}, height: {mw.VisualisationField.ActualHeight}");

            List<int> test = Enumerable.Range(0, 54 + 1).ToList();
            AddRectangles(mw.VisualisationField, new SolidColorBrush(Color.FromRgb(255, 255, 255)), new SolidColorBrush(Color.FromRgb(255, 0, 0)), 0, 0, 54, test);
        }
        public static void AddVisualisation2<T>(MainWindow mw, List<T> listDataPoints)
        {
            Variables.WriteLine($"Width: {mw.VisualisationField.ActualWidth}, height: {mw.VisualisationField.ActualHeight}");

            //List<int> test = Enumerable.Range(0, 54 + 1).ToList();
            //AddRectangles(mw.VisualisationField, new SolidColorBrush(Color.FromRgb(255, 255, 255)), new SolidColorBrush(Color.FromRgb(255, 0, 0)), 0, 0, 54, test);
            //List<Variables.DataPoint<int>> listDataPoints = GenerateRandomSet.GetListOfDataPoints(test);

            //AddRectanglesNew(mw.VisualisationField, new SolidColorBrush(Color.FromRgb(255, 255, 255)), new SolidColorBrush(Color.FromRgb(255, 0, 0)), 
            //    0, 0, 54, listDataPoints);
            //return (List<Variables.DataPoint<T>>)Convert.ChangeType(listDataPoints, typeof(List<Variables.DataPoint<T>>));
        }

        public static void AddVisualisationNew<T>(MainWindow mw, List<NewDataPoint<T>> listDataPoints)
        {
            Variables.WriteLine($"Width: {mw.VisualisationField.ActualWidth}, height: {mw.VisualisationField.ActualHeight}");
            Variables.WriteLine($"Type: {listDataPoints[0].Value.GetType()} <> {listDataPoints[0].Value.GetType() == typeof(int)}");
            AddRectanglesNew(mw.VisualisationField, new SolidColorBrush(Color.FromRgb(255, 255, 255)), 
                new SolidColorBrush(Color.FromRgb(255, 0, 0)),  0, 0, 54, listDataPoints);
        }

        // Add a number of (numbOfRects) rectangles to the canvas (visField) with the color c
        private static void AddRectangles<T>(Canvas visField, Brush fill, Brush border, int spaceBetween, float min, float max, 
            List<Variables.DataPoint<T>> values)
        {
            Type valueType = values[0].Value.GetType();
            double width = visField.ActualWidth;
            double height = visField.ActualHeight;

            int rectWidth = (int)((width - (values.Count - 1) * spaceBetween) / values.Count);
            int availableHeight = (int)(height - Variables.RectangleMarginTop - Variables.RectangleMinHeight);
            double bottomY = height - Variables.RectangleMarginTop;
            for (int i = 0; i < values.Count; i++)
            {
                double h = Variables.RectangleMinHeight;
                if (valueType == typeof(int)) { h += GetHeight(availableHeight, min, max - min, (int)(object)values[i].Value); }
                else if (valueType == typeof(float)) { h += GetHeight(availableHeight, min, max - min, (float)(object)values[i].Value); }
                else if (valueType == typeof(double)) { h += GetHeight(availableHeight, min, max - min, (double)(object)values[i].Value); }

                // Instantiate a new rectangle
                Rectangle r = new Rectangle
                {
                    Width = rectWidth,
                    Height = h,
                    Fill = fill,
                    Stroke = border,
                };

                // Set the location of the rectangles
                Canvas.SetLeft(r, (i * rectWidth) + (i * spaceBetween));
                Canvas.SetBottom(r, 0);
                visField.Children.Add(r);
                values[i].SetRectangle(r);
            }
        }

        // Compute the height of the rectangle
        private static double GetHeight(int availableHeigth, float min, float diff, int x) { return ((x - min) / diff) * availableHeigth; }
        private static double GetHeight(int availableHeigth, float min, float diff, float x) { return ((x - min) / diff) * availableHeigth; }
        private static double GetHeight(int availableHeigth, float min, float diff, double x) { return ((x - min) / diff) * availableHeigth; }

        // Add a number of (numbOfRects) rectangles to the canvas (visField) with the color c
        private static void AddRectangles<T>(Canvas visField, Brush fill, Brush border, int spaceBetween, float min, float max, List<T> values)
        {
            Variables.WriteLine($"Add Rectangles: {values.Count} count, {values[0].GetType()}");
            Type valueType = values[0].GetType();
            double width = visField.ActualWidth;
            double height = visField.ActualHeight;

            int rectWidth = (int)((width - (values.Count - 1) * spaceBetween) / values.Count);
            int availableHeight = (int)(height - Variables.RectangleMarginTop - Variables.RectangleMinHeight);
            double bottomY = height - Variables.RectangleMarginTop;
            for (int i = 0; i < values.Count; i++)
            {
                double h = Variables.RectangleMinHeight;
                if (valueType == typeof(int)) { h += GetHeight(availableHeight, min, max - min, (int)(object)values[i]); }
                else if (valueType == typeof(float)) { h += GetHeight(availableHeight, min, max - min, (float)(object)values[i]); }
                else if (valueType == typeof(double)) { h += GetHeight(availableHeight, min, max - min, (double)(object)values[i]); }

                if (i == 0)
                {

                }

                // Instantiate a new rectangle
                Rectangle r = new Rectangle
                {
                    Width = rectWidth,
                    Height = h,
                    Fill = fill,
                    Stroke = border,
                };

                // Set the location of the rectangles
                Canvas.SetLeft(r, (i * rectWidth) + (i * spaceBetween));
                Canvas.SetBottom(r, 0);
                visField.Children.Add(r);
            }
        }

        private static void AddRectanglesNew<T>(Canvas visField, Brush fill, Brush border, int spaceBetween, float min, float max, 
            List<NewDataPoint<T>> values)
        {
            Variables.WriteLine($"Add Rectangles: {values.Count} count, {values[0].Value.GetType()}");
            Type valueType = values[0].Value.GetType();
            double width = visField.ActualWidth;
            double height = visField.ActualHeight;

            int rectWidth = (int)((width - (values.Count - 1) * spaceBetween) / values.Count);
            int availableHeight = (int)(height - Variables.RectangleMarginTop - Variables.RectangleMinHeight);
            double bottomY = height - Variables.RectangleMarginTop;
            for (int i = 0; i < values.Count; i++)
            {
                double h = Variables.RectangleMinHeight;
                if (valueType == typeof(int)) { h += GetHeight(availableHeight, min, max - min, (int)(object)values[i].Value); }
                else if (valueType == typeof(float)) { h += GetHeight(availableHeight, min, max - min, (float)(object)values[i].Value); }
                else if (valueType == typeof(double)) { h += GetHeight(availableHeight, min, max - min, (double)(object)values[i].Value); }

                // Instantiate a new rectangle
                Rectangle r = new Rectangle
                {
                    Width = rectWidth,
                    Height = h,
                    Fill = fill,
                    Stroke = border,
                };

                // Set the location of the rectangles
                Canvas.SetLeft(r, (i * rectWidth) + (i * spaceBetween));
                Canvas.SetBottom(r, 0);
                visField.Children.Add(r);

                values[i].SetRectangle(r);
            }
        }
    }
}
