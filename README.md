# MAUI Flyout Navigation Demo

This project demonstrates navigation in a .NET MAUI 8 application using a Flyout-based `AppShell`. It explores various navigation scenarios, including absolute and relative routing, query parameters, and common pitfalls, based on the [MAUI Shell Navigation documentation](https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/shell/navigation?view=net-maui-8.0).

The project highlights why absolute routing (`//PageX`) fails for dynamically registered routes and provides workarounds for passing parameters via Flyout `MenuItem` commands.

## Features
- Flyout menu with multiple navigation scenarios:
  - Navigation to pages with/without explicit `Route` attributes.
  - Absolute (`//PageX`) vs. relative (`PageX`) routing.
  - Passing query parameters and complex objects.
  - Handling navigation errors and limitations.
- Detailed comments explaining each navigation case.
- Sample pages (`Page1` to `Page7`) demonstrating different navigation patterns.

## Prerequisites
- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- Visual Studio 2022 (or later) with .NET MAUI workload
- MAUI project templates installed

## Setup
1. Clone the repository:
   ```bash
   git clone https://github.com/shoderico/maui-flyout-navigation-demo.git
   ```
2. Open the solution (`PageTransitionFromDrawerApp.sln`) in Visual Studio.
3. Restore NuGet packages.
4. Set the target platform (Windows, Android, or iOS) and run the project.

## Project Structure
- **`AppShell.xaml`**: Defines the Flyout menu and navigation structure using `FlyoutItem` and `MenuItem`.
- **`AppShell.xaml.cs`**: Implements navigation commands for `MenuItem` elements, including dynamic route registration and parameter passing.
- **`MainPage.xaml`, `Page1.xaml`–`Page7.xaml`**: Sample pages that receive and display navigation parameters.
- **`UserData.cs`**: A sample data model for passing complex objects.

## Navigation Scenarios
The Flyout menu includes the following navigation options, each demonstrating a specific case:

1. **Home (`MainPage`)**:
   - Defined as a `FlyoutItem` with implicit `Route="MainPage"`.
   - Accessible via relative (`MainPage`) or absolute (`//MainPage`) routing.

2. **Page1**:
   - `FlyoutItem` without an explicit `Route` attribute.
   - **Issue**: Absolute routing (`//Page1`) fails due to missing `Route` in the visual hierarchy.
   - **Solution**: Use relative routing (`Page1`).

3. **Page2**:
   - `FlyoutItem` with explicit `Route="Page2"`.
   - **Success**: Absolute routing (`//Page2`) works.
   - **Limitation**: Query parameters are not supported in this configuration.

4. **Page3**:
   - `MenuItem` with no `Route` attribute.
   - **Issue**: Absolute routing (`//Page3`) fails because `MenuItem` cannot define a `Route`.
   - **Solution**: Use relative routing or define an invisible `FlyoutItem`.

5. **Page4**:
   - `MenuItem` with an invisible `FlyoutItem` defining `Route="Page4"`.
   - **Success**: Absolute routing (`//Page4`) works due to the visual hierarchy.

6. **Page5**:
   - `MenuItem` with a dynamically registered route (`Routing.RegisterRoute`).
   - **Issue**: Absolute routing (`//Page5`) fails because dynamic routes are not in the visual hierarchy.
   - **Solution**: Use relative routing (`Page5`).
   - **Reference**: [Invalid Routes](https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/shell/navigation?view=net-maui-8.0#invalid-routes).

7. **Page6**:
   - `MenuItem` with an invisible `FlyoutItem` defining `Route="Page6"`.
   - **Success**: Absolute routing (`//Page6`) with query parameters (`?message=...`).
   - Variations:
     - **Page6 with Param**: Passes a parameter via `CommandParameter`.
     - **Page6 with Query Params**: Uses `ShellNavigationQueryParameters`.
     - **Page6 with Constructor**: Demonstrates the limitation of passing page instances.

8. **Page7**:
   - `MenuItem` with an invisible `FlyoutItem` defining `Route="Page7"`.
   - **Success**: Absolute routing (`//Page7`) with a complex object (`UserData`) via `ShellNavigationQueryParameters`.

## Key Learnings
- **Absolute Routing (`//PageX`)**:
  - Only works for routes defined in the Shell visual hierarchy (e.g., `FlyoutItem` or `ShellContent` with a `Route` attribute).
  - Fails for dynamically registered routes (`Routing.RegisterRoute`).
- **Relative Routing (`PageX`)**:
  - Works for both visual hierarchy routes and dynamically registered routes.
  - Preferred for `MenuItem`-based navigation.
- **Query Parameters**:
  - Supported via query strings (`?key=value`), `ShellNavigationQueryParameters`, or `CommandParameter`.
  - Pages must implement `IQueryAttributable` to receive parameters.
- **Limitations**:
  - `MenuItem` cannot define a `Route`, requiring workarounds like invisible `FlyoutItem` for absolute routing.
  - Constructor-based page instantiation is not supported in `GoToAsync`.

## Example Usage
1. Launch the app to see the Flyout menu.
2. Select menu items to navigate to different pages.
3. Observe navigation behavior:
   - **Page3** and **Page5** show error alerts for absolute routing failures.
   - **Page4**, **Page6**, and **Page7** demonstrate successful absolute routing with parameters.
   - **Page6 with Constructor** shows a limitation alert.

## Troubleshooting
- **Error: "unable to figure out route"**:
  - Ensure the target route is defined in the visual hierarchy for absolute routing.
  - Switch to relative routing for dynamically registered routes.
- **Error: "Global routes cannot be the only page on the stack"**:
  - Avoid absolute routing for routes registered with `Routing.RegisterRoute`.
- **Missing Parameters**:
  - Verify that the target page implements `IQueryAttributable` and handles query parameters correctly.

## Sample Page Implementation
Example for `Page6.xaml.cs`:
```csharp
public partial class Page6 : ContentPage, IQueryAttributable
{
    public Page6()
    {
        InitializeComponent();
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("message", out var message))
        {
            // Display the message (e.g., in a Label)
            Console.WriteLine($"Received message: {message}");
        }
    }
}
```

## References
- [MAUI Shell Navigation Documentation](https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/shell/navigation?view=net-maui-8.0)
- [MAUI Shell Navigation Invalid Routes](https://learn.microsoft.com/en-us/dotnet/maui/fundamentals/shell/navigation?view=net-maui-8.0#invalid-routes)
- [dotnet/maui GitHub Repository](https://github.com/dotnet/maui)

## Contributing
Feel free to submit issues or pull requests to improve this demo. Suggestions for additional navigation scenarios are welcome!

## License
This project is licensed under the MIT License.
