using CK.UnitsOfMeasure;

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace UnitConverter
{
    public enum QuantityIdentifier : int
    {
        None = 0,
        Quantity1 = 1,
        Quantity2 = 2,
    }

    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<MeasureUnit> AvailableUnits { get; set; }

        public Quantity Quantity1
        {
            get => quantity1; 
            set
            {
                quantity1 = value; 
                OnPropertyChanged("Quantity1");
                OnPropertyChanged("DisplayValue1");
            }
        }
        public Quantity Quantity2
        {
            get => quantity2;
            set
            {
                quantity2 = value;
                OnPropertyChanged("Quantity2");
                OnPropertyChanged("DisplayValue2");
            }
        }
        public string DisplayValue1
        {
            get
            {
                if (!HasEnteredQuantity1)
                {
                    return DEFAULT_DISPLAY_VALUE_1_LABEL;
                }
                else
                {
                    string valueRepresentation = Quantity1.Value.ToString();
                    return valueRepresentation;
                }
            }

            set
            {
                double newValue = 0;
                if (double.TryParse(value, out newValue))
                {
                    Quantity1 = newValue.WithUnit(selectedUnit1);
                    HasEnteredQuantity1 = true;
                    OnPropertyChanged("DisplayValue1");
                }
            }
        }
        public string DisplayValue2
        {
            get
            {
                if (!HasEnteredQuantity2)
                {
                    return DEFAULT_DISPLAY_VALUE_2_LABEL;
                }
                else
                {
                    string valueRepresentation = Quantity2.Value.ToString();
                    return valueRepresentation;
                }
            }

            set
            {
                double newValue = 0;
                if (double.TryParse(value, out newValue))
                {
                    Quantity2 = newValue.WithUnit(selectedUnit2);
                    HasEnteredQuantity2 = true;
                    OnPropertyChanged("DisplayValue2");
                }

            }
        }

        private bool HasEnteredQuantity1 = false;
        private bool HasEnteredQuantity2 = false;

        private MeasureUnit selectedUnit1 = MeasureUnit.None;
        private MeasureUnit selectedUnit2 = MeasureUnit.None;

        private Quantity quantity1 = 0.WithUnit(MeasureUnit.None);
        private Quantity quantity2 = 0.WithUnit(MeasureUnit.None);

        private const string DEFAULT_DISPLAY_VALUE_1_LABEL = "Enter a number to convert here";
        private const string DEFAULT_DISPLAY_VALUE_2_LABEL = "It's equivalent will appear here";

        public MainViewModel()
        {
            GenerateDefaultUnits();
        }

        private void GenerateDefaultUnits()
        {
            AvailableUnits = new ObservableCollection<MeasureUnit>();

            var refixes = MeasureStandardPrefix.All;
            foreach (var prefix in refixes)
            {
                var unitWithPrefix = prefix.On(MeasureUnit.Metre);
                AvailableUnits.Add(unitWithPrefix);
            }
        }

        internal void OnValueChange(QuantityIdentifier userEntry)
        {
            switch (userEntry)
            {
                case QuantityIdentifier.Quantity1:
                    HasEnteredQuantity1 = true;
                    break;
                case QuantityIdentifier.Quantity2:
                    HasEnteredQuantity2 = true;
                    break;
                default:
                    break;
            }
        }

        internal void OnUnitSelectionChanged(MeasureUnit selectedUnit, QuantityIdentifier entry)
        {
            if (selectedUnit != null)
            {

                switch (entry)
                {
                    case QuantityIdentifier.Quantity1:
                        selectedUnit1 = selectedUnit;
                        PerformConversion(QuantityIdentifier.Quantity1);
                        break;
                    case QuantityIdentifier.Quantity2:
                        selectedUnit2 = selectedUnit;
                        PerformConversion(QuantityIdentifier.Quantity2);
                        break;
                    default:
                        break;
                }
            }
        }

        internal void PerformConversion(QuantityIdentifier source = QuantityIdentifier.None, string text = "")
        {
            switch (source)
            {
                case QuantityIdentifier.Quantity1:
                    {
                        ConvertQuantity1ToSelectedUnit2(text);
                    }
                    break;
                case QuantityIdentifier.Quantity2:
                    {
                        ConvertQuantity2ToSelectedUnit1(text);
                    }
                    break;
                default:
                    break;
            }
        }

        private void ConvertQuantity2ToSelectedUnit1(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                double newValue = 0;
                if (double.TryParse(text, out newValue))
                {
                    Quantity2 = newValue.WithUnit(selectedUnit2);
                    HasEnteredQuantity1 = true;
                }
            }

            if (Quantity2.CanConvertTo(selectedUnit1))
            {
                Quantity1 = Quantity2.ConvertTo(selectedUnit1);
            }
        }

        private void ConvertQuantity1ToSelectedUnit2(string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                double newValue = 0;
                if (double.TryParse(text, out newValue))
                {
                    Quantity1 = newValue.WithUnit(selectedUnit1);
                    HasEnteredQuantity1 = true;
                }
            }
            if (Quantity1.CanConvertTo(selectedUnit2))
            {
                Quantity2 = Quantity1.ConvertTo(selectedUnit2);
            }
        }

        private void OnPropertyChanged(String info)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(info));
            }
        }

    }

}