namespace FlintUI.Example.Views;

public partial class DataPage
{
    public DataPage()
    {
        InitializeComponent();
        DataContext = this;
    }

    public IReadOnlyList<Person> People { get; } =
    [
        new("Alice Johnson", "Admin", "alice@flint.dev"),
        new("Bob Smith", "Editor", "bob@flint.dev"),
        new("Carol White", "Viewer", "carol@flint.dev"),
        new("David Brown", "Editor", "david@flint.dev"),
        new("Erin Davis", "Viewer", "erin@flint.dev")
    ];

    public record Person(string Name, string Role, string Email);
}
