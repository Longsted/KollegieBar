using BusinessLogic.BusinessLogicLayer;
using BusinessLogic.InterfaceBusiness;
using Data.Repositories;
using Data.UnitOfWork;
using DataTransferObject.Model;

namespace FrontEnd;

public partial class MainPage : ContentPage
{
    private readonly IUserBusinessLogicLayer _userBusinessLogicLayer;
    private readonly IServiceProvider _serviceProvider;

    public MainPage(IUserBusinessLogicLayer userBusinessLogicLayer, IServiceProvider serviceProvider)
    {
        InitializeComponent();

        _userBusinessLogicLayer = userBusinessLogicLayer;
        _serviceProvider = serviceProvider;
    }


    private async void LoginClicked(object sender, EventArgs e)
    {
        string password = PasswordEntry.Text;

        string userName = UsernameEntry.Text;

        int check = await _userBusinessLogicLayer.CheckUserAsync(password, userName);


        switch (check)
        {
            case 1: 
                Application.Current.MainPage = new AppShell(1); 
                break;
            case 2:
                Application.Current.MainPage = new AppShell(2);
                await Shell.Current.GoToAsync("//BarOverview");
                break;
                
            default:
                await DisplayAlert("Error", "Username or Password is incorrect", "OK");
                break;
        }

    }
}