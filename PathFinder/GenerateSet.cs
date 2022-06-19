using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PathFinder
{
    class GenerateSet
    {
        public delegate void DelegatePrint(string msg);
        private static DelegatePrint WriteLine = new DelegatePrint(Variables.WriteLine);

        public static NumberSetNew GenerateNumberSet(MainWindow mw, Variables.NumeralType nt, Random rand)
        {
            Variables.GenOption genOption = (Variables.GenOption)((ComboBoxItem)mw.generateOptions.SelectedItem).Content;
            if (HelperFunctions.FloatingPointAcceptable(nt))     return GenerateNumberSetFloating(mw, nt, rand, genOption); 
            else if (HelperFunctions.PositiveNumberRequired(nt)) return GenerateNumberSetPosIntegral(mw, nt, rand, genOption);
            else                                                 return GenerateNumberSetIntegral(mw, nt, rand, genOption);
            /*
            //int minV = (int)Math.Round(HelperFunctions.ConvertTextToFloat(minVal.Text));
            decimal minVal_ = HelperFunctions.ConvertTextToDecimal(mw.minVal.Text);
            int minV = 0;
            int maxV = (int)Math.Round(HelperFunctions.ConvertTextToFloat(mw.maxVal.Text));
            int points = HelperFunctions.ConvertTextToInt(mw.numbOfPoints.Text);
            Variables.WriteLine($"MinVal: {minVal_}");

            points = 5;
            maxV = 64;

            bool useRectangles = mw.VisualisationField.ActualWidth > points;
            WriteLine($"MinValue: {minV}, max: {maxV}, points: {points}");
            int test1 = -1;
            WriteLine($"Testje: {(byte)test1}");

            NumberSetNew nsn = GenerateRandomSet.GenRandomList(minV, maxV, points, (bool)mw.allowDup.IsChecked, nt, rand, 
                useRectangles, genOption);
            if (nsn != null)
            {
                if (useRectangles) UpdateWindow.AddVisualisationNSN(mw, nsn, 0, minV, maxV, nt);
                foreach (DataWrapper dw in nsn) WriteLine($"Value: {dw.Value}");
            }
            return nsn;
            */
        }


        private static NumberSetNew GenerateNumberSetPosIntegral(MainWindow mw, Variables.NumeralType nt, Random rand, 
            Variables.GenOption genOption)
        {
            // Need to be changed such that they follow the min/max values of genOption
            ulong minV = HelperFunctions.ConvertTextToIntegralPos(mw.minVal.Text, nt, true);
            ulong maxV = HelperFunctions.ConvertTextToIntegralPos(mw.maxVal.Text, nt, false);
            int points = HelperFunctions.ConvertTextToInt(mw.numbOfPoints.Text);
            bool useRectangles = mw.VisualisationField.ActualWidth > points;

            NumberSetNew nsn = GenerateRandomSet.GenRandomListIntegralPos(minV, maxV, points, (bool)mw.allowDup.IsChecked, nt, rand,
                useRectangles, genOption);
            if (nsn != null)
            {
                if (useRectangles) UpdateWindow.AddVisualisationNSNIntegralPos(mw, nsn, Variables.SpaceBetween, minV, maxV, nt);
            }
            return nsn;
        }

        private static NumberSetNew GenerateNumberSetIntegral(MainWindow mw, Variables.NumeralType nt, Random rand, Variables.GenOption genOption)
        {
            // Need to be changed such that they follow the min/max values of genOption
            long minV = HelperFunctions.ConvertTextToIntegral(mw.minVal.Text, nt, true);
            long maxV = HelperFunctions.ConvertTextToIntegral(mw.maxVal.Text, nt, false);
            int points = HelperFunctions.ConvertTextToInt(mw.numbOfPoints.Text);
            bool useRectangles = mw.VisualisationField.ActualWidth > points;

            NumberSetNew nsn = GenerateRandomSet.GenRandomListIntegral(minV, maxV, points, (bool)mw.allowDup.IsChecked, nt, rand,
                useRectangles, genOption);
            if (nsn != null)
            {
                if (useRectangles) UpdateWindow.AddVisualisationNSNIntegral(mw, nsn, Variables.SpaceBetween, minV, maxV, nt);
            }
            return nsn;
        }


        private static NumberSetNew GenerateNumberSetFloating(MainWindow mw, Variables.NumeralType nt, Random rand, Variables.GenOption genOption)
        {
            // Need to be changed such that they follow the min/max values of genOption
            double minV = HelperFunctions.ConvertTextToFloatingPoint(mw.minVal.Text, nt, true);
            double maxV = HelperFunctions.ConvertTextToFloatingPoint(mw.maxVal.Text, nt, false);
            int points = HelperFunctions.ConvertTextToInt(mw.numbOfPoints.Text);
            bool useRectangles = mw.VisualisationField.ActualWidth > points;

            NumberSetNew nsn = GenerateRandomSet.GenRandomListFloating(minV, maxV, points, (bool)mw.allowDup.IsChecked, nt, rand, 
                useRectangles, genOption);
            if (nsn != null)
            {
                if (useRectangles) UpdateWindow.AddVisualisationNSNFloating(mw, nsn, Variables.SpaceBetween, minV, maxV, nt);
            }
            return nsn;
        }
    }
}
