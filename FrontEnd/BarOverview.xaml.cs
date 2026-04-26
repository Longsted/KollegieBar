using BusinessLogic.BusinessLogicLayer;
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

    //public List<ProductDataTransferObject> Snacks { get; set; } = new();

    private List<ProductDataTransferObject> _allProducts = new();


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

            _allProducts = products.ToList();

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
            CurrentOrder.Add(product);
            UpdateTotalSum();
            //Console.WriteLine($"Tilføjet: {product.Name}. Antal varer på bon: {CurrentOrder.Count}");
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
            DisplayAlertAsync("Emty cart", "Add something to the order before you checkout", "cancel");

        }
        else if (CurrentOrder.Count != 0)
        {
            

            var opsummering = CurrentOrder.GroupBy(p => p.Id).Select(g => new {
                Antal = g.Count(),
                Navn = g.First().Name,
                Total = g.Sum(x => x.CostPrice)
            }) .ToList();

            

            string tekst = ""; 
            for (int i = 0; opsummering.Count > i; i++)
            {
                
                tekst += opsummering[i].Antal + " x " + opsummering[i].Navn +"\n";
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

    // This method is is so the bartender can remove a item on the currentOrderList 
    private async void OnReceiptSelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        
        var selectedItem = e.CurrentSelection.FirstOrDefault() as ProductDataTransferObject;

        if (selectedItem == null)
            return;

        
        bool answer = await DisplayAlert("Remove Product", $"Remove {selectedItem.Name}?", "No", "Yes");

        if (!answer)
        {
            CurrentOrder.Remove(selectedItem);
            UpdateTotalSum(); 
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

    private async void ShowBot(object sender, EventArgs e)
    {
        var bottlesOnly = _allProducts
            .Where(p => p is DataTransferObject.Model.LiquidDataTransferObject)
            .ToList();

        ProductCollectionView.ItemsSource = bottlesOnly;
    }
    private async void ShowDrinks(object sender, EventArgs e)
    {
        var drnkOnly = _allProducts
            .Where(p => p is DataTransferObject.Model.DrinkDataTransferObject)
            .ToList();

        ProductCollectionView.ItemsSource = drnkOnly;
    }




    //Helping methods that just clears th cart and setting the total to zero
    private async void HelperMethodsToClearCartAndTotal()
    {
        CurrentOrder.Clear();
        TotalSumLabel.Text = "";
    }

}