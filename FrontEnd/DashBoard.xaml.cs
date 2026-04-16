using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using BusinessLogic.BusinessLogicLayer;
using DataTransferObject.Model;

namespace FrontEnd;

public partial class DashBoard : ContentPage
{
	private readonly ProductBusinessLogicLayer _productBusinessLogicLayer;
	
	public DashBoard(ProductBusinessLogicLayer productBusinessLogicLayer)
	{
		InitializeComponent();
		_productBusinessLogicLayer = productBusinessLogicLayer;
		BindingContext = new DashboardViewModel(_productBusinessLogicLayer);
		
	}
}

public class DashboardViewModel : INotifyPropertyChanged
{
	public ObservableCollection<Product> Products { get; set; }
	private readonly ProductBusinessLogicLayer _productBusinessLogicLayer;

	public DashboardViewModel(ProductBusinessLogicLayer productBusinessLogicLayer)
	{
		_productBusinessLogicLayer = productBusinessLogicLayer;
		// Products = new ObservableCollection<Product>(
		// _productBusinessLogicLayer.GetAllProducts());
	}

	private Product _selectedProduct;
	public Product SelectedProduct
	{
		get => _selectedProduct;
		set
		{
			_selectedProduct = value;
			Quantity = 0; // reset når ny vælges
			OnPropertyChanged();
		}
	}

	private int _quantity;
	public int Quantity
	{
		get => _quantity;
		set
		{
			_quantity = value;
			OnPropertyChanged();
		}
	}

	public ICommand AddQuantityCommand => new Command(AddQuantity);

	private void AddQuantity()
	{
		_productBusinessLogicLayer.RegisterIncomingStock(SelectedProduct.Id, Quantity);
	}


	public event PropertyChangedEventHandler PropertyChanged;
	protected void OnPropertyChanged([CallerMemberName] string name = null)
		=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
	
	
	// private async void GoToCreateProductButton_OnClicked(object? sender, EventArgs e)
	// {
	// 	await Navigation.PushAsync(new CreateProductPage());
	// }
