# Custom Themes

Create a resource dictionary containing all FlintUI brush keys.

```xml
<!-- Base -->
<SolidColorBrush x:Key="Background" Color="#FFF" />
<SolidColorBrush x:Key="ControlBackground" Color="#FFF" />
<SolidColorBrush x:Key="PanelBackground" Color="#FFF" />
<SolidColorBrush x:Key="Border" Color="#DDD" />
<SolidColorBrush x:Key="Border.Hover" Color="#AAA" />
<SolidColorBrush x:Key="Foreground" Color="#222" />
<SolidColorBrush x:Key="Foreground.Muted" Color="#888" />

<!-- Accent -->
<SolidColorBrush x:Key="Accent" Color="#06F" />
<SolidColorBrush x:Key="Accent.Hover" Color="#05D" />
<SolidColorBrush x:Key="Accent.Pressed" Color="#04B" />
<SolidColorBrush x:Key="Accent.Dim" Color="#1F06F" />
<SolidColorBrush x:Key="Accent.Subtle" Color="#F0F4FF" />

<!-- Success -->
<SolidColorBrush x:Key="Success" Color="#2C5" />
<SolidColorBrush x:Key="Success.Background" Color="#F0FDF4" />
<SolidColorBrush x:Key="Success.Border" Color="#BF7" />
<SolidColorBrush x:Key="Success.Foreground" Color="#165" />

<!-- Warning -->
<SolidColorBrush x:Key="Warning" Color="#F90" />
<SolidColorBrush x:Key="Warning.Background" Color="#FFFBEB" />
<SolidColorBrush x:Key="Warning.Border" Color="#FD6" />
<SolidColorBrush x:Key="Warning.Foreground" Color="#920" />

<!-- Danger -->
<SolidColorBrush x:Key="Danger" Color="#E44" />
<SolidColorBrush x:Key="Danger.Hover" Color="#D22" />
<SolidColorBrush x:Key="Danger.Background" Color="#FEF2F2" />
<SolidColorBrush x:Key="Danger.Border" Color="#FEC" />

<!-- Overlay -->
<SolidColorBrush x:Key="Overlay.Hover" Color="#0D000000" />
```

All keys are required. Missing keys do not throw exceptions, but controls may render incorrectly.

## Registration

Register the theme during application startup.

```csharp
ThemeProvider.Instance.Register(new ThemeDescriptor(
    new ThemeType("Blue"),
    "pack://application:,,,/MyApp;component/Themes/BlueBrushes.xaml"));
```

The resource dictionary must be referenced using an absolute pack URI.

## Applying a Theme

```csharp
ThemeService.Instance.SetTheme(new ThemeType("Blue"));
```

The theme must be registered before it can be applied.

## Scope

Themes are applied application-wide.