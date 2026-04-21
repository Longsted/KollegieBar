using BusinessLogic.BusinessLogicLayer;
using Data.Model;
using DataTransferObject.Model;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;

namespace FrontEnd;

public partial class BarOverview : ContentPage
{

	private readonly ProductBusinessLogicLayer _IproductBusinessLogicLayer;

    public ObservableCollection<ProductDto> CurrentOrder { get; set; } = new();
    public BarOverview(ProductBusinessLogicLayer productBusinessLogicLayer)
	{
        InitializeComponent();
        ReceiptCollectionView.ItemsSource = CurrentOrder;

        _IproductBusinessLogicLayer = productBusinessLogicLayer;
        LoadProducts();

    }

    private async void LoadProducts()
    {
        try
        {
            
            var products = await _IproductBusinessLogicLayer.GetAllProductsAsync();

            ProductCollectionView.ItemsSource = products;
        }
        catch (Exception ex)
        {
            
            await DisplayAlert("Fail", "could not get the products " + ex.Message, "OK");
        }
    }


    private async void OnProductClicked(object sender, EventArgs e)
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
    private async void UpdateTotalSum()
    {
        decimal total = CurrentOrder.Sum(p => p.CostPrice);

        TotalSumLabel.Text = $"{total:N2} kr.";
    }


    private async void OnCheckoutClicked(object sender, EventArgs e)
    {

        if (CurrentOrder.Count == 0)
        {
            DisplayAlertAsync("Tom vogn", "Add something to the order before you checkout", "cancel");

        }
        else if (CurrentOrder.Count != 0)
        {

            var opsummering = CurrentOrder.GroupBy(p => p.Id).Select(g => new {
                Navn = g.First().Name,
                Antal = g.Count(),
                Total = g.Sum(x => x.CostPrice)
            }) .ToList();

            string tekst = ""; 
            for (int i = 0; opsummering.Count > i; i++)
            {
                tekst += opsummering[i].Navn + " x " + opsummering[i].Antal +"\n";
            }


            await DisplayAlertAsync("Ordre", tekst, "Cancel","OK");
        }

    }
}