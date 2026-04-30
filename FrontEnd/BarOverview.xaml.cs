using BusinessLogic.InterfaceBusiness;
using CommunityToolkit.Maui.Extensions;
using DataTransferObject.Model;
using System.Collections.ObjectModel;

namespace FrontEnd;

public partial class BarOverview : ContentPage
{

    private readonly IProductBusinessLogicLayer _productBusinessLogicLayer;

    private readonly ISalesBusinessLayer _salesBusinessLayer;

    private readonly IDrinksBusinessLogicLayer _drinksBusinessLogic;

    private DrinkDataTransferObject? _selectedDrink;

    public ObservableCollection<ICartItem> CurrentOrder { get; set; } = new();

    public List<ICartItem> EverythingThatsOnTheMenu { get; set; }


    //public List<ProductDataTransferObject> Snacks { get; set; } = new();

    private List<ProductDataTransferObject> _allProducts = new();

    private List<DrinkDataTransferObject> _allDrinks = new();


    public BarOverview(IProductBusinessLogicLayer productBusinessLogicLayer, ISalesBusinessLayer SalesBusinessLayer, IDrinksBusinessLogicLayer drinksBusiness)
    {
        InitializeComponent();
        ReceiptCollectionView.ItemsSource = CurrentOrder;


        _productBusinessLogicLayer = productBusinessLogicLayer;
        _salesBusinessLayer = SalesBusinessLayer;
        _drinksBusinessLogic = drinksBusiness;
        LoadProducts();

    }

    private async void LoadProducts()
    {
        try
        {
            var products = await _productBusinessLogicLayer.GetAllProductsAsync();

            var drinks = await _drinksBusinessLogic.GetAllDrinksAsync();

            _allProducts = products.ToList();

            _allDrinks = drinks.ToList();

            var alleVare = _allProducts.Cast<ICartItem>()
                           .Concat(_allDrinks.Cast<ICartItem>())
                           .ToList();
            EverythingThatsOnTheMenu = alleVare;

            ProductCollectionView.ItemsSource = EverythingThatsOnTheMenu;


        }
        catch (Exception ex)
        {

            await DisplayAlert("Fail", "could not get the products " + ex.Message, "OK");
        }
    }


    private async void OnProductClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var item = button?.BindingContext as ICartItem;

        if (item != null)
        {
            CurrentOrder.Add(item);

            if (item is DrinkDataTransferObject drink)
            {
                _selectedDrink = drink;
            }
        }
    }



    private async void OnCheckoutClicked(object sender, EventArgs e)
    {
        if (CurrentOrder.Count == 0)
        {
            await DisplayAlertAsync("Empty cart", "Add something to the order before you checkout", "cancel");
            return;
        }

        // Opsummeringen
        var opsummering = CurrentOrder.GroupBy(item => new { item.Id, Type = item.GetType() })
            .Select(g => new
            {
                Antal = g.Count(),
                Navn = g.First().Name
            }).ToList();

        string tekst = "";
        foreach (var linje in opsummering)
        {
            tekst += $"{linje.Antal} x {linje.Navn}\n";
        }


        var productIds = CurrentOrder
            .Where(item => item is ProductDataTransferObject)
            .Select(p => p.Id)
            .ToList();

        var drinkIds = CurrentOrder
            .Where(item => item is DrinkDataTransferObject)
            .Select(d => d.Id)
            .ToList();

        bool answer = await DisplayAlertAsync("Ordre", tekst, "Cancel", "OK");

        if (!answer)
        {
            try
            {
                await _salesBusinessLayer.RegisterSaleAsync(productIds, drinkIds);


                HelperMethodsToClearCartAndTotal();
                await DisplayAlert("Success", "Order registered successfully", "OK");
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to register order: {ex.Message}", "OK");
            }
        }
    }



    private async void ClearCart(object sender, EventArgs e)
    {
        HelperMethodsToClearCartAndTotal();
    }

    // This method registers the selected items as wasted instead of sold.
    private async void Waste(object sender, EventArgs e)
    {
        if (CurrentOrder.Count == 0)
        {
            await DisplayAlert("Empty", "No items to register as waste.", "OK");
            return;
        }

        bool confirm = await DisplayAlert("Register Waste", "Register waste for items on the receipt?", "Yes", "Cancel");
        if (!confirm)
            return;

        var productIds = CurrentOrder.Select(p => p.Id).ToList();

        try
        {
            await _productBusinessLogicLayer.RegisterWaste(productIds);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to register waste: {ex.Message}", "OK");
            return;
        }

        HelperMethodsToClearCartAndTotal();

        await DisplayAlert("Success", "Waste registered.", "OK");

        LoadProducts();
    }

    // This method is is so the bartender can remove a item on the currentOrderList 
    private async void OnReceiptSelectionChanged(object sender, SelectionChangedEventArgs e)
    {

        var selectedItem = e.CurrentSelection.FirstOrDefault() as ICartItem;

        if (selectedItem == null)
            return;


        bool answer = await DisplayAlert("Remove Product", $"Remove {selectedItem.Name}?", "No", "Yes");

        if (!answer)
        {
            CurrentOrder.Remove(selectedItem);
            // UpdateTotalSum(); 
        }
        ((CollectionView)sender).SelectedItem = null;
    }


    public async void ShowSnacks(object sender, EventArgs e)
    {

        // Vi filtrerer på om objekt-typen er SnackDataTransferObject
        var snacksOnly = _allProducts
            .Where(p => p is DataTransferObject.Model.SnackDataTransferObject)
            .ToList();

        ProductCollectionView.ItemsSource = snacksOnly;

    }

    private async void ShowBottles(object sender, EventArgs e)
    {
        List<LiquidDataTransferObject> bottlesOnly = new List<LiquidDataTransferObject>();
        foreach (var p in _allProducts)
        {
            if (p is LiquidDataTransferObject liquid)
            {
                if (liquid.AlcoholPercentage < 15.9 && liquid.VolumeCl < 49 && liquid.Name != "Tonic Water")
                {
                    bottlesOnly.Add(liquid);
                }
            }
        }
        ProductCollectionView.ItemsSource = bottlesOnly;
    }
    private async void ShowDrinks(object sender, EventArgs e)
    {
        var drnkOnly = _allDrinks
            .Where(p => p is DataTransferObject.Model.DrinkDataTransferObject)
            .ToList();

        ProductCollectionView.ItemsSource = drnkOnly;
    }
    private async void ShowMockTails(object sender, EventArgs e)
    {
        var drnkOnly = _allDrinks
            .Where(p => p is DataTransferObject.Model.DrinkDataTransferObject && !p.IsAlcoholic)
            .ToList();

        ProductCollectionView.ItemsSource = drnkOnly;
    }

    private async void ShowSpiritusNMixer(object sender, EventArgs e)
    {

        List<ProductDataTransferObject> spritiusNMixers = new List<ProductDataTransferObject>();
        foreach (var p in _allProducts)
        {
            if (p is LiquidDataTransferObject liquid)
            {
                if (liquid.AlcoholPercentage > 15.9 || !liquid.HasPant || liquid.Name == "Tonic Water")
                {
                    spritiusNMixers.Add(liquid);
                }

            }
        }
        ProductCollectionView.ItemsSource = spritiusNMixers;
    }

    private async void ShowAll(object sender, EventArgs e)
    {
        ProductCollectionView.ItemsSource = EverythingThatsOnTheMenu;
    }

    private async void ModifySelectedDrink(object sender, EventArgs e)
    {
        if (_selectedDrink == null)
        {
            await DisplayAlert("No drink selected", "The last item you added to cart was not a drink", "OK");
            return;
        }

        var modifyDrinkPopUp = new ModifyDrinkPopUp(_selectedDrink, _productBusinessLogicLayer, _drinksBusinessLogic);
        await Shell.Current.CurrentPage.ShowPopupAsync(modifyDrinkPopUp);

        if (modifyDrinkPopUp.IsSaved)
        {
            CurrentOrder.Remove(_selectedDrink);
            CurrentOrder.Add(modifyDrinkPopUp.NewDrink);
            await DisplayAlert("Success", "Drink modified successfully", "OK");
        }

        _selectedDrink = null;
    }




    //Helping methods that just clears th cart and setting the total to zero
    private async void HelperMethodsToClearCartAndTotal()
    {
        CurrentOrder.Clear();
    }

}