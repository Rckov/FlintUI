# FlintUI

[![.NET](https://img.shields.io/badge/.NET-Framework_4.8_%7C_6.0_%7C_8.0_%7C_10.0-512BD4)]()
[![UI](https://img.shields.io/badge/UI-WPF-2B579A)]()
[![Platform](https://img.shields.io/badge/platform-Windows-0078D6)]()
[![Build and Release](https://github.com/Rckov/FlintUI/actions/workflows/release.yml/badge.svg)](https://github.com/Rckov/FlintUI/actions/workflows/release.yml)
[![NuGet](https://img.shields.io/nuget/v/FlintUI.svg?label=NuGet)](https://www.nuget.org/packages/FlintUI)

A small WPF control library with a light theme, one accent color, and controls that share the same spacing and sizing. The repo also ships a gallery app that shows every control in one place.

![FlintUI gallery](docs/screenshots/gallery.png)

## What's inside

FlintUI is two things. First, a set of controls, some custom and some restyled from WPF. Second, a token system for color, spacing, radius, and type that keeps them looking like one family. You merge a single resource dictionary and the styles apply to both groups.

Custom controls: Window, Button, Badge, Icon, TextBox, PasswordBox, NumericUpDown, ToggleSwitch, TabControl, Spinner, Dialog.

Restyled WPF controls: CheckBox, RadioButton, ComboBox, Menu and ContextMenu, Expander, GroupBox, ListBox, TreeView, DataGrid, Slider, ProgressBar, ScrollBar, ToolTip, DatePicker.

## Requirements

The library targets `net48`, `net6.0-windows`, `net8.0-windows`, and `net10.0-windows`, so it runs on .NET Framework 4.8 and on modern .NET. WPF is Windows only, and so is FlintUI. The gallery app targets `net10.0-windows`.

## Getting started

Reference the library, then merge its resources in `App.xaml`:

```xml
<Application xmlns:ui="clr-namespace:FlintUI;assembly=FlintUI">
    <Application.Resources>
        <ResourceDictionary>
            <ResourceDictionary.MergedDictionaries>
                <ui:FlintResources />
            </ResourceDictionary.MergedDictionaries>
        </ResourceDictionary>
    </Application.Resources>
</Application>
```

The implicit styles now cover the built-in controls. For the custom ones, add the controls namespace and use them:

## The gallery

The `examples` project is a browsable demo, similar to the HandyControl sample app. Run it with:

```
dotnet run --project examples/FlintUI.Example.csproj
```

A list on the left switches pages. Each page groups related controls into cards. Screenshots below live in `docs/screenshots`.

### Buttons

Four styles (Default, Accent, Danger, Ghost), with and without icons, an icon-only variant, disabled states, and the badge set.

![Buttons page](docs/screenshots/buttons.png)

### Inputs

Text fields, password fields with a reveal toggle, numeric steppers, a combo box, and a date picker.

![Inputs page](docs/screenshots/inputs.png)

### Selection

Checkboxes, radio buttons, toggle switches, and sliders, including disabled examples.

![Selection page](docs/screenshots/selection.png)

### Data

A data grid bound to a small list, next to a list box and a tree.

![Data page](docs/screenshots/data.png)

### Containers

A closable tab control, expanders, a menu bar, and a right-click context menu.

![Containers page](docs/screenshots/containers.png)

### Feedback

Message dialogs, progress bars, spinners, and a tooltip.

![Feedback page](docs/screenshots/feedback.png)

### Icons

Every value of the `IconKind` enum, drawn from one template. The code reads the enum and binds each value to an icon, so new icons show up here on their own.

![Icons page](docs/screenshots/icons.png)