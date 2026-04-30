using BusinessLogic.InterfaceBusiness;
using DataTransferObject.Model;
using System.Collections.ObjectModel;

namespace FrontEnd;

public partial class ModifyDrinkPopUp : CommunityToolkit.Maui.Views.Popup
{
    private DrinkDataTransferObject _drink;

    public bool IsSaved { get; private set; } = false;

    public DrinkDataTransferObject? NewDrink { get; private set; }

    private readonly IProductBusinessLogicLayer _productBusinessLogicLayer;
    private readonly IDrinksBusinessLogicLayer _drinksBusinessLogicLayer;

    public ObservableCollection<LiquidDataTransferObject> AllLiquids { get; set; } = new();
    public ObservableCollection<LiquidDataTransferObject> PickedLiquids { get; set; } = new();

    // Egenskaber til at huske, hvad brugeren har klikket på i listerne
    public LiquidDataTransferObject? SelectedAvailableLiquid { get; set; }
    public LiquidDataTransferObject? SelectedPickedLiquid { get; set; }

    public ObservableCollection<LiquidDataTransferObject> AvailableLiquids { get; set; } = new();

    // Constructor (no async/Task return)
    public ModifyDrinkPopUp(DrinkDataTransferObject drink, IProductBusinessLogicLayer productBusinessLogicLayer
        , IDrinksBusinessLogicLayer drinksBusinessLogicLayer)
    {
        InitializeComponent();
        BindingContext = this;

        _drink = drink;
        _productBusinessLogicLayer = productBusinessLogicLayer;
        _drinksBusinessLogicLayer = drinksBusinessLogicLayer;

        // Start async initialization without awaiting in constructor
        _ = LoadAvailableLiquids();
    }

    private async Task LoadAvailableLiquids()
    {
        foreach (var liquid in _drink.Ingredients)
        {
            PickedLiquids.Add(liquid);
        }

        var liquids = await _productBusinessLogicLayer.GetAllLiquidsAsync();
        AvailableLiquids.Clear();
        foreach (var liquid in liquids)
        {
            AllLiquids.Add(liquid);
        }
    }

    private async void OnSaveButtonClicked(object sender, EventArgs e)
    {
        if (PickedLiquids.Count == 0)
        {
            await Shell.Current.DisplayAlertAsync("Error", "You must pick at least one ingredient.", "OK");
            return;
        }
        var newDrink = new DrinkDataTransferObject
        {
            Name = _drink.Name,
            IsAlcoholic = _drink.IsAlcoholic,
            Ingredients = PickedLiquids.ToList(),
            Description = _drink.Description
        };
        await _drinksBusinessLogicLayer.CreateDrinkAsync(newDrink);
        NewDrink = newDrink;

        IsSaved = true;
        await CloseAsync();
    }

    private async void OnCancelButtonClicked(object sender, EventArgs e)
    {
        await CloseAsync();
    }

    private void OnAddIngredientClicked(object sender, EventArgs e)
    {
        if (SelectedAvailableLiquid != null && !PickedLiquids.Contains(SelectedAvailableLiquid))
        {
            PickedLiquids.Add(SelectedAvailableLiquid);
            AvailableLiquids.Remove(SelectedAvailableLiquid);
        }
    }

    private void OnRemoveIngredientClicked(object sender, EventArgs e)
    {
        if (SelectedPickedLiquid != null)
        {
            AvailableLiquids.Add(SelectedPickedLiquid);
            PickedLiquids.Remove(SelectedPickedLiquid);
        }
    }
}