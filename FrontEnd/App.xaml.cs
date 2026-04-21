using Microsoft.Extensions.DependencyInjection;

namespace FrontEnd;

public partial class App : Application
{
    public App(IServiceProvider iServiceProvider)
    {
        InitializeComponent();

        MainPage = iServiceProvider.GetRequiredService<MainPage>();
    }

    //protected override Window CreateWindow(IActivationState? activationState)
    //{
    //    return new Window(new AppShell());
    //}
}