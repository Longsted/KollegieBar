using Microsoft.Extensions.DependencyInjection;

namespace FrontEnd;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        // Here vi put MainPage in a NavigationPage.
        // So we can use Navigation.PushAsync() later.
        MainPage = new NavigationPage(new MainPage());
    }

    //protected override Window CreateWindow(IActivationState? activationState)
    //{
    //    return new Window(new AppShell());
    //}
}