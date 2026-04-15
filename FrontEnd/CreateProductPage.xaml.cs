using BusinessLogic.BusinessLogicLayer;
using Data.Context;
using DataTransferObject.Model;
using Microsoft.EntityFrameworkCore.Internal;

namespace FrontEnd;

public partial class CreateProductPage : ContentPage
{
    private readonly ProductBusinessLogicLayer _productBLL;
    
    public CreateProductPage()
    {
        InitializeComponent();
        
        var context = AppDbContext

    }

    private void OnCreateProduct_Clicked(object sender, EventArgs e)
    {
        try
        {
            
        }
        catch (FormatException)
        {
            ResultLabel.Text = "Make sure every textfield is filled";
            throw;
        }
        catch (Exception ex)
        {
            ResultLabel.Text = $"Fejl: {ex.Message}";
        }
    }
    
    
    
}