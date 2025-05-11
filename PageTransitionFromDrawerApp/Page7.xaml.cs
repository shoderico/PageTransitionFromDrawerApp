namespace PageTransitionFromDrawerApp;

public class UserData
{
    public string Name { get; set; }
    public int Age { get; set; }
}

[QueryProperty(nameof(UserData), "userdata")]
public partial class Page7 : ContentPage
{
    private UserData _userData;

    public UserData UserData
    {
        get => _userData;
        set
        {
            _userData = value;
            OnPropertyChanged();
        }
    }

    public Page7()
	{
		InitializeComponent();
        BindingContext = this;

	}
}