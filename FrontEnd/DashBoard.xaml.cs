namespace FrontEnd;

public partial class DashBoard : ContentPage
{
	public DashBoard()
	{
		InitializeComponent();
	}

	private async void GoToCreateProductButton_OnClicked(object? sender, EventArgs e)
	{
		await Navigation.PushAsync(new CreateProductPage());
	}
}