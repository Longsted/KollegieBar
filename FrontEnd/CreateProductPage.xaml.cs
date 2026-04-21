using BusinessLogic.BusinessLogicLayer;
using BusinessLogic.InterfaceBusiness;
using DataTransferObject.Model;

namespace FrontEnd;

public partial class CreateProductPage : ContentPage
{
    private string _selectedTab = "Snack";
    private readonly IProductBusinessLogicLayer _iProductBusinessLogicLayer;

    public CreateProductPage(IProductBusinessLogicLayer iProductBusinessLogicLayer)
    {
        InitializeComponent();
        _iProductBusinessLogicLayer = iProductBusinessLogicLayer;
    }
    
    protected override void OnAppearing()
    {
        base.OnAppearing();
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

    private void UpdateUI()
    {
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

        if (_selectedTab == "Snack")
            SetActive(SnackTabButton);
        else if (_selectedTab == "Liquid")
            SetActive(LiquidTabButton);
        else if (_selectedTab == "Consumable")
            SetActive(ConsumableTabButton);
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
        ResultLabel.Text = string.Empty;
    }
    
    private void OnIsAlcoholicChanged(object sender, CheckedChangedEventArgs e)
    {
        // Shows/hides the entire alcohol input section
        AlcoholInputContainer.IsVisible = e.Value;

        // Optional: Clear the text if user unchecks it
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

            if (!decimal.TryParse(CostPriceEntry.Text, out decimal costPrice))
            {
                ResultLabel.Text = "Cost price must be a valid number.";
                return;
            }

            if (!int.TryParse(StockQuantityEntry.Text, out int stockQuantity))
            {
                ResultLabel.Text = "Stock quantity must be a valid whole number.";
                return;
            }

            if (_selectedTab == "Snack")
            {
                SnackDataTransferObject snackDataTransferObject =
                    new SnackDataTransferObject(name, costPrice, stockQuantity);

                await _iProductBusinessLogicLayer.CreateProductAsync(snackDataTransferObject);
                ClearFields();
                ResultLabel.Text = $"Snack created: {snackDataTransferObject.Name}";
            }
            if (_selectedTab == "Liquid")
            {
                if (!int.TryParse(LiquidVolumeClEntry.Text, out int volumeCl))
                {
                    ResultLabel.Text = "Please enter volume.";
                    return;
                }

                // Logic for optional alcohol percentage
                double alcohol = 0;
                if (IsAlcoholicCheckBox.IsChecked)
                {
                    double.TryParse(AlcoholPercentageEntry.Text, out alcohol);
                }

                bool sugarFree = SugarFreeCheckBox.IsChecked;

                // Use your constructor - passing 0 if it's a soda
                var liquid = new LiquidDataTransferObject(name, costPrice, stockQuantity, volumeCl, alcohol)
                {
                    SugarFree = sugarFree
                };

                await _iProductBusinessLogicLayer.CreateProductAsync(liquid);
                ClearFields();
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
                ClearFields();
                ResultLabel.Text = $"Consumable created: {consumableDataTransferObject.Name}";
            }

            await DisplayAlert("Success", "Product has been created.", "OK");
        }
        catch (Exception ex)
        {
            ResultLabel.Text = $"Error: {ex.Message}";
        }
    }
}