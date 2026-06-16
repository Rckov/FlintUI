# Window

`FlintUI.Controls.Window` is intended to be used through inheritance.

```csharp
public partial class MainWindow : FlintUI.Controls.Window
{
    public MainWindow()
    {
        InitializeComponent();
    }
}
```

```xml
<ct:Window x:Class="MyApp.MainWindow"
           xmlns:ct="clr-namespace:FlintUI.Controls;assembly=FlintUI">
    <!-- Content -->
</ct:Window>
```

## Title Bar Content

The title bar supports optional content areas and a subtitle.

| Property | Description |
|-----------|-------------|
| `Subtitle` | Additional text displayed next to the window title. |
| `LeftContent` | Content displayed on the left side of the title bar. |
| `RightContent` | Content displayed on the right side of the title bar. |

```xaml
<ct:Window x:Class="MyApp.MainWindow"
           Title="Settings"
           Subtitle="Project preferences"
           xmlns:ct="clr-namespace:FlintUI.Controls;assembly=FlintUI">

    <ct:Window.LeftContent>
        <ct:Button Icon="ArrowLeft"
                   ButtonKind="Ghost" />
    </ct:Window.LeftContent>

</ct:Window>
```

## WindowChrome Defaults

| Property | Value |
|-----------|-------|
| `CaptionHeight` | `40` |
| `ResizeBorderThickness` | `4` |
| `GlassFrameThickness` | `1` |
| `UseAeroCaptionButtons` | `false` |