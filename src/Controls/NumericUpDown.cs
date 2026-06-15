using System.Globalization;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace FlintUI.Controls;

[TemplatePart(Name = "PART_TextBox", Type = typeof(System.Windows.Controls.TextBox))]
[TemplatePart(Name = "PART_UpButton", Type = typeof(RepeatButton))]
[TemplatePart(Name = "PART_DownButton", Type = typeof(RepeatButton))]
public class NumericUpDown : Control
{
    public static readonly DependencyProperty ValueProperty =
        DependencyProperty.Register(nameof(Value), typeof(double), typeof(NumericUpDown),
            new FrameworkPropertyMetadata(0d, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault,
                (d, _) => ((NumericUpDown)d).UpdateText(),
                (d, v) => ((NumericUpDown)d).CoerceToRange((double)v)));

    public static readonly DependencyProperty MinimumProperty =
        DependencyProperty.Register(nameof(Minimum), typeof(double), typeof(NumericUpDown),
            new PropertyMetadata(0d, (d, _) => ((NumericUpDown)d).CoerceValue(ValueProperty)));

    public static readonly DependencyProperty MaximumProperty =
        DependencyProperty.Register(nameof(Maximum), typeof(double), typeof(NumericUpDown),
            new PropertyMetadata(100d, (d, _) => ((NumericUpDown)d).CoerceValue(ValueProperty)));

    public static readonly DependencyProperty StepProperty =
        DependencyProperty.Register(nameof(Step), typeof(double), typeof(NumericUpDown), new PropertyMetadata(1d));

    public static readonly DependencyProperty StringFormatProperty =
        DependencyProperty.Register(nameof(StringFormat), typeof(string), typeof(NumericUpDown),
            new PropertyMetadata("0", (d, _) => ((NumericUpDown)d).UpdateText()));

    private RepeatButton? _downButton;
    private System.Windows.Controls.TextBox? _textBox;
    private RepeatButton? _upButton;

    static NumericUpDown()
    {
        DefaultStyleKeyProperty.OverrideMetadata(typeof(NumericUpDown),
            new FrameworkPropertyMetadata(typeof(NumericUpDown)));
    }

    public double Value
    {
        get => (double)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public double Minimum
    {
        get => (double)GetValue(MinimumProperty);
        set => SetValue(MinimumProperty, value);
    }

    public double Maximum
    {
        get => (double)GetValue(MaximumProperty);
        set => SetValue(MaximumProperty, value);
    }

    public double Step
    {
        get => (double)GetValue(StepProperty);
        set => SetValue(StepProperty, value);
    }

    public string StringFormat
    {
        get => (string)GetValue(StringFormatProperty);
        set => SetValue(StringFormatProperty, value);
    }

    public override void OnApplyTemplate()
    {
        base.OnApplyTemplate();

        if (_textBox is not null)
        {
            _textBox.LostFocus -= OnTextBoxLostFocus;
            _textBox.PreviewKeyDown -= OnTextBoxKeyDown;
            _textBox.GotKeyboardFocus -= OnTextBoxGotFocus;
        }

        if (_upButton is not null)
        {
            _upButton.Click -= OnUpClick;
        }

        if (_downButton is not null)
        {
            _downButton.Click -= OnDownClick;
        }

        _textBox = GetTemplateChild("PART_TextBox") as System.Windows.Controls.TextBox;
        _upButton = GetTemplateChild("PART_UpButton") as RepeatButton;
        _downButton = GetTemplateChild("PART_DownButton") as RepeatButton;

        if (_textBox is not null)
        {
            _textBox.LostFocus += OnTextBoxLostFocus;
            _textBox.PreviewKeyDown += OnTextBoxKeyDown;
            _textBox.GotKeyboardFocus += OnTextBoxGotFocus;
        }

        if (_upButton is not null)
        {
            _upButton.Click += OnUpClick;
        }

        if (_downButton is not null)
        {
            _downButton.Click += OnDownClick;
        }

        UpdateText();
    }

    protected override void OnPreviewMouseWheel(MouseWheelEventArgs e)
    {
        if (!IsKeyboardFocusWithin)
        {
            return;
        }

        Spin(e.Delta > 0 ? Step : -Step);
        e.Handled = true;
    }

    private void Spin(double step)
    {
        CommitText();
        Value += step;
    }

    private double CoerceToRange(double value)
    {
        var max = Math.Max(Minimum, Maximum);
        if (value < Minimum)
        {
            return Minimum;
        }

        return value > max ? max : value;
    }

    private void UpdateText()
    {
        if (_textBox is null)
        {
            return;
        }

        _textBox.Text = string.IsNullOrEmpty(StringFormat)
            ? Value.ToString(CultureInfo.CurrentCulture)
            : Value.ToString(StringFormat, CultureInfo.CurrentCulture);
    }

    private void CommitText()
    {
        if (_textBox is null)
        {
            return;
        }

        if (double.TryParse(_textBox.Text, NumberStyles.Any, CultureInfo.CurrentCulture, out var parsed))
        {
            Value = parsed;
        }
        else
        {
            UpdateText();
        }
    }

    private void OnTextBoxLostFocus(object sender, RoutedEventArgs e)
    {
        CommitText();
    }

    private void OnTextBoxGotFocus(object sender, KeyboardFocusChangedEventArgs e)
    {
        ((System.Windows.Controls.TextBox)sender).SelectAll();
    }

    private void OnTextBoxKeyDown(object sender, KeyEventArgs e)
    {
        switch (e.Key)
        {
            case Key.Up:
                Spin(Step);
                e.Handled = true;
                break;
            case Key.Down:
                Spin(-Step);
                e.Handled = true;
                break;
            case Key.Enter:
                CommitText();
                e.Handled = true;
                break;
        }
    }

    private void OnUpClick(object sender, RoutedEventArgs e)
    {
        Spin(Step);
    }

    private void OnDownClick(object sender, RoutedEventArgs e)
    {
        Spin(-Step);
    }
}