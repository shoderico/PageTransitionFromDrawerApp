namespace PageTransitionFromDrawerApp;

[QueryProperty(nameof(Message), "message")]
public partial class Page6 : ContentPage
{
    private string _message;
    public string Message
    {
        get => _message;
        set
        {
            _message = value;
            OnPropertyChanged();
        }
    }

    public Page6()
	{
		InitializeComponent();
        BindingContext = this;
	}

    public Page6(string message)
    {
        InitializeComponent();
        Message = message;
        BindingContext = this;
    }
}