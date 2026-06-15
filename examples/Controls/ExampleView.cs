using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using FlintUI.Controls;
using ICSharpCode.AvalonEdit;
using ICSharpCode.AvalonEdit.Editing;
using ICSharpCode.AvalonEdit.Highlighting;
using ICSharpCode.AvalonEdit.Rendering;
using Button = FlintUI.Controls.Button;

namespace FlintUI.Example.Controls;

[TemplatePart(Name = EditorPartName, Type = typeof(TextEditor))]
[TemplatePart(Name = CopyButtonPartName, Type = typeof(Button))]
public class ExampleView : ContentControl
{
    private const string EditorPartName = "PART_Editor";
    private const string CopyButtonPartName = "PART_CopyButton";

    public static readonly DependencyProperty CodeProperty =
        DependencyProperty.Register(
            nameof(Code),
            typeof(string),
            typeof(ExampleView),
            new PropertyMetadata(string.Empty, OnCodeChanged));

    private readonly DispatcherTimer _copyResetTimer = new()
    {
        Interval = TimeSpan.FromSeconds(1.5)
    };

    private Button? _copyButton;

    private TextEditor? _editor;

    static ExampleView()
    {
        DefaultStyleKeyProperty.OverrideMetadata(
            typeof(ExampleView),
            new FrameworkPropertyMetadata(typeof(ExampleView)));
    }

    public ExampleView()
    {
        _copyResetTimer.Tick += OnCopyResetTimerTick;
    }

    public string Code
    {
        get => (string)GetValue(CodeProperty);
        set => SetValue(CodeProperty, value);
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        _editor = GetTemplateChild(EditorPartName) as TextEditor;

        ApplyEditorTheme();
        UpdateEditor();

        AttachCopyButton();
    }

    private static void OnCodeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        ((ExampleView)d).UpdateEditor();
    }

    private void AttachCopyButton()
    {
        if (_copyButton is not null)
        {
            _copyButton.Click -= OnCopy;
        }

        _copyButton = GetTemplateChild(CopyButtonPartName) as Button;

        if (_copyButton is not null)
        {
            _copyButton.Click += OnCopy;
        }
    }

    private void UpdateEditor()
    {
        _editor?.Text = Code.Trim();
    }

    private void ApplyEditorTheme()
    {
        if (_editor is null)
        {
            return;
        }

        ConfigureSelection();
        HideLineNumberMargin();
        ApplySyntaxColors();
    }

    private void ConfigureSelection()
    {
        if (_editor is null)
        {
            return;
        }

        if (TryFindResource("Border.Hover") is Brush brush)
        {
            _editor.TextArea.SelectionBrush = brush;
        }

        _editor.TextArea.SelectionForeground = null;
        _editor.TextArea.SelectionBorder = null;
    }

    private void HideLineNumberMargin()
    {
        if (_editor == null)
        {
            return;
        }

        if (_editor.TextArea.LeftMargins.Count > 1)
        {
            _editor.TextArea.LeftMargins[1].Opacity = 0;
        }

        foreach (var leftMargin in _editor.TextArea.LeftMargins)
            if (leftMargin is LineNumberMargin numbers)
            {
                numbers.Margin = new Thickness(0, 0, 8, 0);
            }
    }

    private void ApplySyntaxColors()
    {
        if (_editor?.SyntaxHighlighting is not { } definition)
        {
            return;
        }

        SetHighlightColor(definition, "XmlTag", "Accent");
        SetHighlightColor(definition, "AttributeName", "Foreground.Muted");
        SetHighlightColor(definition, "AttributeValue", "Success.Foreground");
        SetHighlightColor(definition, "Comment", "Foreground.Muted");
    }

    private void SetHighlightColor(
        IHighlightingDefinition definition,
        string colorName,
        string resourceKey)
    {
        var color = definition.GetNamedColor(colorName);

        if (color is null)
        {
            return;
        }

        if (TryFindResource(resourceKey) is not Brush brush)
        {
            return;
        }

        color.Foreground = new ThemeBrush(brush);
    }

    private void OnCopy(object sender, RoutedEventArgs e)
    {
        try
        {
            Clipboard.SetText(Code.Trim());
        }
        catch
        {
            return;
        }

        if (_copyButton is null)
        {
            return;
        }

        _copyButton.Icon = IconKind.Check;
        _copyButton.Content = "Copied";

        _copyResetTimer.Stop();
        _copyResetTimer.Start();
    }

    private void OnCopyResetTimerTick(object? sender, EventArgs e)
    {
        _copyResetTimer.Stop();
        ResetCopyButton();
    }

    private void ResetCopyButton()
    {
        if (_copyButton is null)
        {
            return;
        }

        _copyButton.Icon = IconKind.Copy;
        _copyButton.Content = "Copy";
    }

    private sealed class ThemeBrush(Brush brush) : HighlightingBrush
    {
        public override Brush GetBrush(ITextRunConstructionContext context)
        {
            return brush;
        }
    }
}