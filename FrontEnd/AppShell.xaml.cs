namespace FrontEnd;

public partial class AppShell : Shell
{
    public AppShell(int userType)
    {
        InitializeComponent();
        if (userType == 2) 
        {
            DashboardItem.IsVisible = false;
            CreateProductItem.IsVisible = false;
        }
    }
}