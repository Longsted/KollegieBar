namespace FrontEnd;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
    }



    private async void LoginClicked(object sender, EventArgs e)
    {
        string password = PasswordEntry.Text;

        string UserName = UsernameEntry.Text;





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