using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace PathFinder
{
    class Initialising
    {
        public delegate void DelegatePrint(string msg);
        private static DelegatePrint WriteLine;

        public static void InitialiseControls(MainWindow mw)
        {
            WriteLine = new DelegatePrint(Variables.WriteLine);
            WriteLine("Start initialising the controls");

            // Update the title of the window
            mw.Title = Variables.NameWindow;

            // Set button content
            mw.randomise.Content = Variables.randomiseContent;
            mw.performAction.Content = Variables.performAction;

            // Update the TextBlocks' text
            UpdateTextBlockContent(mw);

            // Update the settings for buttons in the control grid
            UpdateControlGridControls(mw);

            // Fill the comboBoxes
            FillComboBoxes(mw);

            WriteLine("Finished initialising the controls");
            HelperFunctions.InitialiseWriteLine();
        }

        private static void UpdateTextBlockContent(MainWindow mw)
        {
            // Update the text of the TextBlocks
            mw.numbOfPointsText.Text = Variables.numbOfPointsText;
            mw.minValText.Text = Variables.minValText;
            mw.maxValText.Text = Variables.maxValText;
        }

        private static void UpdateControlGridControls(MainWindow mw)
        {
            // Update the settings for the buttons in the control grid
            foreach (UIElement item in mw.controlGridStackPanel.Children)
            {
                if (item.GetType() == typeof(Button))
                {
                    Button but = (Button)item;
                    // Alignment
                    //but.HorizontalAlignment = Variables.controlHorizAlign;
                    but.VerticalAlignment = Variables.controlVerticAlign;

                    // Size
                    //but.Width = Variables.controlWidth;
                    but.Height = Variables.controlHeight;

                    // Margin
                    but.Margin = Variables.controlMargin;
                }
                else if (item.GetType() == typeof(ComboBox))
                {
                    ComboBox cb = (ComboBox)item;
                    // Alignment
                    //cb.HorizontalAlignment = Variables.controlHorizAlign;
                    cb.VerticalAlignment = Variables.controlVerticAlign;

                    // Size
                    //cb.Width = Variables.controlWidth;

                    // Margin
                    cb.Margin = Variables.controlMargin;
                }
                else if (item.GetType() == typeof(Grid))
                {
                    Grid sp = (Grid)item;
                    foreach (UIElement spChild in sp.Children)
                    {
                        WriteLine($"Child type {spChild.GetType()}");
                        if (spChild.GetType() == typeof(TextBlock))
                        {
                            TextBlock tb = (TextBlock)spChild;
                            // Alignment
                            tb.HorizontalAlignment = Variables.textBlockHorizAlign;
                        }
                        else if (spChild.GetType() == typeof(TextBox))
                        {
                            WriteLine("TEXTBOX");
                            TextBox tb = (TextBox)spChild;
                            // Alignment
                            tb.HorizontalAlignment = Variables.textBoxHorizAlign;
                        }
                    }
                }
            }
        }

        private static void FillComboBoxes(MainWindow mw)
        {
            WriteLine("Start filling the comboboxes");

            // Fill the generate options combobox
            FillComboBox(mw.generateOptions, Variables.genOptsCombo);

            // Fill the object type combobox
            FillComboBox(mw.objectType, Variables.objTypeCombo, selectedIndex: Variables.ObjTypeInitialSelected);

            // Fill the algorithms combobox
            FillAlgorithmsComboBox(mw.algorithmToUse);
        }

        private static void FillComboBox(ComboBox generateOptions, object[] itemList, int selectedIndex = 0)
        {
            // For each tuple in the array, add a new ComboBoxItem
            foreach (var item in itemList)
            {
                if (item.GetType() == typeof(Tuple<string>)) WriteLine($"Item: {((Tuple<string>)item).Item1}");
                else if (item.GetType() == typeof(Tuple<string, Variables.NumeralType>))
                    WriteLine($"Item: {((Tuple<string, Variables.NumeralType>)item).Item1}, tag: {((Tuple<string, Variables.NumeralType>)item).Item2}");

                ComboBoxItem cbi = new ComboBoxItem();
                if (item.GetType() == typeof(Tuple<string>)) cbi.Content = ((Tuple<string>)item).Item1;
                else if (item.GetType() == typeof(Tuple<string, Variables.NumeralType>))
                {
                    Tuple<string, Variables.NumeralType> tup = item as Tuple<string, Variables.NumeralType>;
                    cbi.Content = ((Tuple<string, Variables.NumeralType>)item).Item1;
                    WriteLine($"Type: {tup.Item2.GetType()}");
                    cbi.Tag = ((Tuple<string, Variables.NumeralType>)item).Item2;
                }
                else if (item.GetType() == typeof(Tuple<Variables.GenOption>)) cbi.Content = ((Tuple<Variables.GenOption>)item).Item1;
                else WriteLine("ERROR IN FILLCOMBOBOX");
                generateOptions.Items.Add(cbi);
            }
            // Set the initially selected ComboBoxItem
            generateOptions.SelectedIndex = selectedIndex;
        }

        // Fill the combobox containing all the sorting algorithms (alphabetically sorted)
        private static void FillAlgorithmsComboBox(ComboBox atu)
        {
            var q = (from t in Assembly.GetExecutingAssembly().GetTypes() where t.IsClass && !t.IsAbstract
                    && t.IsSubclassOf(typeof(SortingAlgorithms.BasisSortAlgorithm)) select t).ToList();
            List<Tuple<string, int>> opts = new List<Tuple<string, int>>();
            for (int i = 0; i < q.Count; i++)
            {
                opts.Add(new Tuple<string, int>(((SortingAlgorithms.BasisSortAlgorithm)(Activator.CreateInstance(q[i]))).GetName(), i));
            }
            opts.Sort((x, y) => x.Item1.CompareTo(y.Item1));
            for (int i = 0; i < opts.Count; i++)
            {
                ComboBoxItem cbi = new ComboBoxItem
                {
                    Content = opts[i].Item1,
                    Tag = q[opts[i].Item2],
                };
                atu.Items.Add(cbi);
            }
            atu.SelectedIndex = 0;
        }
    }
}
