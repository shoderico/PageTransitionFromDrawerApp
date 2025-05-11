using System.Windows.Input;
using System.Diagnostics;

namespace PageTransitionFromDrawerApp
{
    public partial class AppShell : Shell
    {
        // Navigation commands for MenuItems
        public ICommand NavigateToPage3Command { get; }
        public ICommand NavigateToPage4Command { get; }
        public ICommand NavigateToPage5Command { get; }
        public ICommand NavigateToPage6Command { get; }
        public ICommand NavigateToPage6WithParamCommand { get; }
        public ICommand NavigateToPage6WithQueryParamsCommand { get; }
        public ICommand NavigateToPage6WithConstructorCommand { get; }
        public ICommand NavigateToPage7Command { get; }

        public AppShell()
        {
            InitializeComponent();

            // Register dynamic route for Page5
            // Note: Routes registered with Routing.RegisterRoute cannot be used with absolute routing (//Page5)
            // See: https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/shell/navigation?view=net-maui-8.0#invalid-routes
            Routing.RegisterRoute(nameof(Page5), typeof(Page5));

            // Command for Page3: Attempts absolute routing (fails)
            NavigateToPage3Command = new Command(async () =>
            {
                try
                {
                    // Fails because Page3 is not in the visual hierarchy (no Route attribute in XAML)
                    // Error: "unable to figure out route for: //Page3"
                    await Shell.Current.GoToAsync($"//{nameof(Page3)}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Navigation Error (Page3): {ex.Message}");
                    await DisplayAlert("Navigation Error", $"Failed to navigate to Page3: {ex.Message}", "OK");
                }
            });

            // Command for Page4: Uses absolute routing (succeeds)
            NavigateToPage4Command = new Command(async () =>
            {
                // Succeeds because Page4 has an invisible FlyoutItem with Route="Page4" in the visual hierarchy
                await Shell.Current.GoToAsync($"//{nameof(Page4)}");
                Shell.Current.FlyoutIsPresented = false; // Close Flyout after navigation
            });

            // Command for Page5: Attempts absolute routing (fails)
            NavigateToPage5Command = new Command(async () =>
            {
                try
                {
                    // Fails because Page5 is registered dynamically (Routing.RegisterRoute)
                    // Absolute routing is not supported for dynamic routes
                    // Error: "Global routes currently cannot be the only page on the stack"
                    await Shell.Current.GoToAsync($"//{nameof(Page5)}");
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Navigation Error (Page5): {ex.Message}");
                    await DisplayAlert("Navigation Error", $"Failed to navigate to Page5: {ex.Message}", "OK");
                }
            });

            // Command for Page6: Uses absolute routing with query parameter
            NavigateToPage6Command = new Command(async () =>
            {
                // Succeeds because Page6 has an invisible FlyoutItem with Route="Page6"
                await Shell.Current.GoToAsync($"//{nameof(Page6)}?message=messageFromQueryString");
                Shell.Current.FlyoutIsPresented = false;
            });

            // Command for Page6 with CommandParameter
            NavigateToPage6WithParamCommand = new Command(async (param) =>
            {
                // Succeeds, passes parameter from XAML CommandParameter
                await Shell.Current.GoToAsync($"//{nameof(Page6)}?message={param}");
                Shell.Current.FlyoutIsPresented = false;
            });

            // Command for Page6 with ShellNavigationQueryParameters
            NavigateToPage6WithQueryParamsCommand = new Command(async () =>
            {
                // Succeeds, passes parameter via ShellNavigationQueryParameters
                await Shell.Current.GoToAsync($"//{nameof(Page6)}", new ShellNavigationQueryParameters
                {
                    { "message", "messageFromShellNavigationQueryParameters" }
                });
                Shell.Current.FlyoutIsPresented = false;
            });

            // Command for Page6 with constructor limitation
            NavigateToPage6WithConstructorCommand = new Command(async () =>
            {
                // Demonstrates limitation: Cannot pass page instance directly via GoToAsync
                await DisplayAlert("Navigation Limitation",
                    "Cannot pass a page instance to GoToAsync. Constructor argument approach is not supported from MenuItem in Flyout.\r\n\r\n"
                    + "await Shell.Current.GoToAsync(new Page6(\"messageFromConstructorArgument\");",
                    "OK");
                // await Shell.Current.GoToAsync(new Page6("messageFromConstructorArgument");
            });

            // Command for Page7: Passes complex object
            NavigateToPage7Command = new Command(async () =>
            {
                var userData = new UserData { Name = "shoderico", Age = 48 };
                // Succeeds, passes complex object via ShellNavigationQueryParameters
                await Shell.Current.GoToAsync($"//{nameof(Page7)}", new ShellNavigationQueryParameters
                {
                    { "userdata", userData }
                });
                Shell.Current.FlyoutIsPresented = false;
            });


            BindingContext = this;
        }
    }

}