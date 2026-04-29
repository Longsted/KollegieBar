using System.Collections.ObjectModel;
using BusinessLogic.InterfaceBusiness;
using DataTransferObject.Model;

namespace FrontEnd;

public partial class CreateProductPage : ContentPage
{
    private string _selectedTab = "Snack";
    private readonly IProductBusinessLogicLayer _iProductBusinessLogicLayer;
    
    public ObservableCollection<LiquidDataTransferObject> AvailableIngredients { get; set; } = new();
    public ObservableCollection<LiquidDataTransferObject> PickedIngredients { get; set; } = new();

    public CreateProductPage(IProductBusinessLogicLayer iProductBusinessLogicLayer)
    {
        InitializeComponent();
        _iProductBusinessLogicLayer = iProductBusinessLogicLayer;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Sets the pant enum options from DTO onto the dropdown menu
        PantPicker.ItemsSource = Enum.GetValues(typeof(PantDataTransferObject)).Cast<PantDataTransferObject>().ToList();

        UpdateUI();
    }

    private void OnSnackSelected(object sender, EventArgs e)
    {
        _selectedTab = "Snack";
        UpdateUI();
    }

    private void OnLiquidSelected(object sender, EventArgs e)
    {
        _selectedTab = "Liquid";
        UpdateUI();
    }

    private void OnConsumableSelected(object sender, EventArgs e)
    {
        _selectedTab = "Consumable";
        UpdateUI();
    }

    private async void OnDrinkSelected(object sender, EventArgs e)
    {
        _selectedTab = "Drink";
        UpdateUI();
        
        // Get all liquids from data
        var allProducts = await _iProductBusinessLogicLayer.GetAllProductsAsync();
        var liquids = allProducts.OfType<LiquidDataTransferObject>().ToList();
        
        AvailableIngredients.Clear();
        foreach (var l in liquids) AvailableIngredients.Add(l);

        // Adding the different list to CollectionViews
        IngredientsCollectionView.ItemsSource = AvailableIngredients;
        PickedIngredientsCollectionView.ItemsSource = PickedIngredients;
    }

    private void UpdateUI()
    {
        bool isDrink = _selectedTab == "Drink";

        StandardProductFields.IsVisible = !isDrink;
        DrinkFieldsContainer.IsVisible = isDrink;

        SnackFieldsContainer.IsVisible = _selectedTab == "Snack";
        LiquidFieldsContainer.IsVisible = _selectedTab == "Liquid";
        ConsumableFieldsContainer.IsVisible = _selectedTab == "Consumable";

        CreateButton.Text = $"Create {_selectedTab}";

        // Refresh tab button appearance
        UpdateButtonStyles();
    }

    private void UpdateButtonStyles()
    {
        ResetButton(SnackTabButton);
        ResetButton(LiquidTabButton);
        ResetButton(ConsumableTabButton);
        ResetButton(DrinkTabButton);

        if (_selectedTab == "Snack")
            SetActive(SnackTabButton);
        else if (_selectedTab == "Liquid")
            SetActive(LiquidTabButton);
        else if (_selectedTab == "Consumable")
            SetActive(ConsumableTabButton);
        else if (_selectedTab == "Drink")
            SetActive(DrinkTabButton);
    }

    private void ResetButton(Button button)
    {
        button.BackgroundColor = Color.FromArgb("#111114");
        button.BorderColor = Color.FromArgb("#B84A84");
        button.BorderWidth = 1;
        button.TextColor = Color.FromArgb("#FFD3E6");
    }

    private void SetActive(Button button)
    {
        button.BackgroundColor = Color.FromArgb("#FF4FA3");
        button.BorderColor = Color.FromArgb("#FF4FA3");
        button.BorderWidth = 1.5;
        button.TextColor = Colors.White;
    }

    private void ClearFields()
    {
        NameEntry.Text = string.Empty;
        CostPriceEntry.Text = string.Empty;
        StockQuantityEntry.Text = string.Empty;
        DescriptionEntry.Text = string.Empty;

        LiquidVolumeClEntry.Text = string.Empty;
        AlcoholPercentageEntry.Text = string.Empty;
        IsAlcoholicCheckBox.IsChecked = false;
        SugarFreeCheckBox.IsChecked = false;
        PantPicker.SelectedItem = null;
        ResultLabel.Text = string.Empty;
        
        IsAlcoholicFreeCheckBox.IsChecked = false;
        DrinkDescriptionEntry.Text = string.Empty;
        PickedIngredients.Clear();
        PickedIngredientsCollectionView.SelectedItem = null;
        IngredientsCollectionView.SelectedItem = null;
    }

    private void OnIsAlcoholicChanged(object sender, CheckedChangedEventArgs e)
    {
        AlcoholInputContainer.IsVisible = e.Value;
        
        if (!e.Value)
        {
            AlcoholPercentageEntry.Text = string.Empty;
        }
    }

    private async void OnCreateProduct_Clicked(object sender, EventArgs e)
    {
        try
        {
            string? name = NameEntry.Text;
            if (string.IsNullOrWhiteSpace(name))
            {
                ResultLabel.Text = "Name must be filled.";
                return;
            }
            
            decimal costPrice = 0;
            int stockQuantity = 0;

            if (_selectedTab != "Drink")
            {
                if (!decimal.TryParse(CostPriceEntry.Text, out costPrice))
                {
                    ResultLabel.Text = "Cost price must be a valid number.";
                    return;
                }

                if (!int.TryParse(StockQuantityEntry.Text, out stockQuantity))
                {
                    ResultLabel.Text = "Stock quantity must be a valid whole number.";
                    return;
                }
            }
            
            if (_selectedTab == "Drink")
            {
                if (PickedIngredients.Count == 0)
                {
                    ResultLabel.Text = "Please pick at least one ingredient.";
                    return;
                }
                
                // DrinkDataTransferObject drink = new DrinkDataTransferObject(name, PickedIngredients.ToList() , IsAlcoholicFreeCheckBox.IsChecked);
                // await _iProductBusinessLogicLayer.CreateProductAsync(drink);
                ResultLabel.Text = $"Drink created: {name}";
            }

            if (_selectedTab == "Snack")
            {
                SnackDataTransferObject snackDataTransferObject = new SnackDataTransferObject(name, costPrice, stockQuantity);

                await _iProductBusinessLogicLayer.CreateProductAsync(snackDataTransferObject);
                ResultLabel.Text = $"Snack created: {snackDataTransferObject.Name}";
            }
            else if (_selectedTab == "Liquid")
            {
                if (!int.TryParse(LiquidVolumeClEntry.Text, out int volumeCl))
                {
                    ResultLabel.Text = "Please enter volume.";
                    return;
                }

                // Ensure pant is selected
                if (PantPicker.SelectedItem == null)
                {
                    ResultLabel.Text = "Please select a pant type.";
                    return;
                }

                var selectedPant = (PantDataTransferObject)PantPicker.SelectedItem;

                // Logic for optional alcohol percentage
                double alcohol = 0;
                if (IsAlcoholicCheckBox.IsChecked)
                {
                    double.TryParse(AlcoholPercentageEntry.Text, out alcohol);
                }

                bool sugarFree = SugarFreeCheckBox.IsChecked;

                // Create DTO using the overloads that include Pant
                LiquidDataTransferObject liquid;
                if (IsAlcoholicCheckBox.IsChecked)
                {
                    // use constructor with pant + alcohol
                    liquid = new LiquidDataTransferObject(name, costPrice, stockQuantity, volumeCl, selectedPant, alcohol)
                    {
                        SugarFree = sugarFree
                    };
                }
                else
                {
                    // use constructor with pant + sugarFree
                    liquid = new LiquidDataTransferObject(name, costPrice, stockQuantity, volumeCl, selectedPant, sugarFree);
                }

                await _iProductBusinessLogicLayer.CreateProductAsync(liquid);
                await DisplayAlert("Success", "Liquid product created!", "OK");
            }
            else if (_selectedTab == "Consumable")
            {
                ConsumablesDataTransferObject consumableDataTransferObject = new ConsumablesDataTransferObject
                {
                    Name = name,
                    CostPrice = costPrice,
                    StockQuantity = stockQuantity,
                    Description = DescriptionEntry.Text
                };

                await _iProductBusinessLogicLayer.CreateProductAsync(consumableDataTransferObject);
                ResultLabel.Text = $"Consumable created: {consumableDataTransferObject.Name}";
            }
            
            ClearFields();
            await DisplayAlert("Success", "Product has been created.", "OK");
        }
        catch (Exception ex)
        {
            ResultLabel.Text = $"Error: {ex.Message}";
        }
    }

    private void AddIngredient_OnClicked(object? sender, EventArgs e)
    {
        if (IngredientsCollectionView.SelectedItem is LiquidDataTransferObject selected)
        {
            PickedIngredients.Add(selected);
            IngredientsCollectionView.SelectedItem = null;
        }
    }

    private void RemoveIngredient_OnClicked(object? sender, EventArgs e)
    {
        if (PickedIngredientsCollectionView.SelectedItem is LiquidDataTransferObject selected)
        {
            PickedIngredients.Remove(selected);
            PickedIngredientsCollectionView.SelectedItem = null;
        }
    }
}