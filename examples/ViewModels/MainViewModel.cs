using CommunityToolkit.Mvvm.ComponentModel;

using System.ComponentModel.DataAnnotations;

namespace FlintUI.Example.ViewModels;

public partial class MainViewModel : ObservableValidator
{
	[Required(ErrorMessage = "Email is required")]
	[EmailAddress(ErrorMessage = "Invalid email")]
	public string? Email
	{
		get => field;
		set => SetProperty(ref field, value, validate: true);
	}

	public IReadOnlyList<Person> People { get; } =
	[
		new("Alice Johnson", "Admin", true),
		new("Bob Smith", "Editor", false),
		new("Carol White", "Viewer", true),
		new("David Brown", "Editor", true),
	];
}

public class Person(string name, string role, bool active)
{
	public string Name { get; set; } = name;
	public string Role { get; set; } = role;
	public bool Active { get; set; } = active;
}