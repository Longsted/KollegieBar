using BusinessLogic.InterfaceBusiness;
using CommunityToolkit.Maui.Extensions;
using DataTransferObject.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace FrontEnd;

public partial class DashBoard : ContentPage
{
    public DashBoard(IProductBusinessLogicLayer iProductBusinessLogicLayer)
    {
        InitializeComponent();
        BindingContext = new DashboardViewModel(iProductBusinessLogicLayer);
    }
}

public class DashboardViewModel : INotifyPropertyChanged
{
    private readonly IProductBusinessLogicLayer _iProductBusinessLogicLayer;

    // Backing store for all products (unfiltered)
    private List<object> _allProducts = new();

    public ObservableCollection<ProductDataTransferObject> Products { get; set; }

    // Filter / sort collections for the Pickers
    public ObservableCollection<string> Categories { get; } = new ObservableCollection<string>();
    public ObservableCollection<string> SortOptions { get; } = new ObservableCollection<string>();

    public DashboardViewModel(IProductBusinessLogicLayer logic)
    {
        _iProductBusinessLogicLayer = logic;
        Products = new ObservableCollection<ProductDataTransferObject>();

        // Setup categories (based on DTO classes)
        Categories.Add("All");
        Categories.Add("Snack");
        Categories.Add("Liquid");
        Categories.Add("Consumable"); // fallback group: ProductDataTransferObject but not Snack/Liquid

        // Setup sort options (only by StockQuantity or Name)
        SortOptions.Add("Quantity");
        SortOptions.Add("Name");

        SelectedCategory = "All";
        SelectedSortOption = "Quantity";

        LoadProducts();
    }

    private async void LoadProducts()
    {
        var products = await _iProductBusinessLogicLayer.GetAllProductsAsync();

        // Keep original list; GetAllProductsAsync returns ProductDataTransferObject items.
        // But there may be other DTOs in the system (e.g., DrinkDataTransferObject) — preserve as object.
        _allProducts = products.Cast<object>().ToList();

        ApplyFilterAndSort();
    }

    private ProductDataTransferObject _selectedProduct;
    public ProductDataTransferObject SelectedProduct
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

    // Category selection
    private string _selectedCategory;
    public string SelectedCategory
    {
        get => _selectedCategory;
        set
        {
            if (_selectedCategory == value) return;
            _selectedCategory = value;
            OnPropertyChanged();
            ApplyFilterAndSort();
        }
    }

    // Sort selection
    private string _selectedSortOption;
    public string SelectedSortOption
    {
        get => _selectedSortOption;
        set
        {
            if (_selectedSortOption == value) return;
            _selectedSortOption = value;
            OnPropertyChanged();
            ApplyFilterAndSort();
        }
    }

    // Add quantity command (unchanged)
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

        await _iProductBusinessLogicLayer.RegisterIncomingStockAsync(SelectedProduct.Id, Quantity.Value);

        await Shell.Current.DisplayAlert("Success", $"{Quantity} items added to {SelectedProduct.Name}", "OK");

        Quantity = null;

        await RefreshProductsFromServer();
    });

    private async System.Threading.Tasks.Task RefreshProductsFromServer()
    {
        var products = await _iProductBusinessLogicLayer.GetAllProductsAsync();
        _allProducts = products.Cast<object>().ToList();
        ApplyFilterAndSort();
    }

    // Combined filter + sort
    private void ApplyFilterAndSort()
    {
        if (_allProducts == null) return;

        IEnumerable<object> query = _allProducts;

        // Category filter based on actual class
        if (!string.IsNullOrEmpty(SelectedCategory) && SelectedCategory != "All")
        {
            switch (SelectedCategory)
            {
                case "Snack":
                    query = query.Where(p => p is SnackDataTransferObject);
                    break;
                case "Drink":
                    query = query.Where(p => p is DrinkDataTransferObject);
                    break;
                case "Liquid":
                    query = query.Where(p => p is LiquidDataTransferObject);
                    break;
                case "Consumable":
                    // Consumable: any ProductDataTransferObject that is not Snack or Liquid
                    query = query.Where(p =>
                        p is ProductDataTransferObject pd &&
                        !(pd is SnackDataTransferObject) &&
                        !(pd is LiquidDataTransferObject));
                    break;
                default:
                    break;
            }
        }

        // Map to ProductDataTransferObject where possible
        var mapped = query.Select(p => p as ProductDataTransferObject).ToList();

        // Sort
        if (!string.IsNullOrEmpty(SelectedSortOption))
        {
            switch (SelectedSortOption)
            {
                case "Quantity":
                    // Use StockQuantity from ProductDataTransferObject where available; missing -> 0
                    mapped = mapped.OrderByDescending(p => p?.StockQuantity ?? 0).ToList();
                    break;
                case "Name":
                    mapped = mapped.OrderBy(p => p?.Name ?? string.Empty).ToList();
                    break;
                default:
                    break;
            }
        }

        // Update observable collection
        Products.Clear();
        foreach (var p in mapped)
        {
            if (p != null)
                Products.Add(p);
        }
    }

    public event PropertyChangedEventHandler PropertyChanged;
    protected void OnPropertyChanged([CallerMemberName] string name = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

    public ICommand EditProductCommand => new Command(async () =>
    {
        if (SelectedProduct == null)
        {
            await Shell.Current.DisplayAlertAsync("Error", "Please select a product first", "OK");
            return;
        }

        var product = SelectedProduct;
        var popup = new EditProductPopup(product);
        await Shell.Current.CurrentPage.ShowPopupAsync(popup);

        if (popup.IsSaved)
        {
            await _iProductBusinessLogicLayer.UpdateProductAsync(product);
        }
    });

}
