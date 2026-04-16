namespace FrontEnd;

public partial class DashBoard : ContentPage
{
	private readonly IServiceProvider _serviceProvider;
	
	public DashBoard(IServiceProvider serviceProvider)
	{
		InitializeComponent();
		_serviceProvider = serviceProvider;
	}

	private async void GoToCreateProductButton_OnClicked(object? sender, EventArgs e)
	{
		await Navigation.PushAsync(_serviceProvider.GetRequiredService<CreateProductPage>());
	}
	
}