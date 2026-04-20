using Microsoft.Extensions.DependencyInjection;

namespace FrontEnd;

public partial class App : Application
{
    public App(IServiceProvider serviceProvider)
    {
        InitializeComponent();

        MainPage = serviceProvider.GetRequiredService<MainPage>();
    }

    //protected override Window CreateWindow(IActivationState? activationState)
    //{
    //    return new Window(new AppShell());
    //}
}