using System;
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
        button.BackgroundColor = Color.FromArgb("#111114");   // dark background
        button.BorderColor = Color.FromArgb("#B84A84");       // pink corner
        button.BorderWidth = 1;
        button.TextColor = Color.FromArgb("#FFD3E6");         // light pink text
    }

    private void SetActive(Button button)
    {
        button.BackgroundColor = Color.FromArgb("#FF4FA3");   // Same as create button
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

                var snackDto = new SnackDataTransferObject(name, costPrice, stockQuantity);
                
                await _productBusinessLogicLayer.CreateProductAsync(snackDto);

                ResultLabel.Text = $"Snack created: {snackDto.Name}";
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

                DataTransferObject.Model.Pant pantEnum = DataTransferObject.Model.Pant.A;
                
                var liquidDto = new LiquidDataTransferObject(name, costPrice, stockQuantity, volumeCl, pantEnum, alcoholPercentage);
                
                await _productBusinessLogicLayer.CreateProductAsync(liquidDto);

                ResultLabel.Text = $"Liquid with alcohol created: {liquidDto.Name}";
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

                DataTransferObject.Model.Pant pantEnum = DataTransferObject.Model.Pant.A;
                

                var liquidDto = new LiquidDataTransferObject(name, costPrice, stockQuantity, volumeCl, pantEnum, sugarFree);
                
                await _productBusinessLogicLayer.CreateProductAsync(liquidDto);

                ResultLabel.Text = $"Liquid without alcohol created: {liquidDto.Name}";
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
                
                await _productBusinessLogicLayer.CreateProductAsync(consumableDataTransferObject);

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