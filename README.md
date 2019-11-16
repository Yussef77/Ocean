# Oceanware Ocean Library
The Ocean Library is a cross-platform library that provides API's for WPF, Blazor, Xamarin, UWP, WebApi, Silverlight, and .NET Class Libraries. The Ocean Library has builds for .NET Framework, .NET Standard 2.0 and .NET Core 3.0.

## Build Status
|Ocean|[![Build Status](https://dev.azure.com/re-booting/Oceanware.Ocean/_apis/build/status/OceanLibrary.Ocean?branchName=master)](https://dev.azure.com/re-booting/Oceanware.Ocean/_build/latest?definitionId=2&branchName=master) |
|---|---|

## Documentation
[Ocean Library Documentation](https://oceanlibrary.github.io/Ocean-Documentation/)

## Code Snippets
See the `SetPropertyCodeSnippets.zip` file for two code snippets for easily adding entity properties to entity classes that derive from `BusinessEntityBase`.

## NuGet Packages
- Package: [Oceanware.Ocean](https://www.nuget.org/packages/Oceanware.Ocean/)
- Package: [Oceanware.Ocean.Blazor Validator](https://www.nuget.org/packages/Oceanware.Ocean.Blazor/)

### Oceanware.Ocean
`Oceanware.Ocean` is the cross-platform library.

Package: [Oceanware.Ocean](https://www.nuget.org/packages/Oceanware.Ocean/)

Library Features:

- Validation
  - Declarative rule attributes that decorate properties
  - Rich set of provided validation rules
  - Easy to create additional validation rules and attributes.
  - Dynamically added rules that are shared across all instances of the type
  - Dynamically added rules that are instance based, each instance has it's own copy of the rule to be applied
  - RuleSet's allow object rules to be applied based on the object state or Active Rule Set
  - Provides a ModelRulesInvoker to enable easy and simple integration with various UI stacks and their different requirements of validating input, a class, or class property.
 
- Input String Correction
  - Declarative rule attributes that decorate string properties
  - Rich set of provided case correction rules
  - Provides custom case correction rules and for application developers to add rules at run-time
  - Provides a ModelRulesInvoker to enable easy and simple integration with various UI stacks and their different requirements of validating input, a class, or class property.

- Audit
  - Declarative rules attributes that decorate properties
  - API to create either a string or dictionary that represents the state of the entity and property values, very useful in logging or populating exceptions with state data
  - Enables skipping properties, for example a Password property values should not be logged.
  
- Other
  - Feature Rich Sample Data Generator
  - Text encryption utility
  - BusinessEntityBase class used for XAML applications and .NET Library projects

### Oceanware.Ocean.Blazor
`Oceanware.Ocean.Blazor` is for Blazor UI projects.

Package: [Oceanware.Ocean.Blazor Validator](https://www.nuget.org/packages/Oceanware.Ocean.Blazor/)

Provides the `OceanValidator` that is the Blazor middleware between the Ocean validation and case correction library, and the Blazor UI. The `OceanValidator` replaces the `DataAnnotations` validation library validator.

Provides the `OceanNumericInput` component.

- Supports binding to Int32, Int32?, Int64, Int64?, Single, Single?, Double, Double?, Decimal, and Decimal? data types.
- Has a FormatString property.
- Prevents users from entering invalid numeric values and values with too many digits after the digits separator.
- String formatting of the entered values uses the culture-specific numeric separator, digits separator, and negative sign.
- Always uses CurrentCulture when performing string format operations.
- Allows entering and displaying a number separator. (the stock Blazor InputNumber component does not as it uses the HTML 5 input with type="number" that prevents a comma from being entered.
- Easily enable mobile browser numeric popup keyboard, set the BrowserInputMode property to Decimal or Numeric.
- Supports displaying the currency symbol in the input field if desired.  To enable, set the FormatString to `c`. 

Provides the `OceanAutoComplete` component.
- Enables a highly customizable auto complete experience.
- Search callback to return items.
- Selected item changed event when an item is selected.
- Change appearance of the search results container using a custom CSS class or modify the auto-complete-container CSS class.
- Change appearance of the selected search result item using a custom CSS class or modify the selected CSS class.
- Handles the arrow up and down, page up and down, escape, enter, and tab keys.
  - Arrow up and down move the selected item 1 item up or down.
  - Page up and down move the selected item 6 items up or down.
  - Escape closes the search results lists.
  - Enter and tab keys raises the selected item changed event and closes the search results lists.


#### Oceanware.Ocean.Blazor Requirements
.NET Core 3.0 RTM, and latest Visual Studio 2019.

## History
Back in 2008 I wrote a Code Project article about my initial WPF library for line-of-business applications. 
It was the foundation of Ocean. 

[April 2008 - Declarative Validation - Original Ocean Validation Library](https://www.codeproject.com/Articles/24823/WPF-Business-Application-Series-Part-3-of-n-Busine)

## Where Does the Name Ocean Come From?

[Read My About Page](https://oceanware.wordpress.com/about/)
