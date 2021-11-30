using CK.UnitsOfMeasure;

using FluentAssertions;

using NUnit.Framework;

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace UnitConverter
{

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly MainViewModel ViewModel;

        public MainWindow()
        {
            InitializeComponent();
            ViewModel = new MainViewModel();
            DataContext = ViewModel;
            ResizeMode = ResizeMode.NoResize;
        }

        private void Unit1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var selectedUnit = e.AddedItems[0] as MeasureUnit;
                ViewModel?.OnUnitSelectionChanged(selectedUnit, QuantityIdentifier.Quantity1);
            }
        }

        private void Unit2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var selectedUnit = e.AddedItems[0] as MeasureUnit;
                ViewModel?.OnUnitSelectionChanged(selectedUnit, QuantityIdentifier.Quantity2);
            }
        }

        private void Value1_TextChanged(object sender, TextChangedEventArgs e)
        {
            ViewModel?.OnValueChange(QuantityIdentifier.Quantity1);
        }

        private void Value2_TextChanged(object sender, TextChangedEventArgs e)
        {
            ViewModel?.OnValueChange(QuantityIdentifier.Quantity2);
        }

        private void Value1_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var textBox = sender as TextBox;
                if (textBox != null)
                {
                    ViewModel?.PerformConversion(QuantityIdentifier.Quantity1, textBox.Text);
                }
            }
        }

        private void Value2_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var textBox = sender as TextBox;
                if (textBox != null)
                {
                    ViewModel?.PerformConversion(QuantityIdentifier.Quantity2, textBox.Text);
                }
            }
        }

    }
}
