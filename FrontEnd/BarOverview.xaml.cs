using BusinessLogic.BusinessLogicLayer;
using Data.Model;
using DataTransferObject.Model;
using System.Collections.ObjectModel;

namespace FrontEnd;

public partial class BarOverview : ContentPage
{

	private readonly ProductBusinessLogicLayer _productBusinessLogicLayer;

    public ObservableCollection<ProductDto> CurrentOrder { get; set; } = new();
    public BarOverview(ProductBusinessLogicLayer productBusinessLogicLayer)
	{
        InitializeComponent();
        ReceiptCollectionView.ItemsSource = CurrentOrder;

        _productBusinessLogicLayer = productBusinessLogicLayer;
        LoadProducts();

    }

    private async void LoadProducts()
    {
        try
        {
            
            var products = await _productBusinessLogicLayer.GetAllProductsAsync();

            ProductCollectionView.ItemsSource = products;
        }
        catch (Exception ex)
        {
            
            await DisplayAlert("Fail", "could not get the products " + ex.Message, "OK");
        }
    }


    private void OnProductClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var product = button?.BindingContext as ProductDto;

        if (product != null)
        {
            // Vi tilføjer til den globale "CurrentOrder" i stedet for en lokal liste
            CurrentOrder.Add(product);

            UpdateTotalSum();


            // Valgfrit: Console.WriteLine for at se det i din log
            Console.WriteLine($"Tilføjet: {product.Name}. Antal varer på bon: {CurrentOrder.Count}");
        }
    }
    private void UpdateTotalSum()
    {
        // Vi bruger LINQ til at lægge alle SalesPrice sammen
        decimal total = CurrentOrder.Sum(p => p.CostPrice);

        // Opdaterer teksten i vores Label
        TotalSumLabel.Text = $"{total:N2} kr.";
    }
}