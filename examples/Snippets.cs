namespace FlintUI.Example;

public static class Snippets
{
    public const string Button =
        """
        <ct:Button Content="Default" />
        <ct:Button Content="Accent" ButtonKind="Accent" />
        <ct:Button Content="Save" Icon="Save" />
        <ct:Button Icon="Search" />
        """;

    public const string Badge =
        """
        <ct:Badge Content="Default" />
        <ct:Badge Content="Success" BadgeType="Success" />
        """;

    public const string Icon =
        """
        <ct:Icon Kind="Heart" Size="20" />
        """;

    public const string TextBox =
        """
        <ct:TextBox Placeholder="Search" IconKind="Search" />
        <ct:TextBox Text="Clear me" ClearButton="True" />
        """;

    public const string PasswordBox =
        """
        <ct:PasswordBox Placeholder="Password" />
        <ct:PasswordBox Placeholder="Password" RevealButton="False" />
        """;

    public const string NumericUpDown =
        """
        <ct:NumericUpDown Value="25" Minimum="0" Maximum="100" Step="5" />
        <ct:NumericUpDown Value="1.5" Step="0.1" StringFormat="0.0" />
        """;

    public const string ComboBox =
        """
        <ComboBox SelectedIndex="0">
            <ComboBoxItem Content="Low priority" />
            <ComboBoxItem Content="Medium priority" />
            <ComboBoxItem Content="High priority" />
        </ComboBox>
        """;

    public const string DatePicker =
        """
        <DatePicker />
        """;

    public const string Slider =
        """
        <Slider Minimum="0" Maximum="100" Value="60" />
        <Slider Minimum="0" Maximum="10" Value="4" TickFrequency="1" TickPlacement="BottomRight" IsSnapToTickEnabled="True" />
        """;

    public const string CheckBox =
        """
        <CheckBox Content="Email" IsChecked="True" />
        <CheckBox Content="Push" />
        """;

    public const string RadioButton =
        """
        <RadioButton Content="Free" GroupName="Plan" IsChecked="True" />
        <RadioButton Content="Pro" GroupName="Plan" />
        """;

    public const string ToggleSwitch =
        """
        <ct:ToggleSwitch Content="Wi-Fi" IsChecked="True" />
        <ct:ToggleSwitch Content="Bluetooth" />
        """;

    public const string DataGrid =
        """
        <DataGrid ItemsSource="{Binding People}" AutoGenerateColumns="False" IsReadOnly="True">
            <DataGrid.Columns>
                <DataGridTextColumn Header="Name" Binding="{Binding Name}" Width="*" />
                <DataGridTextColumn Header="Role" Binding="{Binding Role}" Width="Auto" />
                <DataGridTextColumn Header="Email" Binding="{Binding Email}" Width="2*" />
            </DataGrid.Columns>
        </DataGrid>
        """;

    public const string ListBox =
        """
        <ListBox>
            <ListBoxItem Content="Inbox" IsSelected="True" />
            <ListBoxItem Content="Starred" />
            <ListBoxItem Content="Sent" />
        </ListBox>
        """;

    public const string TreeView =
        """
        <TreeView>
            <TreeViewItem Header="Project" IsExpanded="True">
                <TreeViewItem Header="src" />
                <TreeViewItem Header="examples" />
            </TreeViewItem>
        </TreeView>
        """;

    public const string ProgressBar =
        """
        <ProgressBar Value="35" Height="6" />
        <ProgressBar IsIndeterminate="True" Height="6" />
        """;

    public const string TabControl =
        """
        <ct:TabControl CloseButton="True">
            <ct:TabItem Header="Overview" CanClose="False">
                <TextBlock Text="Pinned tab" />
            </ct:TabItem>
            <ct:TabItem Header="Activity">
                <TextBlock Text="Activity" />
            </ct:TabItem>
        </ct:TabControl>
        """;

    public const string Expander =
        """
        <Expander Header="Shipping address" IsExpanded="True">
            <TextBlock Text="1 Market St, San Francisco, CA" />
        </Expander>
        """;

    public const string GroupBox =
        """
        <GroupBox Header="Account">
            <CheckBox Content="Two-factor authentication" IsChecked="True" />
        </GroupBox>
        """;

    public const string Menu =
        """
        <Menu>
            <MenuItem Header="File">
                <MenuItem Header="New" />
                <Separator />
                <MenuItem Header="Exit" />
            </MenuItem>
        </Menu>
        """;

    public const string Dialog =
        """
        <ct:Button Content="Success" Command="{Binding SuccessCommand}" />

        Dialog.Show("Your changes have been saved.", "Saved", DialogButton.Ok, DialogIcon.Success);
        """;

    public const string Spinner =
        """
        <ct:Spinner Width="28" Height="28" />
        """;

    public const string ToolTip =
        """
        <ct:Button Content="Hover over me" ToolTip="A short, helpful hint" />
        """;
}