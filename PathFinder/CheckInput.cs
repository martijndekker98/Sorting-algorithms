using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PathFinder
{
    class CheckInput
    {
        public delegate void DelegatePrint(string msg);
        private static DelegatePrint WriteLine = new DelegatePrint(Variables.WriteLine);
        //PreviewTextInput="EnsureNumbPreviewTextInput" TextChanged="EnsureTextBoxNotEmpty"

        public static bool EnsureNumbPreviewTextInput(object sender, TextCompositionEventArgs e, ComboBox objectType)
        {
            //WriteLine($"Text>{((TextBox)sender).Text}< and Input>{e.Text}<");
            //WriteLine($"Selection? {((TextBox)sender).SelectionStart} & {((TextBox)sender).SelectionLength}");
            //WriteLine($"The new text will be: {newText}");
            //WriteLine($"Acceptables as int: {HelperFunctions.ParsableInt(newText)} float: {HelperFunctions.ParsableRealNumber(newText)}");
            //WriteLine(Variables.regexNumbOfPoints.IsMatch(e.Text).ToString());
            //e.Handled = false;

            TextBox tb = sender as TextBox;
            string newText = tb.Text.Substring(0, tb.SelectionStart) + e.Text + tb.Text.Substring(tb.SelectionStart + tb.SelectionLength);
            if (HelperFunctions.IntegerValRequired((ComboBoxItem)objectType.SelectedItem))
            {
                WriteLine($"Requires an integer: {Variables.chRegexInteger.IsMatch(((TextBox)sender).Text)}");
                WriteLine($"The new addition: {e.Text}, acceptable pos {Variables.chRegexIntegerPos.IsMatch(e.Text)}, {Variables.chRegexInteger.IsMatch(e.Text)}");
                WriteLine($"Pos: {tb.SelectionStart} {HelperFunctions.PositiveNumberRequired((ComboBoxItem)objectType.SelectedItem)}");
                if (HelperFunctions.PositiveNumberRequired((ComboBoxItem)objectType.SelectedItem) || tb.SelectionStart > 0)
                {
                    return !Variables.chRegexIntegerPos.IsMatch(e.Text);
                }
                else
                {
                    return !Variables.chRegexInteger.IsMatch(e.Text);
                }
                return !HelperFunctions.ParsableInt(newText);
            }
            else
            {
                WriteLine("Requires a float or integer");
                //bool containsDot = tb.Text.Contains(".");
                //if (tb.SelectionStart > 0 && containsDot) return Variables.chRegexInteger.IsMatch(e.Text);
                if (tb.SelectionStart > 0) return Variables.chRegexRealNumberPos.IsMatch(e.Text);
                else return Variables.chRegexRealNumber.IsMatch(e.Text);
                return !HelperFunctions.ParsableRealNumber(newText);
            }
        }

        // NEEDS REWRITING
        public static void EnsureTextBoxNotEmpty(object sender, TextBox minVal, TextBox maxVal, TextBox numbOfPoints, ComboBox objectType)
        {
            //return;
            WriteLine("Ensure not empty");
            if (minVal != null && maxVal != null && numbOfPoints != null && objectType != null)
            {
                if (((TextBox)sender).Text.Length == 0)
                {
                    if (sender == numbOfPoints) ((TextBox)sender).Text = "1";
                    else ((TextBox)sender).Text = "0";
                }
                else if (HelperFunctions.IntegerValRequired((ComboBoxItem)objectType.SelectedItem))
                {
                    if (HelperFunctions.ConvertTextToInt(minVal.Text) > HelperFunctions.ConvertTextToInt(maxVal.Text))
                    {
                        if (sender == maxVal) { maxVal.Text = 
                                $"{HelperFunctions.ConvertTextToInt(minVal.Text) + HelperFunctions.ConvertTextToInt(numbOfPoints.Text)}"; }
                        else if (sender == minVal) { minVal.Text = $"0"; }
                    }
                }
                else
                {
                    if (HelperFunctions.ConvertTextToFloat(minVal.Text) > HelperFunctions.ConvertTextToFloat(maxVal.Text))
                    {
                        if (sender == maxVal) { maxVal.Text = 
                                $"{HelperFunctions.ConvertTextToFloat(minVal.Text) + HelperFunctions.ConvertTextToInt(numbOfPoints.Text)}"; }
                        else if (sender == minVal) { minVal.Text = $"0"; }
                    }
                }
                //else if (allowDup.IsChecked == true)  // == true because IsChecked returns a 'bool?', not a 'bool'
                //{
                //    if (HelperFunctions.IntegerValRequired((ComboBoxItem)objectType.SelectedItem))
                //    {

                //    }
                //}
            }
        }

        // Checks whether unchecking 'Allow duplicates' is possible based on min-max values 
        public static bool AllowDupUnchecked(ComboBox objectType, TextBox maxVal, TextBox minVal, TextBox numbOfPoints)
        {
            //WriteLine($"Type {objectType.SelectedItem.GetType()}");
            // When integers will be used, there have to be enough options IF duplicates are not allowed
            if (HelperFunctions.IntegerValRequired((ComboBoxItem)objectType.SelectedItem))
            {
                WriteLine($"Diff: {int.Parse(maxVal.Text) - int.Parse(minVal.Text)}, <> {int.Parse(numbOfPoints.Text)}");
                if ((int.Parse(maxVal.Text) - int.Parse(minVal.Text) + 1) < int.Parse(numbOfPoints.Text)) return true;
            }
            return false;
        }
    }
}
