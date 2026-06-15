namespace FlintUI.Example.ViewModels;

public class DataGridViewModel
{
    public IReadOnlyList<Person> People { get; } =
    [
        new("Alice Johnson", "Admin", "alice@flint.dev"),
        new("Bob Smith", "Editor", "bob@flint.dev"),
        new("Carol White", "Viewer", "carol@flint.dev"),
        new("David Brown", "Editor", "david@flint.dev"),
        new("Erin Davis", "Viewer", "erin@flint.dev")
    ];
}

public class Person(string name, string role, string email)
{
    public string Name { get; } = name;
    public string Role { get; } = role;
    public string Email { get; } = email;
}