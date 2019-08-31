# Oceanware Ocean Library
Ocean is a cross-platform library that provides API's for WPF, Blazor, Xamarin, UWP, WebApi, Silverlight, and .NET Class Libraries.
Library has builds for .NET Framework, .NET Standard 2.0m and .NET Core 3.0.

Library does not require entities to implement any interfaces or based classes in order to be used in Blazor.
For XAML based applications, a rich base class is provided that integrates with XAML binding stacks.

## Build Status
|Ocean|[![Build Status](https://dev.azure.com/re-booting/Oceanware.Ocean/_apis/build/status/OceanLibrary.Ocean?branchName=master)](https://dev.azure.com/re-booting/Oceanware.Ocean/_build/latest?definitionId=2&branchName=master) |
|---|---|

## Documentation
[Ocean Library Documentation (Coming Soon!)]()

## NuGet Packages
### Oceanware.Ocean.Blazor
[Oceanware.Ocean.Blazor Validator](https://www.nuget.org/packages/Oceanware.Ocean.Blazor/1.0.0)

#### Requirements
.NET Core 3, Preview 8 and latest Visual Studio 2019 Preview

Provides the OceanValidator that is the Blazor middleware between the Ocean validation and case correction library and the Blazor UI.
This NuGet package provides the OceanValidator that is used on Blazor Razor pages. This replaces the default
DataAnnotations validation library validator.

### Oceanware.Ocean
[Oceanware.Ocean](https://www.nuget.org/packages/Oceanware.Ocean/1.0.0)

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
  - Provides custom case correction rules and for application developers to add rules at runtime
  - Provides a ModelRulesInvoker to enable easy and simple integration with various UI stacks and their different requirements of validating input, a class, or class property.

- Audit
  - Declarative rules attributes that decorate properties
  - API to create either a string or dictionary that represents the state of the entity and property values, very useful in logging or populating execptions with state data
  - Enables skipping properties, for example a Password property values should not be logged.
  
- Other
  - Feature Rich Sample Data Generator
  - Text encryption utility
  - BusinessEntityBase class used for XAML applications and .NET Library projects

## History
Back in 2008 I wrote a Code Project article about my initial WPF library for line-of-business applications. 
It was the foundation of Ocean. 

[April 2008 - Declarative Validation - Original Ocean Validation Library](https://www.codeproject.com/Articles/24823/WPF-Business-Application-Series-Part-3-of-n-Busine)

## Where Does the Name Ocean Come From?

[Read My About Page](https://oceanware.wordpress.com/about/)
