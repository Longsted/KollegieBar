using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

using BusinessLogic.InterfaceBusiness;
using DataTransferObject.Model;

namespace FrontEnd;

public partial class DashBoard : ContentPage
{
    public DashBoard(IProductBusinessLogicLayer IproductBusinessLogicLayer, IServiceProvider provider)
    {
        InitializeComponent();
        BindingContext = new DashboardViewModel(IproductBusinessLogicLayer);
    }
}

public class DashboardViewModel : INotifyPropertyChanged
{
    private readonly IProductBusinessLogicLayer _productBusinessLogicLayer;

    public ObservableCollection<ProductDto> Products { get; set; }

    public DashboardViewModel(IProductBusinessLogicLayer logic)
    {
        _productBusinessLogicLayer = logic;
        Products = new ObservableCollection<ProductDto>();

        LoadProducts();
    }

    private async void LoadProducts()
    {
        var products = await _productBusinessLogicLayer.GetAllProductsAsync();

        foreach (var product in products)
        {
            Products.Add(product);
        }
    }

    private ProductDto _selectedProduct;
    public ProductDto SelectedProduct
    {
        get => _selectedProduct;
        set
        {
            _selectedProduct = value;
            Quantity = null;
            OnPropertyChanged();
        }
    }

    private int? _quantity;
    public int? Quantity
    {
        get => _quantity;
        set
        {
            _quantity = value;
            OnPropertyChanged();
        }
    }

    public ICommand AddQuantityCommand => new Command(async () =>
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

        await _productBusinessLogicLayer.RegisterIncomingStockAsync(
            SelectedProduct.Id, Quantity.Value);

        await Shell.Current.DisplayAlert("Success", $"{Quantity} items added to {SelectedProduct.Name}", "OK");

        Quantity = null;
    });

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}