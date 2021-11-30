Simple Unit Converter
--------------------------------

Decisions :
- Use a known library for Units of Measure : [CK-UnitsOfMeasure](https://github.com/Invenietis/CK-UnitsOfMeasure)
- Mimic the behaviour of the Unit converter of the Microsoft Calculator, but without the virtual keyboard.
- Minimal design, don't tell the user anything except to enter a number. It is assumed the user will press the enter key at one point.

Reason for using a ViewModel : one place for the functionality of conversion and initialization.
All UI events remain in the MainWindow.cs file.

Known Issues :
- A lot of repetion due to the logic of separating both the units and the values.
- Use of the Enter key to tell the UI to perform the conversion, as live conversion proved dificult to handle in a short time.
- Would need a way not to fire the OnChange events at the first run.

TODO
-------------
- Add sections related to the units of measure that we want, such as Power, Energy, Weight, Temperature.
  - This could be done via a .json containing the section and then the wanted units : {name:'Joule', symbol:'J', formula:'1 J'}.
  - In GenerateDefaultUnits we could then generate all base units per section, their prefixed versions, and then get the other units from the json and create aliases according to the formulae.

Personal Issues
-------------
Had some trouble with OnPropertyChange event getting removed after the first changes are called, this was fixed by commenting the data context in the MainWIndow.xaml. Reason for this issue is unknown to me.
Also had quite some trouble binding the control content to the properties, I was always missing the DataContext. This is what I get for using libraries all the time. The documentation's samples are also quite convoluted and they never give a simple example that doesn't use a static resource declared in XAML.
