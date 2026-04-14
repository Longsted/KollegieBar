using BusinessLogic.BusinessLogicLayer;
using DataTransferObject.Model;

namespace FrontEnd;

public partial class CreateProductPage : ContentPage
{
    private readonly Product _product;
    
    public CreateProductPage()
    {
        InitializeComponent();

        _product = new Product();
        
        

    }
}