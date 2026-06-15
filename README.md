[![.NET](https://img.shields.io/badge/.NET-4.8%20%7C%206%20%7C%208%20%7C%2010-512BD4)](https://dotnet.microsoft.com/)
[![Build and Release](https://github.com/Rckov/FlintUI/actions/workflows/release.yml/badge.svg)](https://github.com/Rckov/FlintUI/actions/workflows/release.yml)
[![NuGet](https://img.shields.io/nuget/v/FlintUI.svg?label=NuGet)](https://www.nuget.org/packages/FlintUI)

<img src="examples/Resources/icon.ico" width="45" height="45" align="left">

# FlintUI

A WPF control library.

## Getting started

```
dotnet add package FlintUI
```

Merge `FlintResources` into `App.xaml` and pick a theme:

```xml
<Application xmlns:ui="clr-namespace:FlintUI;assembly=FlintUI" ...>
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ui:FlintResources Theme="Light" />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```

## Demo

A gallery of every control. Grab the build from
[Releases](https://github.com/Rckov/FlintUI/releases/latest), or run it from source with
`dotnet run --project examples`.

## Screenshots

![](images/tab1.png)

![](images/tab2.png)

![](images/tab3.png)

---

[MIT License](LICENSE) · [Report an Issue](https://github.com/Rckov/FlintUI/issues)