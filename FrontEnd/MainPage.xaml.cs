using BusinessLogic.BusinessLogicLayer;
using Data.Repositories;
using DataTransferObject.Model;

namespace FrontEnd;

public partial class MainPage : ContentPage
{

    private readonly UserRepository _userRepository;
    public MainPage()
    {
        InitializeComponent();
    }



    private async void LoginClicked(object sender, EventArgs e)
    {
        string password = PasswordEntry.Text;

        string UserName = UsernameEntry.Text;

        tjekUsers(sender, e);

        //if (UserName == "Admin" && password == "1234")
        //{
        //    // 1. Going to the DashBoard nicely
        //    await Navigation.PushAsync(new DashBoard());

        //    // 2. Removing the "LoginPage" from the stack
        //    Navigation.RemovePage(this);
        //} else  if (UserName == "Bar" && password == "1234"){
        //    await Navigation.PushAsync(new BarOverview());
    }

    private async void tjekUsers(object sender, EventArgs e)
    {
        string password = PasswordEntry.Text;
        string UserName = UsernameEntry.Text;

        var logic = new UserBusinessLogicLayer();
        var users = logic.GetUsers();

        var foundUser = users.FirstOrDefault(U => U.Password == password && U.UserName == UserName);

        if (foundUser.Role == UserRoles.BoardMember)
        {
            // 1. Going to the DashBoard nicely
            await Navigation.PushAsync(new DashBoard());

        }
        else if (foundUser.Role == UserRoles.Bartender)
        {
            await Navigation.PushAsync(new BarOverview());
        }
        // 2. Removing the "LoginPage" from the stack
        Navigation.RemovePage(this);

    }

}



//private void OnCounterClicked(object? sender, EventArgs e)
//{
//    count++;

//    if (count == 1)
//        CounterBtn.Text = $"Clicked {count} time";
//    else
//        CounterBtn.Text = $"Clicked {count} times";

//    SemanticScreenReader.Announce(CounterBtn.Text);
//}
