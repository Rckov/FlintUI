using System.Windows;
using System.Windows.Controls;
using ICSharpCode.AvalonEdit;

namespace FlintUI.Example.Controls;

[TemplatePart(Name = "PART_Editor", Type = typeof(TextEditor))]
public class ExampleView : ContentControl
{
    public static readonly DependencyProperty CodeProperty =
        DependencyProperty.Register(nameof(Code), typeof(string), typeof(ExampleView), new PropertyMetadata(string.Empty, OnCodeChanged));

    private TextEditor? _editor;

    static ExampleView()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(ExampleView),
            new FrameworkPropertyMetadata(typeof(ExampleView)));
    }

    public string Code
    {
        get => (string)GetValue(CodeProperty);
        set => SetValue(CodeProperty, value);
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        _editor = GetTemplateChild("PART_Editor") as TextEditor;
        UpdateEditor();
    }

    private static void OnCodeChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs e)
    {
        ((ExampleView)dependencyObject).UpdateEditor();
    }

    private void UpdateEditor()
    {
        _editor?.Text = Code.Trim();
    }
}