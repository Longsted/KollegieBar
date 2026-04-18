using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using BusinessLogic.BusinessLogicLayer;
using DataTransferObject.Model;

namespace FrontEnd;

public partial class DashBoard : ContentPage
{
	public DashBoard(ProductBusinessLogicLayer productBusinessLogicLayer, IServiceProvider provider)
	{
		InitializeComponent();
		BindingContext = new DashboardViewModel(productBusinessLogicLayer);
	}
}

public class DashboardViewModel : INotifyPropertyChanged
{
	private readonly ProductBusinessLogicLayer _productBusinessLogicLayer;
	// Dynamic list that updates the UI automatically when items change
	public ObservableCollection<Product> Products { get; set; }

	public DashboardViewModel(ProductBusinessLogicLayer logic)
	{
		_productBusinessLogicLayer = logic;
		// Load all products from the backend into the collection
		Products = new ObservableCollection<Product>(_productBusinessLogicLayer.GetAllProducts());
	}

	private Product _selectedProduct;
	public Product SelectedProduct
	{
		get => _selectedProduct;
		set 
		{ 
			_selectedProduct = value; 
			Quantity = null; // Reset input field when a new product is picked
			OnPropertyChanged(); // Notify UI that the selection changed
		}
	}

	private int? _quantity;
	public int? Quantity
	{
		get => _quantity;
		set 
		{ 
			_quantity = value; 
			OnPropertyChanged(); // Notify UI to update the entry field
		}
	}

	// Logic executed when the "Add quantity" button is clicked
	public ICommand AddQuantityCommand => new Command(async() => 
	{
		if (SelectedProduct == null)
		{
			await Shell.Current.DisplayAlert("Error", "Please select a product first", "OK");
			return;
		}
		
		if (Quantity == null || Quantity <= 0)
		{
			await Shell.Current.DisplayAlert("Error", "Please enter a quantity greater than 0", "OK");
			return;
		}
		
		_productBusinessLogicLayer.RegisterIncomingStock(SelectedProduct.Id, Quantity.Value);
		
		await Shell.Current.DisplayAlert("Success", $"{Quantity} items added to {SelectedProduct.Name}", "OK");
		
		Quantity = null;
	});

	// Boilerplate code to tell the UI when a property has been updated
	public event PropertyChangedEventHandler PropertyChanged;
	protected void OnPropertyChanged([CallerMemberName] string name = null)
		=> PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}
