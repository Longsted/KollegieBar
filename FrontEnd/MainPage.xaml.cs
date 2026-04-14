namespace FrontEnd;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }



    private async void LoginClicked(object sender, EventArgs e)
    {
        string password = PasswordEntry.Text;

        string UserName = UsernameEntry.Text;

        if (UserName == "Admin" && password == "1234")
        {
            // 1. Going to the DashBoard nicely
            await Navigation.PushAsync(new DashBoard());

            // 2. Removing the "LoginPage" from the stack
            Navigation.RemovePage(this);
        } else  if (UserName == "Bar" && password == "1234"){
            await Navigation.PushAsync(new BarOverview());
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
}