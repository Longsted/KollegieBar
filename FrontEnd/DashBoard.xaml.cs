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
    private readonly IServiceProvider _provider;

    public DashBoard(ProductBusinessLogicLayer productBusinessLogicLayer, IServiceProvider provider)
    {
        InitializeComponent();
        _provider = provider;
        _productBusinessLogicLayer = productBusinessLogicLayer;

        BindingContext = new DashboardViewModel(_productBusinessLogicLayer);
    }
}

public class DashboardViewModel : INotifyPropertyChanged
{
    public ObservableCollection<ProductDto> Products { get; set; }
    private readonly ProductBusinessLogicLayer _productBusinessLogicLayer;

    public DashboardViewModel(ProductBusinessLogicLayer productBusinessLogicLayer)
    {
        _productBusinessLogicLayer = productBusinessLogicLayer;
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

    private ProductDto _selectedProductDto;
    public ProductDto SelectedProductDto
    {
        get => _selectedProductDto;
        set
        {
            _selectedProductDto = value;
            Quantity = 0;
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

    private async void AddQuantity() 
    {
        if (SelectedProductDto == null) return; 

        await _productBusinessLogicLayer.RegisterIncomingStockAsync(
            SelectedProductDto.Id, Quantity);
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
}