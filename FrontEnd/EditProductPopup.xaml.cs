namespace FrontEnd;

public partial class EditProductPopup : CommunityToolkit.Maui.Views.Popup
{
    private DataTransferObject.Model.ProductDataTransferObject _product;

    public bool IsSaved { get; private set; } = false;
    public EditProductPopup(DataTransferObject.Model.ProductDataTransferObject selectedProduct)
    {
        InitializeComponent();

        _product = selectedProduct;

        MinStockEntry.Text = _product.MinStockQuantity.ToString();
        MaxStockEntry.Text = _product.MaxStockQuantity.ToString();
        TitleLabel.Text = $"Edit Product: {_product.Name}";
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await CloseAsync();
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (int.TryParse(MinStockEntry.Text, out int min) && min >= 0 &&
            int.TryParse(MaxStockEntry.Text, out int max) && max >= 0)
        {
            if (min > max)
            {
                await Shell.Current.DisplayAlertAsync("Error", "Min stock cannot be greater than Max stock", "OK");
                return;
            }
            _product.MinStockQuantity = min;
            _product.MaxStockQuantity = max;
            IsSaved = true;
            await CloseAsync();
        }
        else
        {
            await Shell.Current.DisplayAlertAsync("Error", "Input has to be a non-negative number", "OK");
        }
    }
}
