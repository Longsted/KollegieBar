using BusinessLogic.BusinessLogicLayer;
using Data.Model;
using DataTransferObject.Model;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using BusinessLogic.InterfaceBusiness;
using System.Runtime.CompilerServices;

namespace FrontEnd;

public partial class BarOverview : ContentPage
{

	private readonly IProductBusinessLogicLayer _productBusinessLogicLayer;

    public ObservableCollection<ProductDataTransferObject> CurrentOrder { get; set; } = new();
    public BarOverview(IProductBusinessLogicLayer productBusinessLogicLayer)
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


    private async void OnProductClicked(object sender, EventArgs e)
    {
        var button = sender as Button;
        var product = button?.BindingContext as ProductDataTransferObject;

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
            List<int> productIds = CurrentOrder.Select(p => p.Id).ToList();

           // await _productBusinessLogicLayer.RegisterSaleAsync(productIds);


            bool answer = await DisplayAlertAsync("Ordre", tekst, "Cancel","OK");

            if (!answer)
            {
                HelperMethodsToClearCartAndTotal();
            }
        }

    }

    private async void ClearCart(object sender, EventArgs e)
    {
        HelperMethodsToClearCartAndTotal();
    }

    private async void Waste(object sender, EventArgs e)
    {

        // _productBusinessLogicLayer.RegisterWaste();

        HelperMethodsToClearCartAndTotal();

        DisplayAlertAsync("IKke så meget pis", "Det virker", "cáncel");
        
    }
    private async void OnReceiptSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        // 1. Hent det valgte objekt
        var selectedItem = e.CurrentSelection.FirstOrDefault() as ProductDataTransferObject;

        if (selectedItem == null)
            return;

        // 2. Gør noget (f.eks. spørg om varen skal fjernes)
        bool answer = await DisplayAlert("Remove Product", $"Remove {selectedItem.Name}?", "No", "Yes");

        if (!answer)
        {
            CurrentOrder.Remove(selectedItem);
            UpdateTotalSum(); 
        }
        ((CollectionView)sender).SelectedItem = null;
    }


    //Helping methods that just clears thw cart and setting the total to zero
    private async void HelperMethodsToClearCartAndTotal()
    {
        CurrentOrder.Clear();
        TotalSumLabel.Text = "";
    }

}