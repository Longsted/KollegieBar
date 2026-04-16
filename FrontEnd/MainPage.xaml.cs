using BusinessLogic.BusinessLogicLayer;
using Data.Repositories;
using DataTransferObject.Model;

namespace FrontEnd;

public partial class MainPage : ContentPage
{
    private readonly UserBusinessLogicLayer _userBusinessLogicLayer;
    private readonly IServiceProvider _serviceProvider;

    public MainPage(UserBusinessLogicLayer userBusinessLogicLayer, IServiceProvider serviceProvider)
    {
        InitializeComponent();

        _userBusinessLogicLayer = userBusinessLogicLayer;
        _serviceProvider = serviceProvider;
    }
    

    private async void LoginClicked(object sender, EventArgs e)
    {
        await CheckUsers();
    }

    private async Task CheckUsers()
    {
        string password = PasswordEntry.Text;
        string UserName = UsernameEntry.Text;

        var users = _userBusinessLogicLayer.GetUsers();
        
        var foundUser = users.FirstOrDefault(U => U.Password == password && U.UserName == UserName);

        if (foundUser == null)
        {
            await DisplayAlert("Error", "Username or password is incorrect", "OK");
            return;
        }
        
        if (foundUser.Role == UserRoles.BoardMember)
        {
            // 1. Going to the DashBoard nicely
            var dashboard = _serviceProvider.GetRequiredService<DashBoard>();
            await Navigation.PushAsync(dashboard);
            Navigation.RemovePage(this);

        }
        else if (foundUser.Role == UserRoles.Bartender)
        {
            var barOverview = _serviceProvider.GetRequiredService<BarOverview>();
            await Navigation.PushAsync(barOverview);
            Navigation.RemovePage(this);
        }
        else
        {
            await DisplayAlert("Error", "Username or password is incorrect", "OK");
        }
    }

}

