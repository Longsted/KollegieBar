using BusinessLogic.BusinessLogicLayer;
using BusinessLogic.InterfaceBusiness;
using Data.Repositories;
using Data.UnitOfWork;
using DataTransferObject.Model;

namespace FrontEnd;

public partial class MainPage : ContentPage
{
    private readonly IUserBusinessLogicLayer _iUserBusinessLogicLayer;

    public MainPage(IUserBusinessLogicLayer iUserBusinessLogicLayer)
    {
        InitializeComponent();

        _iUserBusinessLogicLayer = iUserBusinessLogicLayer;
    }

    private async void LoginClicked(object sender, EventArgs e)
    {
        string password = PasswordEntry.Text;

        string userName = UsernameEntry.Text;

        int check = await _iUserBusinessLogicLayer.CheckUserAsync(password, userName);
        if (1 == check)
        {
            // 1. Going to the DashBoard nicely
            Application.Current.MainPage = new AppShell(1);
        }
        else if (2 == check)
        {
            Application.Current.MainPage = new AppShell(2);
            await Shell.Current.GoToAsync("//BarOverview");
        }
        else if (0 == check)
        {
            await DisplayAlert("Error", "Username or Password is incorrect", "OK");
            return;
        }

    }
}