using CommunityToolkit.Maui.Views; // Sørg for denne er der
//using CommunityToolkit.Maui.Views;
namespace FrontEnd;

public partial class EditProductPopup : Popup
{
    public EditProductPopup(DataTransferObject.Model.ProductDataTransferObject selectedProduct)
    {
        InitializeComponent();
    }
}