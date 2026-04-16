using BusinessLogic.BusinessLogicLayer;
using DataTransferObject.Model;

namespace FrontEnd;

public partial class CreateProductPage : ContentPage
{
    private string _selectedTab = "Snack";
    private readonly ProductBusinessLogicLayer _productBusinessLogicLayer;
    
    public CreateProductPage(ProductBusinessLogicLayer productBusinessLogicLayer)
    {
        InitializeComponent();
        
        _productBusinessLogicLayer = productBusinessLogicLayer;
        
        UpdateUI();
        
    }

    private void OnSnackSelected(object sender, EventArgs e)
    {
        _selectedTab = "Snack";
        UpdateUI();
    }

    private void OnLiquidWithPctSelected(object sender, EventArgs e)
    {
        _selectedTab = "LiquidWithPct";
        UpdateUI();
    }

    private void OnLiquidWithoutPctSelected(object sender, EventArgs e)
    {
        _selectedTab = "LiquidWithoutPct";
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
        LiquidWithPctFieldsContainer.IsVisible = _selectedTab == "LiquidWithPct";
        LiquidWithoutPctFieldsContainer.IsVisible = _selectedTab == "LiquidWithoutPct";
        ConsumableFieldsContainer.IsVisible = _selectedTab == "Consumable";

        if (_selectedTab == "Snack")
            CreateButton.Text = "Create Snack";
        else if (_selectedTab == "LiquidWithPct")
            CreateButton.Text = "Create Liquid with %";
        else if (_selectedTab == "LiquidWithoutPct")
            CreateButton.Text = "Create Liquid without %";
        else if (_selectedTab == "Consumable")
            CreateButton.Text = "Create Consumable";

        UpdateButtonStyles();
    }

    private void UpdateButtonStyles()
    {
        ResetButton(SnackTabButton);
        ResetButton(LiquidWithPctTabButton);
        ResetButton(LiquidWithoutPctTabButton);
        ResetButton(ConsumableTabButton);

        if (_selectedTab == "Snack")
            SetActive(SnackTabButton);
        else if (_selectedTab == "LiquidWithPct")
            SetActive(LiquidWithPctTabButton);
        else if (_selectedTab == "LiquidWithoutPct")
            SetActive(LiquidWithoutPctTabButton);
        else if (_selectedTab == "Consumable")
            SetActive(ConsumableTabButton);
    }

    private void ResetButton(Button button)
    {
        button.BackgroundColor = Color.FromArgb("#111114");   // mørk baggrund
        button.BorderColor = Color.FromArgb("#B84A84");       // pink kant
        button.BorderWidth = 1;
        button.TextColor = Color.FromArgb("#FFD3E6");         // lys pink tekst
    }

    private void SetActive(Button button)
    {
        button.BackgroundColor = Color.FromArgb("#FF4FA3");   // SAMME SOM CREATE KNAP
        button.BorderColor = Color.FromArgb("#FF4FA3");
        button.BorderWidth = 1.5;
        button.TextColor = Colors.White;
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
                if (!decimal.TryParse(SnackSalesPriceEntry.Text, out decimal salesPrice))
                {
                    ResultLabel.Text = "Sales price must be a valid number.";
                    return;
                }

                Snack snack = new Snack(name, costPrice, stockQuantity, salesPrice);
                
                ResultLabel.Text = $"Snack created: {snack.Name}";
            }
            else if (_selectedTab == "LiquidWithPct")
            {
                if (!int.TryParse(LiquidWithPctVolumeClEntry.Text, out int volumeCl))
                {
                    ResultLabel.Text = "Volume must be a valid whole number.";
                    return;
                }

                if (!decimal.TryParse(LiquidWithPctSalesPriceEntry.Text, out decimal salesPrice))
                {
                    ResultLabel.Text = "Sales price must be a valid number.";
                    return;
                }

                if (!double.TryParse(AlcoholPercentageEntry.Text, out double alcoholPercentage))
                {
                    ResultLabel.Text = "Alcohol percentage must be a valid number.";
                    return;
                }

                LiquidWithAlcohol liquid = new LiquidWithAlcohol(
                    name,
                    costPrice,
                    stockQuantity,
                    volumeCl,
                    salesPrice,
                    alcoholPercentage
                );

                ResultLabel.Text = $"Liquid with alcohol created: {liquid.Name}";
            }
            else if (_selectedTab == "LiquidWithoutPct")
            {
                if (!int.TryParse(LiquidWithoutPctVolumeClEntry.Text, out int volumeCl))
                {
                    ResultLabel.Text = "Volume must be a valid whole number.";
                    return;
                }

                if (!decimal.TryParse(LiquidWithoutPctSalesPriceEntry.Text, out decimal salesPrice))
                {
                    ResultLabel.Text = "Sales price must be a valid number.";
                    return;
                }

                bool sugarFree = SugarFreeCheckBox.IsChecked;

                LiquidWithoutAlcohol liquid = new LiquidWithoutAlcohol(
                    name,
                    costPrice,
                    stockQuantity,
                    volumeCl,
                    salesPrice,
                    sugarFree
                );
                
                _productBusinessLogicLayer.AddProduct(liquid);

                ResultLabel.Text = $"Liquid without alcohol created: {liquid.Name}";
            }
            else if (_selectedTab == "Consumable")
            {
                Consumables consumable = new Consumables
                {
                    Name = name,
                    CostPrice = costPrice,
                    StockQuantity = stockQuantity,
                    Description = DescriptionEntry.Text
                };
                
                _productBusinessLogicLayer.AddProduct(consumable);

                ResultLabel.Text = $"Consumable created: {consumable.Name}";
            }

            await DisplayAlert("Success", "Product has been created.", "OK");
        }
        catch (Exception ex)
        {
            ResultLabel.Text = $"Error: {ex.Message}";
        }
    }
}